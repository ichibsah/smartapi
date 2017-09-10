using erminas.SmartAPI.CMS.Project.Pages;
using erminas.SmartAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using erminas.SmartAPI.Utils.CachedCollections;
using erminas.SmartAPI.CMS.Project;

namespace fullPublish
{
    public partial class _Default : Page
    {
        public string strLang { get; set; }
        public string output = string.Empty;
        public PageFlags PageFlags;
        public static Dictionary<string, string> dicSession = new Dictionary<string, string>();
        public static Dictionary<string, string> dicXSearch = new Dictionary<string, string>();
        public static IProject selectedProject;

        Thread disableBtn = new Thread(DisableBtn);
        Thread updateOConsole = new Thread(UpdateOConsole);
        Thread oConsoleCount = new Thread(OConsoleCount);
        Thread enableBtn = new Thread(EnableBtn);

        protected void Page_Load(object sender, EventArgs e)
        {

            foreach (string name in Request.QueryString)
            {
                try
                {
                    dicSession.Add(name, Request.QueryString[name]);
                }
                catch {
                    try
                    {
                        dicSession[name] = Request.QueryString[name];
                    } catch {}
                }
            }

            foreach (string name in Request.Form)
            {
                try
                {
                    dicSession.Add(name, Request.Form[name]);
                }
                catch {
                    try
                    {
                        dicSession[name] = Request.Form[name];
                    }
                    catch { }
                }
            }

            showValues(dicSession);

            if(Request.QueryString.Count > 0)
            {
                Session["LoginGuid"] = Request["LoginGuid"];
                Session["SessionKey"] = Request["SessionKey"];
                Session["ProjectGuid"] = Request["ProjectGuid"];
            }

            var url = "http://localhost/cms";
            var login = new ServerLogin(url, null);

            Guid loginGuid = new Guid(Session["LoginGuid"].ToString());
            Guid projectGuid = new Guid(Session["ProjectGuid"].ToString());
            String sessionKey = Session["SessionKey"].ToString();

            var sessionBuilder = new SessionBuilder(login, loginGuid, sessionKey, projectGuid);

            //session
            var session = sessionBuilder.CreateSession();

            //serverManger
            var serverManager = session.ServerManager;

            selectedProject = serverManager.Projects.GetByGuid(projectGuid);
            //var selectedProject = session.ServerManager.Projects["tfl.gov.uk"];
            //var selectedProject = session.SelectedProject;

            TextBox1.Text = "Project: " + selectedProject.Name;

            var projectVariants = selectedProject.ProjectVariants;
            var languageVariants = selectedProject.LanguageVariants;
            var allPages = selectedProject.Pages.OfCurrentLanguage; //does not work. must find alternetive
            
            if(!IsPostBack)
            {

            foreach(var prjVariant in projectVariants)
            {
                    //populate variant in a drop down list
                    ListItem tmpPrjVariant = new ListItem(prjVariant.Name, prjVariant.Name);
                    CheckBoxPrjVariant.Items.Add(tmpPrjVariant);
            }
            
            foreach(var langVariant in languageVariants)
            {
                    //populate variant in a drop down list
                    ListItem tmpLangVariant = new ListItem(langVariant.Name, langVariant.Name);
                    CheckBoxLangVariant.Items.Add(tmpLangVariant);
            }
                        
                TextBox2.Text = "pages to publish: " + allPages.Count().ToString();
               
            }

            /**/
            if(IsPostBack) 
            {
                disableBtn.Start(SubmitButton);
                
                
                //enableBtn.Start(SubmitButton);

                //Button1.Visible = false;
                oConsole.Text = string.Empty;

                //var allContentClass = session.;

                //var pageSearch = selectedProject.Pages.CreateSearch();
                //pageSearch.Category = selectedProject.Pages.;
                //pageSearch.ContentClass = selectedProject.ContentClasses.GetByName("New content class");
                /*
                var allPages = pageSearch.Execute();
                */
               
                //startFullPublish.Start(obj);


                //SubmitButton.Enabled = true;

                /*
                if(Methods.DebugMode > 0)
                {
                    Debug1.Text = Methods.SessionVariables(dicSession) + Methods.viewDebug;
                    Debug1.Visible = true;
                }
                */
                //oConsoleCount.Start(oConsole);

                ThreadStart fullPublish = new ThreadStart(fullPublishCall);
                Thread startFullPublish = new Thread(fullPublish);

                //startFullPublish.Start();

                var allPagess = selectedProject.Pages.OfCurrentLanguage; //does not work. must find alternetive


                foreach(var currPage in allPagess)
                {
                    oConsole.Text += "publishing Page ID: " + currPage.Id.ToString() + "\n";

                    IPagePublishJob prop = currPage.CreatePublishJob();
                    prop.IsPublishingAllFollowingPages = false;
                    prop.IsPublishingRelatedPages = false;
                    prop.LanguageVariants = languageVariants;
                    currPage.CreatePublishJob();
                    currPage.Commit();
                }
                
            }


            ThreadStart childthreat = new ThreadStart(childthreadcall);
            //Response.Write("Child Thread Started <br/>");
            Thread child = new Thread(childthreat);

            //child.Start();

            //Response.Write("Main sleeping  for 2 seconds.......<br/>");
            //Thread.Sleep(2000);
            //Response.Write("<br/>Main aborting child thread<br/>");

            //child.Abort();

        }


        public void fullPublishCall()
        {
            var allPages = selectedProject.Pages.OfCurrentLanguage; //does not work. must find alternetive


            foreach(var currPage in allPages)
            {
                lblmessage.Text += "publishing Page ID: " + currPage.Id.ToString() + "\n";

                var prop = currPage.CreatePublishJob();
                prop.IsPublishingAllFollowingPages = false;
                prop.IsPublishingRelatedPages = false;
                //prop.LanguageVariants = languageVariants;
                currPage.CreatePublishJob();
                currPage.Commit();
            }


            try
            {
                lblmessage.Text = "<br />full publish started <br/>";
                lblmessage.Text += "full publish: Counting to ";



                lblmessage.Text += "<br/> fullPublish finished";

            }
            catch(ThreadAbortException e)
            {

                lblmessage.Text += "<br /> fullPublish - exception";

            }
            finally
            {
                lblmessage.Text += "<br /> fullPublish - unable to catch the  exception";
            }
        }


        public void childthreadcall()
        {
            try
            {
                lblmessage.Text = "<br />Child thread started <br/>";
                lblmessage.Text += "Child Thread: Counting to 10";

                for(int i = 0; i < 10; i++)
                {
                    Thread.Sleep(500);
                    lblmessage.Text += "<br/> in Child thread </br>";
                }

                lblmessage.Text += "<br/> child thread finished";

            }
            catch(ThreadAbortException e)
            {

                lblmessage.Text += "<br /> child thread - exception";

            }
            finally
            {
                lblmessage.Text += "<br /> child thread - unable to catch the  exception";
            }
        }

        private static void DisableBtn(object obj)
        {
            var btn = (Button)obj;
            btn.Enabled = false;
        }

        public static void OConsoleCount(object obj)
        {
            
            var oConsoleText = (TextBox)obj;
            for(int i = 0; i < 999; i++)
            {
                oConsoleText.Text = oConsoleText.Text +" " + i + "\n";
            }
        }

        private static void UpdateOConsole(object obj)
        {
            var oConsoleText = (TextBox)obj;
            oConsoleText.Text = oConsoleText.Text + "publishing Page ID: " + "pageId" + "\n";

        }
        
        private static void EnableBtn(object obj)
        {

            var btn = (Button)obj;
            btn.Enabled = true;
        }

        private void showValues(Dictionary<string, string> dicSession)
        {
            Debug1.Visible = true;
            Debug1.Text = string.Empty;
            foreach(var item in dicSession)
            {
                Debug1.Text = Debug1.Text + " " + item.Key + ": " + item.Value + "\n";

            }
        }

        protected void oConsole_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //disableBtn.Start(SubmitButton);
            //oConsoleCount.Start(oConsole);
            //SubmitButton.Enabled = true;
            //enableBtn.Start(SubmitButton);
            Console.WriteLine("You click me ...................");
            System.Diagnostics.Debug.WriteLine("You click me ..................");
            System.Diagnostics.Trace.WriteLine("You click me ..................");
        }

        protected void CheckBoxPrjVariant_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBoxLangVariant_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
/* https://www.youtube.com/watch?v=Lvt1BnSwRvo&index=5&list=PL6n9fhu94yhXQS_p1i-HLIftB9Y7Vnxlo */
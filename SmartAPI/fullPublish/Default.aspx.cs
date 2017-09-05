using erminas.SmartAPI.CMS.Project;
using erminas.SmartAPI.CMS.Project.Pages;
using erminas.SmartAPI.CMS.Project.Publication;
using erminas.SmartAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fullPublish
{
    public partial class _Default : Page
    {
        protected void init(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
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

            var selectedProject = serverManager.Projects.GetByGuid(projectGuid);
            //var selectedProject = session.ServerManager.Projects["tfl.gov.uk"];
            //var selectedProject = session.SelectedProject;

            TextBox1.Text = "Project: " + selectedProject.Name;

            var projectVariants = selectedProject.ProjectVariants;
            foreach(var prjVariant in projectVariants)
            {
                if(!IsPostBack)
                {
                    //populate variant in a drop down list
                    ListItem tmp = new ListItem(prjVariant.Name, prjVariant.Name);
                    CheckBoxPrjVariant.Items.Add(tmp);
                }
            }

            var languageVariants = selectedProject.LanguageVariants;
            foreach(var langVariant in languageVariants)
            {

                if(!IsPostBack)
                {
                    //populate variant in a drop down list
                    ListItem tmpLangVariant = new ListItem(langVariant.Name, langVariant.Name);
                    CheckBoxLangVariant.Items.Add(tmpLangVariant);
                }
            }
            
            if(IsPostBack)
            {
                oConsole.Text = "";
                
                //var allContentClass = session.;
                /*
                var pageSearch = selectedProject.Pages.CreateSearch();
                pageSearch.ContentClass = selectedProject.ContentClasses.GetByName("New content class");

                var allPages = pageSearch.Execute();
                */
                var allPages = selectedProject.Pages.OfCurrentLanguage; //does not work. must find alternetive
                TextBox2.Text = "pages to publish: " + allPages.Count().ToString();


                foreach(var currPage in allPages)
                {
                    oConsole.Text = oConsole.Text + "publishing Page ID: " + currPage.Id.ToString() + "\n";

                    var prop = currPage.CreatePublishJob();
                    prop.IsPublishingAllFollowingPages = false;
                    prop.IsPublishingRelatedPages = false;
                    prop.LanguageVariants = languageVariants;
                    //currPage.CreatePublishJob();
                    currPage.Commit();

                }

                Button1.Enabled = true;
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
            System.Threading.Thread.Sleep(30);
            Button1.Enabled = false;
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
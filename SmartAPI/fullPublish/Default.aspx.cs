using erminas.SmartAPI.CMS.Project;
using erminas.SmartAPI.CMS.Project.Pages;
using erminas.SmartAPI.CMS.Project.Publication;
using erminas.SmartAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace fullPublish
{
    public partial class _Default : Page
    {
        protected void init(object sender, EventArgs e)
        {


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoginGuid"] = Request["LoginGuid"];
            Session["SessionKey"] = Request["SessionKey"];
            Session["ProjectGuid"] = Request["ProjectGuid"];

            var url = "http://localhost/cms";
            var login = new ServerLogin(url, null);

            Guid test = new Guid(Session["LoginGuid"].ToString());
            Guid loginGuid;
            Guid projectGuid;
            Guid.TryParse(Convert.ToString(Session["LoginGuid"]), out loginGuid);
            String sessionKey = Convert.ToString(Session["SessionKey"]);
            Guid.TryParse(Convert.ToString(Session["ProjectGuid"]), out projectGuid);

            var sessionBuilder = new SessionBuilder(login, loginGuid, sessionKey, projectGuid);

            //session
            var session = sessionBuilder.CreateSession();

            //serverManger
            var serverManager = session.ServerManager;

            IProject selectedProject = serverManager.Projects.ForCurrentUser.GetByGuid(projectGuid);
            //var selectedProject = session.SelectedProject;


            TextBox1.Text = "Project: " + selectedProject.Name;

            var projectVariant = selectedProject.ProjectVariants;

            foreach(var prjVariant in projectVariant)
            {
                //populate variant in a drop down list
                TextVariant.Text = prjVariant.Name;
            }

            var allPages = selectedProject.Pages.OfCurrentLanguage;
            TextBox2.Text = allPages.Count().ToString();

            Pages _pages = new Pages(selectedProject);

            var languageVariant = selectedProject.LanguageVariants.Current;

            //var getPage = _pages.GetByGuid(pageGuid, languageVariant);
            //_pages.

            
            IProject proj = selectedProject;
            
            //var newPage = selectedProject.Pages.OfCurrentLanguage[0];

            List<string> pubList = new List<string>();

            foreach(IPublicationTarget t in proj.PublicationTargets.ToList())
            {

                oConsole.Text += t.Name + "<br>";

                pubList.Add(t.Name);

            }

            IPublicationTarget target = selectedProject.PublicationTargets.GetByName(pubList[0]);

            oConsole.Text = "Guid: " + target.Guid.ToString()

            + "<br> Name: " + target.Name

            + "<br> project: " + target.Project

            + "<br> Session: " + target.Session

            + "<br> url prefix: " + target.UrlPrefix

            + "<br> Type: " + target.Type

            + "<br> <br><br><br><br>";

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
    }
}
/* https://www.youtube.com/watch?v=Lvt1BnSwRvo&index=5&list=PL6n9fhu94yhXQS_p1i-HLIftB9Y7Vnxlo */
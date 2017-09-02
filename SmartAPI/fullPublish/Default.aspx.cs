using erminas.SmartAPI.Utils;
using erminas.SmartAPI.CMS.Project;
using erminas.SmartAPI.CMS.ServerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using erminas.SmartAPI.CMS.Project.Pages;
using erminas.SmartAPI.Utils;

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

 
            Guid loginGuid;
            Guid projectGuid;
            Guid.TryParse(Convert.ToString(Session["LoginGuid"]), out loginGuid);
            String sessionKey = Convert.ToString(Session["SessionKey"]);
            Guid.TryParse(Convert.ToString(Session["ProjectGuid"]), out projectGuid);

            var sessionBuilder = new SessionBuilder(login, loginGuid, sessionKey, projectGuid);

            //note that we don't use the using statement because we usually
            //do not want to close the running cms session, when we are done (e.g. in a plugin)
            var session = sessionBuilder.CreateSession();
            //session.
            //TextBox1.Text = sessionKey;
            
            IProject project;
            session.ServerManager.Projects.TryGetByGuid(projectGuid, out project);

            String projectName = project.Name;

            TextBox1.Text = projectName;



        }

        protected void TextBox1_TextChanged(object sender, System.EventArgs e)
        {

        }


    }
}
/* https://www.youtube.com/watch?v=Lvt1BnSwRvo&index=5&list=PL6n9fhu94yhXQS_p1i-HLIftB9Y7Vnxlo */
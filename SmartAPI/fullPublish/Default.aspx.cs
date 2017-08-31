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
        protected void Page_Load(object sender, EventArgs e)
        {
            //url of the cms server
            var url = "http://mycmsserver/cms";
 
            //Authentication data can be null, if you want to use an existing session.
            //Note that there are some methods however which require the users password to be set,
            //e.g. the deletion of keywords. This is stated in the documentation of the according methods.
            var login = new ServerLogin(url, null);
 
            Guid loginGuid = ...;
            string sessionKey = ...;
            Guid projectGuid = ...;
            var sessionBuilder = new SessionBuilder(login, loginGuid, sessionKey, projectGuid);
 
            //note that we don't use the using statement because we usually
            //do not want to close the running cms session, when we are done (e.g. in a plugin)
            var session = sessionBuilder.CreateSession();
        }

       
    }
}
/* https://www.youtube.com/watch?v=Lvt1BnSwRvo&index=5&list=PL6n9fhu94yhXQS_p1i-HLIftB9Y7Vnxlo */
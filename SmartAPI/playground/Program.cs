using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using erminas.SmartAPI.CMS.Project.Pages;
using erminas.SmartAPI.Utils;
using erminas.SmartAPI.CMS.ServerManagement;
using erminas.SmartAPI.CMS.Project;

namespace playground
{
    class Program
    {
        static void Main(string[] args)
        {
            //url of the cms server
            var url = "http://localhost/cms";

            //Authentication data can be null, if you want to use an existing session.
            //Note that there are some methods however which require the users password to be set,
            //e.g. the deletion of keywords. This is stated in the documentation of the according methods.
            var user = "";
            var password = "";

            SessionContext.UserID = teacher.TeacherID;
            SessionContext.UserRole = Role.Teacher;

            // Global context for operations performed by MainWindow
    

        var authData = new PasswordAuthentication(user, password);
            //var login = new ServerLogin() { Address = new Uri(url), AuthData = authData };
            ServerLogin login = new ServerLogin(url, authData){ Name = "adminLogin" };
            //var login = new ServerLogin(url, null);
            var session = SessionBuilder.CreateOrReplaceOldestSession(login);
            if (session.GetLoginStatus()) { };

            var serverManager = session.ServerManager;

            //these are all projects, the active user is assigned to
            var currentUsersProjects = serverManager.Projects.ForCurrentUser;
            Guid loginGuid = session.LogonGuid;
            var sessionKey = session.SessionKey;
            var projectGuidDropList = currentUsersProjects.ToList();
            //var projectGuid = serverManager.Projects.ForCurrentUser.GetByGuid(new Guid());
            var projectGuid = new Guid();
            var selectedProject = (from IProject sp in currentUsersProjects
                                   where currentUsersProjects.ContainsName("prjName")
                                   select sp.Guid);

            var sessionBuilder = new SessionBuilder(login, loginGuid, sessionKey, projectGuid);
            //sessionBuilder.

                //note that we don't use the using statement because we usually
                //do not want to close the running cms session, when we are done (e.g. in a plugin)
                //var session = sessionBuilder.CreateSession();



                var project = session.ServerManager.Projects["nav_demo"];
                var search = project.Pages.CreateSearch();
                search.PageType = PageType.Unlinked;
                var unlinkedPages = search.Execute();

                IEnumerable<IPage> processedPages = new List<IPage>();

                while (unlinkedPages.Any())
                {
                    foreach (var curPage in unlinkedPages)
                    {
                        Console.WriteLine("Deleting " + curPage);
                        //curPage.Delete();
                    }
                    processedPages = processedPages.Union(unlinkedPages);
                    unlinkedPages = search.Execute().Where(page => !processedPages.Contains(page));
                }

                Console.WriteLine("Done");
            

        }
    }
}

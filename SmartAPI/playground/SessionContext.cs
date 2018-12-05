using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playground
{
    // Global context for operations performed by MainWindow
    public static class SessionContext
    {
        public static int UserID;
        public static string UserName;
        public static Role UserRole;
        public static Student CurrentStudent;
        public static Teacher CurrentTeacher;

        /**
         * var user = "";
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
    */
    }
}

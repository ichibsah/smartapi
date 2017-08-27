using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using erminas.SmartAPI.CMS.Project.Pages;
using erminas.SmartAPI.Utils;
using erminas.SmartAPI.CMS.Project;
using erminas.SmartAPI.CMS.ServerManagement;

public class DoSomething
{
    static void Main(string[] args)
    {
        var authData = new PasswordAuthentication("admin", "123456");
        var url = "http://localhost/cms";

        var login = new ServerLogin(url, authData);
        using(var session = SessionBuilder.CreateOrReplaceOldestSession(login))
        {
            //this is the currently active project in the session
            var selectedProject = session.SelectedProject;

            var serverManager = session.ServerManager;

            //these are all projects, the active user is assigned to
            var currentUsersProjects = serverManager.Projects.ForCurrentUser;

            //these are all projects for the user xy, you need to be server manager to do this
            var user = serverManager.Users["some username"];
            var projectsOfUserXy = serverManager.Projects.ForUser(user.Guid);

            //these are all projects on the server, you need to be server manager to access projects you are not assigned to
            var allProjects = serverManager.Projects;

            //you can access a single project by name
            //var myProject = serverManager.Projects.ForCurrentUser["myproject"];
            //this is the short for
            //var myProjectToo = serverManager.Projects.ForCurrentUser.GetByName("myproject");

            //if you do not know whether a specific project exists, you can use
            IProject unknownProject;
            if(serverManager.Projects.ForCurrentUser.TryGetByName("nav_demo", out unknownProject))
            {
                Console.WriteLine("Found project with guid: " + unknownProject.Guid);
            }
            else
            {
                Console.WriteLine("No project with name myproject assigned to user");
            }

            //you can also get a project by guid
            var projectGuid = new Guid("...");
            var projectByGuid = serverManager.Projects.ForCurrentUser.GetByGuid(projectGuid);
            // a TryGetByGuid method is available, too

            //print all project names and guids
            Console.WriteLine("Projects:");
            foreach(var curProject in serverManager.Projects)
            {
                Console.WriteLine(curProject);
            }
        }
    }
}
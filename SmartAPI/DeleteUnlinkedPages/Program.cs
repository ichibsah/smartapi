using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using erminas.SmartAPI.CMS.Project.Pages;
using erminas.SmartAPI.Utils;

namespace DeleteUnlinkedPages
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = args[0];
            var user = args[1];
            var password = args[2];

            var authData = new PasswordAuthentication(user, password);
            var login = new ServerLogin() { Address = new Uri(url), AuthData = authData };

            using(var session = SessionBuilder.CreateOrReplaceOldestSession(login))
            {
                var project = session.ServerManager.Projects["nav_demo"];
                var search = project.Pages.CreateSearch();
                search.PageType = PageType.Unlinked;
                var unlinkedPages = search.Execute();

                IEnumerable<IPage> processedPages = new List<IPage>();

                while(unlinkedPages.Any())
                {
                    foreach(var curPage in unlinkedPages)
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
}

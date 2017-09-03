using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullPublish
{
public class Class1
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
//var project = session.ServerManager.Projects.;
IProject project;
session.ServerManager.Projects.TryGetByGuid(projectGuid, out project);

IProject selectedProject = serverManager.Projects.ForCurrentUser.GetByGuid(projectGuid);
var getElement = erminas.SmartAPI.CMS.Project.Pages.Elements.PageElementFactory.Instance.CreateElement(selectedProject, elementGuid, languageVariant);


/***************
//IProject project;
// session.ServerManager.Projects.TryGetByGuid(projectGuid, out project);

String projectName = project.Name;

TextBox1.Text = projectName;

IContentClasses contentClass = project.ContentClasses;
//oConsole.Text = project.Pages.StartPages.ForMainLanguage.Count().ToString(); //works
var search = project.Pages.CreateSearch();
//search.Category = 
search.PageType = PageType.Unlinked;
var unlinkedPages = search.Execute();
//project.Pages.
//unlinkedPages.
*/
/******/
//this is the currently active project in the session

var selectedProject = session.SelectedProject;

var serverManager = session.ServerManager;

oConsole.Text = "Project: " + selectedProject.Name + "<br>";

var projectVariant = project.ProjectVariants;

IProject proj = selectedProject;
List<string> pubList = new List<string>();
            
            
foreach(project.Publication.IPublicationTarget t in proj.PublicationTargets.ToList())
{

oConsole.Text += t.Name + "<br>";

pubList.Add(t.Name);

}

project.Publication.IPublicationTarget target = selectedProject.PublicationTargets.GetByName(pubList[0]);

oConsole.Text = "Guid: " + target.Guid.ToString()

+ "<br> Name: " + target.Name

+ "<br> project: " + target.Project

+ "<br> Session: " + target.Session

+ "<br> url prefix: " + target.UrlPrefix

+ "<br> Type: " + target.Type

+ "<br> <br><br><br><br>";

private List<XmlElement> GetPublicationFolder()
{
//Get the selected project
var session = ServerConnection();
var serverManager = session.ServerManager;
IProject selectedProject = serverManager.Projects.ForCurrentUser.GetByGuid(projectGuid);
List<XmlElement> urlList = new List<XmlElement>();
//loop through all the publicationtargets and get the elements of each 
foreach (project.Publication.IPublicationFolder t in selectedProject.PublicationFolders.ToList())
{
var element = (XmlElement)t.GetType().GetProperty("XmlElement").GetValue(t, null);
//add each element into a list
urlList.Add(element);
}

return urlList;

}
       

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using erminas.SmartAPI.CMS.Project;
using erminas.SmartAPI.CMS.ServerManagement;
using System.Net;
using erminas.SmartAPI.Utils;
using erminas.SmartAPI.CMS;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var authData = new PasswordAuthentication("", "");
        var url = "";
        var login = new ServerLogin(url, authData);
        using (var session = SessionBuilder.CreateOrReplaceOldestSession(login))
        {

            var selectedProject = session.SelectedProject;
            var serverManager = session.ServerManager;

            var languageVariant = selectedProject.LanguageVariants.Current;
            var pageGuid = new Guid("");
            var elementGuid = new Guid("");

            var getElement = (ITextHtml)erminas.SmartAPI.CMS.Project.Pages.Elements.PageElementFactory.Instance.CreateElement(selectedProject, elementGuid, languageVariant);
            getElement.Value = "<p>my new text</p>";
            getElement.Commit();

    }
}

    
                    var project = session.ServerManager.Projects.GetByName("ProjectName");
                    var contentClass = project.ContentClasses.GetByName("ContentClass");
                    var announcement = project.Pages.CreateAndConnect(contentClass, LinkPageGuid, "Headline");
                    announcement.Filename = announcement.Id.ToString();

                    var elements = announcement.ContentElements.ToList();

But now I need to set values for elements on the newly created page, does the value property not have a set?
                    elements[0].Value = "ElementContent"; //??


    'm not 100% sure i got your question, but i'll try to answer :)
IOptionList is a bit of a special case. You can get the possible values from
optionList.PossibleValues

So it would normally look something like

var valueToSet = optionListElement.PossibleValues.First(v=>v.Name == "MyValue");
optionListElement.Value = valueToSet;
optionListElement.Commit();


    var currentUsersProjects = session.ServerManager.Projects.ForCurrentUser;
foreach (var curProject in currentUsersProjects) {


}


    
 var login = new ServerLogin { Address = new Uri("http://localhost/cms"), AuthData = authData };

  using (var session = SessionBuilder.CreateOrReplaceOldestSession(login))
  {
      Response.Write("Projektname: " + project + ".<br/>");
      var erminas_project = session.ServerManager.Projects.GetByName(project);

      string sParamA = "<IODATA loginguid=\"" + session.SessionKey + "\" sessionkey=\"" + session.SessionKey + "\"><PREVIEW projectguid=\"" + session.SelectedProject.Guid.ToString().Replace("-","").ToUpper() + "\" loginguid=\"" + session.SessionKey + "\" url=\"/CMS/ioRD.asp\" querystring=\" Action=Preview&amp;Pageguid=" + pageguid + "\" /></IODATA>";
      object sErrorA = new object();
      object sResultInfoA = new object();
      ExecuteRequest ExecuteRequestObject = new ExecuteRequest(sParamA, sErrorA, sResultInfoA);
      //ExecuteResponse ExecuteResponseObject = new ExecuteResponse(sParamA, sErrorA, sResultInfoA);
      //Response.Write(ExecuteResponseObject.Result.ToString());
      //Response.Write(ExecuteResponseObject.sResultInfoA.ToString());
      Response.Write(Execute(sParamA));
  }



using (var session = SessionBuilder.CreateOrReplaceOldestSession(login))
                {
                    //Response.Write("Projektname: " + project + ".<br/>");
                    var erminas_project = session.ServerManager.Projects.GetByName(project);

                    // Find all pages based on the Content Class "MyContentClass"
                    string RQLRequest = "<PREVIEW projectguid=\"" + session.SelectedProject.Guid.ToString().Replace("-", "").ToUpper() + "\" loginguid=\"" + session.SessionKey + "\" url=\"./PreviewHandler.ashx\" querystring=\"action=RedDot&amp;mode=0&amp;pageguid=" + pageguid + "\" />";
                    String RQLResponse = erminas_project.ExecuteRQL(RQLRequest).ToString();
                    Response.Write(RQLResponse);
                }






var authData = new PasswordAuthentication(#user#, #password#);
var login = new ServerLogin {Address = new Uri(#cmsserverurl#),AuthData = authData};
using (var session = SessionBuilder.CreateOrReplaceOldestSession(login))
{
	var project = session.SelectedProject;
	var page=project.Pages.GetByGuid(new Guid(#pageguid#), project.LanguageVariants.Current);
	var elt = (IText) page.ContentElements.GetByName(#texteltname#);
	elt.Value = elt.Value.Replace("a", "abc");
	elt.Commit();
}



using

project.Folders.AssetManagerFolders.GetByName("nameOfMyFolder").Files.​GetByNamePattern("nameOfFile)


instead of .TryGet should speed it up considerably.



TryGet leads to SmartAPI loading the whole list of files and filtering locally, while GetByNamePattern searches on the server and the SmartAPI only loads

the results (we probably should mention that in the documentation ;) ).



Best regards


var newPage = project.Pages.OfCurrentLanguage[209];

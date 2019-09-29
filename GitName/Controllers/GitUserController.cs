using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Web.Mvc;
using GitName.Models;

namespace GitName.Controllers
{
    public class GitUserController : Controller
    {
        private MyJSon MyJsonObject { get; set; }
        // GET: GitUser
        public ActionResult Index(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            GetGitUser("https://api.github.com/users/" + searchString);
            return View(MyJsonObject);
        }

       private void GetGitUser(string url)
        {
            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {
                        string reader = responseReader.ReadToEnd();
                        MyJsonObject = JsonConvert.DeserializeObject<MyJSon>(reader);
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            
        }
    }
}
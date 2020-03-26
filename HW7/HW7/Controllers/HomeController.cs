using HW7.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace HW7.Controllers
{
    public class HomeController : Controller
    {
        private readonly string myToken = System.Web.Configuration.WebConfigurationManager.AppSettings["token"];

        public ActionResult Index()
        {
            string userString = SendRequest("https://api.github.com/user", myToken, "ksh0rt");

            JObject dataObject = JObject.Parse(userString);
            User theUser = new User(dataObject);

            UserRepos(theUser);


            return View(theUser);
        }

        private string SendRequest(string uri, string credentials, string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + credentials);
            request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
            request.Accept = "application/json";

            string jsonString = null;
            // TODO: You should handle exceptions here
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
        }

        public void UserRepos(User user)
        {
            List<Repo> repos = new List<Repo>();

            string repoString = SendRequest("https://api.github.com/user/repos", myToken, "ksh0rt");

            JArray repoArray = JArray.Parse(repoString);
            for(int i = 0; i < repoArray.Count; i++)
            {
                Repo currentRepo = new Repo(repoArray[i]);
                repos.Add(currentRepo);
            }
            user.repoList = repos;
        }
    }
}
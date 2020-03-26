using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW7.Models
{
    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string location { get; set; }
        public string profilePic { get; set; }
        public string bio { get; set; }
        public List<Repo> repoList;

        public User(JToken user)
        {
            name = user.Value<string>("name");
            email = user.Value<string>("email");
            userName = user.Value<string>("login");
            location = user.Value<string>("location");
            bio = user.Value<string>("bio");
            profilePic = (string)user["avatar_url"];
            repoList = new List<Repo>();
        }
    }

}
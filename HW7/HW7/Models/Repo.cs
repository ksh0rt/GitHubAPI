using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW7.Models
{
    public class Repo
    {
        public string name { get; set; }
        public string repoName { get; set; }
        public string edited { get; set; }

        public Repo(JToken repo)
        {
            name = repo.Value<string>("name");
            repoName = repo.Value<string>("full_name");
            edited = repo.Value<string>("updated_at");
        }
    }
}
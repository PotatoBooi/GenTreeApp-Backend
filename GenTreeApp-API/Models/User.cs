using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class User
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public HashSet<Tree> TreeList { get; set; }
    }
}
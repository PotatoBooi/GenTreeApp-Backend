using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class Relation
    {
        public string UUID { get; set; }
        public string[] Parents_UUID { get; set; }
        public string Child_UUID { get; set; }

    }
}
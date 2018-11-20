using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class Person
    {
        public string UUID { get; set; }
        public HashSet<Relation> RelationList{get; set;}
        public string Details_UUID { get; set; }
        

    }
}
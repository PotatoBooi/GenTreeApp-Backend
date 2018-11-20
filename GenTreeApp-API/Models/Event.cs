using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class Event
    {
        public string UUID { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
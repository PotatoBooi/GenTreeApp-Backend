using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GenTreeApp_API.Models
{

    public class Tree
    {
        public string UUID { get; set; }
        public bool Editable { get; set; }
        public string Person_UUID { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class Details
    {
        public string UUID { get; set; }
        public HashSet<Media> MediaList { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public HashSet<Comment> CommentList { get; set; }
        public HashSet<Event> EventList { get; set; }

    }
}
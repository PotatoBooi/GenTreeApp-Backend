using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class Details
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }
        public virtual ICollection<Media> MediaList { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public virtual ICollection<Comment> CommentList { get; set; }
        public virtual ICollection<Event> EventList { get; set; }

    }
}
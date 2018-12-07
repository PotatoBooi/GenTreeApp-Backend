using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Details
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Sex Sex { get; set; }
        public ICollection<Comment> CommentList { get; set; }
        public ICollection<Event> EventList { get; set; }
        public ICollection<Media> MediaList { get; set; }


    }

    public enum Sex
    {
        [DisplayName("Male")]
        Male,
        [DisplayName("Female")]
        Female
    }

}
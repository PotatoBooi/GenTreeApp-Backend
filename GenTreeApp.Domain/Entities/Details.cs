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
        //navigation properties
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Media> Media { get; set; }
        public Person Person { get; set; }

        public Details()
        {
            Comments = new HashSet<Comment>();
            Events = new HashSet<Event>();
            Media = new HashSet<Media>();
        }
    }

    public enum Sex
    {
        [DisplayName("Male")]
        Male,
        [DisplayName("Female")]
        Female,
        [DisplayName("Unknown")]
        Unknown

    }

}
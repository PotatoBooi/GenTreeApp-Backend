using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Comment
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Body { get; set; }
        public Person Person { get; set; }
        //navigation property
        public Details Details { get; set; }
    }
}
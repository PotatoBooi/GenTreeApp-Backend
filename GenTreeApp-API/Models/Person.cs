using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }
        public virtual ICollection<Relation> RelationList{get; set;}
        public virtual Details Details { get; set; }
        

    }
}
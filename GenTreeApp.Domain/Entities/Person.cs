using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Details Details { get; set; }
        public ICollection<Relation> Relations1{get; set;}
        public ICollection<Relation> Relations2{get; set;}

        public Person()
        {
            Relations1 = new HashSet<Relation>();
            Relations2 = new HashSet<Relation>();
        }

    }
}
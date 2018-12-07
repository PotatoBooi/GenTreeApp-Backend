using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid DetailsId { get; set; }
        public Details Details { get; set; }
        public ICollection<Relation> Relations{get; set;}

        public Person()
        {
            Relations = new HashSet<Relation>();
        }

    }
}
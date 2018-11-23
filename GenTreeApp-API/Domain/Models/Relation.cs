using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp_API.Domain.Models
{
    public class Relation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }
        public virtual ICollection<Person> Parents { get; set; }
        public virtual Person Child { get; set; }

    }
}
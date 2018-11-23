using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp_API.Domain.Models
{

    public class Tree
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid UUID { get; set; }
        public bool Editable { get; set; }
        public virtual ICollection<Relation> Relations { get; set; }

    }
}
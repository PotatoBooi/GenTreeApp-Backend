using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GenTreeApp_API.Models
{

    public class Tree
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid UUID { get; set; }
        public bool Editable { get; set; }
        public virtual ICollection<Person> Persons { get; set; }

    }
}
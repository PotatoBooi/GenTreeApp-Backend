using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Models
{
    public class Relation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }
        public virtual ICollection<Person> Parents { get; set; }
        public virtual Person Child { get; set; }

    }
}
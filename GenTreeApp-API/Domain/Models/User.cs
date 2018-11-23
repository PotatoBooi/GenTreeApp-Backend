using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp_API.Domain.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Tree> TreeList { get; set; }
    }
}
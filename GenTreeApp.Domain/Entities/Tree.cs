using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{

    public class Tree
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public bool Editable { get; set; }
        public string Name { get; set; }
        public ICollection<Person> Persons { get; set; }


        public ICollection<UserTree> UserTrees { get; set; }

        public Tree()
        {
            Persons = new HashSet<Person>();
            UserTrees = new HashSet<UserTree>();
        }

    }
}
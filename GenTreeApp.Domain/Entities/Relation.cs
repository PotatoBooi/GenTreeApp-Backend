using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Relation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public RelationType Type { get; set; }
        public Person Person { get; set; }
        public Person SecondPerson { get; set; }


    }

    public enum RelationType
    {
       Child,
       Marriage

    }
}
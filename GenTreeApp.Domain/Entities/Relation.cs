using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Relation
    {
        public RelationType Type { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public Person SecondPerson { get; set; }
        public Guid SecondPersonId { get; set; }

    }

    public enum RelationType
    {
        [DisplayName("Parent")]
        Parent,
        [DisplayName("Child")]
        Child,
        [DisplayName("Sibling")]
        Sibling,
        [DisplayName("Marriage")]
        Marriage
       
    }
}
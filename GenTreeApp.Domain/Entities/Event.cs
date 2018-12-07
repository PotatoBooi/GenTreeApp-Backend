using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
    }

    public enum EventType
    {
        [DisplayName("Birth")]
        Birth,
        [DisplayName("Death")]
        Death
    }
}
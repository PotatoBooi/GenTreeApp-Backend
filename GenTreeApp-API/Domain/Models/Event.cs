using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp_API.Domain.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
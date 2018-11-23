using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp_API.Domain.Models
{
    public class Media
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }

        public string Type { get; set; }
        public string Url { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenTreeApp.API.DTOs.Event
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}

using System;

namespace GenTreeApp.API.DTOs.Event
{
    public class EventCreationDto
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
}
}

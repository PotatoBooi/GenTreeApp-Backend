using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using GenTreeApp.API.DTOs.Event;
using GenTreeApp.API.DTOs.Media;
using GenTreeApp.API.DTOs.Relation;

namespace GenTreeApp.API.DTOs.Person
{
    public class PersonCreationDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public List<RelationCreationDto> Relations { get; set; } = new List<RelationCreationDto>();
        public List<EventCreationDto> Events { get; set; }= new List<EventCreationDto>();
        public List<MediaCreationDto> Media { get; set; } = new List<MediaCreationDto>();
       
    }
}

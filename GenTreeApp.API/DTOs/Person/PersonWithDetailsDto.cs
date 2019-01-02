using GenTreeApp.API.DTOs.Relation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenTreeApp.API.DTOs.Person
{
    public class PersonWithDetailsDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public Guid DetailsId { get; set; }
        public string AvatarUrl { get; set; }
        public List<RelationDto> Relations { get; set; }
    }
}

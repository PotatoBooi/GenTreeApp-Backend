using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenTreeApp.API.DTOs.Details;
using GenTreeApp.API.DTOs.Relation;

namespace GenTreeApp.API.DTOs.Person
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public DetailsDto Details { get; set; }
        public List<RelationDto> Relations { get; set; }

        public PersonDto()
        {
            Relations = new List<RelationDto>();
        }
    }
}

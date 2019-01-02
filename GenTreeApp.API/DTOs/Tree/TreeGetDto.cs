using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenTreeApp.API.DTOs.Person;

namespace GenTreeApp.API.DTOs.Tree
{
    public class TreeGetDto
    {
        public Guid Id { get; set; }
        //public string Name { get; set; }
        public List<PersonWithDetailsDto> Persons { get; set; }

    }
}

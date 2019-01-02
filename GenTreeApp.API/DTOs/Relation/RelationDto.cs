using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenTreeApp.Domain.Entities;

namespace GenTreeApp.API.DTOs.Relation
{
    public class RelationDto
    {
        public Guid FirstPersonId { get; set; }
        public Guid SecondPersonId { get; set; }
        public string Type { get; set; }
    }
}

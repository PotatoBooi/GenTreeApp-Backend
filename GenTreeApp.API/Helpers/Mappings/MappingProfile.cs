using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenTreeApp.API.DTOs.Event;
using GenTreeApp.API.DTOs.Media;
using GenTreeApp.API.DTOs.Person;
using GenTreeApp.API.DTOs.Relation;
using GenTreeApp.API.DTOs.Tree;
using GenTreeApp.Domain.Entities;

namespace GenTreeApp.API.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TreeCreationDto, Tree>();
            CreateMap<PersonCreationDto, Person>()
                .ForPath(dest => dest.Details.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Details.Surname, opt => opt.MapFrom(src => src.Surname)); // add enum resolver 


            CreateMap<RelationDto, Relation>();//figure out how to map created relation to entity model

            CreateMap<EventCreationDto, Event>();
            CreateMap<MediaCreationDto, Media>();

        }
    }
}

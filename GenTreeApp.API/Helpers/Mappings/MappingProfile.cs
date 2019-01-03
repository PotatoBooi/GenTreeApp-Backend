using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenTreeApp.API.DTOs.Comment;
using GenTreeApp.API.DTOs.Event;
using GenTreeApp.API.DTOs.Media;
using GenTreeApp.API.DTOs.Person;
using GenTreeApp.API.DTOs.Relation;
using GenTreeApp.API.DTOs.Tree;
using GenTreeApp.Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace GenTreeApp.API.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TreeCreationDto, Tree>();
            CreateMap<EventCreationDto, Event>()
                .ForPath(dest => dest.Details.Id, opt => opt.MapFrom(src => src.DetailsId));
            CreateMap<MediaCreationDto, Media>()
                .ForPath(dest=>dest.Details.Id,opt=>opt.MapFrom(src=>src.DetailsId))
                .ForPath(dest=>dest.User.Id,opt=>opt.MapFrom(src=>src.UserId));
            CreateMap<PersonCreationDto, Person>()
                .ForPath(dest => dest.Details.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Details.Surname, opt => opt.MapFrom(src => src.Surname)); // add enum resolver 

            CreateMap<Sex,string>()
                .ConvertUsing(src=>src.ToString());
            CreateMap<EventType, string>()
                .ConvertUsing(src => src.ToString());
            CreateMap<RelationType, string>()
                .ConvertUsing(src => src.ToString());
            CreateMap<MediaType, string>()
                .ConvertUsing(src => src.ToString());

        
            CreateMap<Tree, TreeGetDto>();
            CreateMap<Person, PersonDto>()
                .ForMember(dest=>dest.Relations,opt=>opt.MapFrom(src=>mergeLists(src))) ;
                
            CreateMap<RelationCreationDto, Relation>()
                .ForPath(dest => dest.Person.Id, opt => opt.MapFrom(src => src.FirstPersonId))
                .ForPath(dest => dest.SecondPerson.Id, opt => opt.MapFrom(src => src.SecondPersonId));
            CreateMap<Relation, RelationDto>()
                .ForMember(dest => dest.FirstPersonId, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.SecondPersonId, opt => opt.MapFrom(src => src.SecondPerson.Id));
            CreateMap<Media, MediaDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentCreationDto, Comment>()
                .ForPath(dest => dest.Details.Id, opt => opt.MapFrom(src => src.DetailsId));


        }

        private List<Relation> mergeLists(Person person)
        {
            var list1 = person.Relations1.ToList();
            var list2 = person.Relations2.ToList();
            List<Relation> merged = new List<Relation>();
            merged.AddRange(list1);
            merged.AddRange(list2);
            return merged;
        }
    }
}

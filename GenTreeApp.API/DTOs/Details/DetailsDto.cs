using System;
using System.Collections.Generic;
using GenTreeApp.API.DTOs.Comment;
using GenTreeApp.API.DTOs.Event;
using GenTreeApp.API.DTOs.Media;

namespace GenTreeApp.API.DTOs.Details
{
    public class DetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        //navigation properties
        public List<CommentDto> Comments { get; set; }
        public List<EventDto> Events { get; set; }
        public List<MediaDto> Media { get; set; }
    }
}

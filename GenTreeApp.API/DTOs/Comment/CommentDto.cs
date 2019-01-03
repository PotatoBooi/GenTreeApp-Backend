using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenTreeApp.API.DTOs.Comment
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
    }
}

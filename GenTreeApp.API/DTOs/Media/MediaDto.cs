using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenTreeApp.API.DTOs.Media
{
    public class MediaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        

    }
}

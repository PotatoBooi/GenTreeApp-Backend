using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenTreeApp_API.Domain.DTOs
{
    public class PersonDetailDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public string AvatarUrl { get; set; }

        public PersonDetailDto(string name, string surname, string sex, string avatarUrl)
        {
            Name = name;
            Surname = surname;
            Sex = sex;
            AvatarUrl = avatarUrl;
        }
    }
}
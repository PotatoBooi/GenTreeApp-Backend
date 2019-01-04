using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenTreeApp.API.DTOs.Users
{
    public class AddAvatarToUserDto
    {
        public Guid UserId { get; set; }
        public Guid MediaId { get; set; }
    }
}

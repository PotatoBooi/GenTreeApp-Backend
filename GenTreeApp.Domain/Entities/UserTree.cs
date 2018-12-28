using System;
using System.Collections.Generic;
using System.Text;

namespace GenTreeApp.Domain.Entities
{
    public class UserTree
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid TreeId { get; set; }
        public Tree Tree { get; set; }
    }
}

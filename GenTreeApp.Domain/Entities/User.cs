using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Media Avatar { get; set; }
        public ICollection<UserTree> UserTrees { get; set; }
        

        
    }
}
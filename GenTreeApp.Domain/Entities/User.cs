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
        public Guid AvatarId { get; set; }
        public ICollection<Tree> TreeList { get; set; }
        public Media Avatar { get; set; }

        public User()
        {
            TreeList = new HashSet<Tree>();
        }
    }
}
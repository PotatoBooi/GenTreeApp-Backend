using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenTreeApp.Domain.Entities
{
    public class Media
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public MediaType Type { get; set; }
        public string Url { get; set; }
        //navigation property
        public Details Details { get; set; }
        //navigation to avatar owner
        public User User { get; set; }


    }

    public enum MediaType
    {
        [DisplayName("Picture")]
        Picture,
        [DisplayName("Video")]
        Video,
        [DisplayName("Avatar")]
        Avatar
    }
}
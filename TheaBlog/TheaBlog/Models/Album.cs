

namespace TheaBlog.Models
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class Album
    {
        public Album()
        {
            this.Photos = new List<Photo>();
            this.Comments = new List<Comment>();
        }
        [Key]
        public Guid AlbumId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public List<Photo> Photos { get; set; }

        public List<Comment> Comments { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
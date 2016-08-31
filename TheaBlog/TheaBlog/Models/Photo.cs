namespace TheaBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Photo
    {
        public Photo()
        {
            this.Comments = new List<Comment>();
        }

        [Key]
        public Guid PhotoId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public virtual File Image { get; set; }

        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public List<Comment> Comments { get; set; }

        public Guid AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }
    }
}
namespace AlmostTheaBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Photo
    {
        public Photo()
        {
        }

        public Photo(string title, string imagePath, string category, string description)
        {
            this.Title = title;
            this.ImagePath = imagePath;
            this.Category = category;
            this.Description = description;
            this.Comments = new List<Comment>();
            this.Date = DateTime.Now;
        }

        [Key]
        public Guid PhotoId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        public virtual File Image { get; set; }

        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public List<Comment> Comments { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public Guid AlbumId;

        [ForeignKey("AlbumId")]
        public Album Album;

        [NotMapped]
        public byte[] Content { get; set; }
    }
}
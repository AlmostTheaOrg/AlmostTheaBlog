namespace AlmostTheaBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Photo
    {
        public Photo()
        {
        }

        public Photo(string title, string imagePath, string category)
        {
            this.Title = title;
            this.ImagePath = imagePath;
            this.Category = category;
            this.Comments = new List<Comment>();
            this.Date = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public List<Comment> Comments { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
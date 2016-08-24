namespace AlmostTheaBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

       
        public List<Photo> Photos { get; set; }

        [Required]
        public string HeadImagePath { get; set; }

        public List<string> Categories { get; set; }

        public List<Comment> Comments { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
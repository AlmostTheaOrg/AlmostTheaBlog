namespace AlmostTheaBlog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
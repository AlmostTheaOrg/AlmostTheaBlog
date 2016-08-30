namespace TheaBlog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {

        public Comment()
        {
            this.Date = DateTime.Now;
        }

        public Comment(Guid id, string authorId, string description, Guid photoId)
        {
            this.Id = id;
            this.AuthorId = authorId;
            this.Description = description;
            this.Date = DateTime.Now;
            this.PhotoId = photoId;
        }

        [Key]
        public Guid Id { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public Guid PhotoId { get; set; }

        [ForeignKey("PhotoId")]
        public Photo Photo { get; set; }
    }
}
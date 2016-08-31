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

        public int FileId { get; set; }

        [ForeignKey("FileId")]
        public virtual File Image { get; set; }

        public string CategoryName { get; set; }

        // ToDo: Make required.
        [ForeignKey("CategoryName")]       
        public Category Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public List<Comment> Comments { get; set; }

        public Guid AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }
    }
}
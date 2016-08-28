namespace TheaBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid PhotoId { get; set; }

        [ForeignKey("PhotoId")]
        public Photo Photo { get; set; }
    }
}
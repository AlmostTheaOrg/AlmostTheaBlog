namespace TheaBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CommentDetailsViewModel
    {
        public CommentDetailsViewModel(Comment comment)
        {
            this.Description = comment.Description;
            this.Author = comment.Author.Email;
            this.Date = comment.Date;
            this.Id = comment.Id;
        }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public Guid Id { get; set; }
    }
}
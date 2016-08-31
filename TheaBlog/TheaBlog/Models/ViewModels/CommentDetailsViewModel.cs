namespace TheaBlog.Models.ViewModels
{
    using System;

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
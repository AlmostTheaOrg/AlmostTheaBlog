namespace TheaBlog.Models.ViewModels
{
    using System;

    public class CommentEditViewModel
    {
        public CommentEditViewModel()
        {
        }

        public CommentEditViewModel(Comment comment)
        {
            this.Id = Id;
            this.Description = comment.Description;
            this.Date = comment.Date;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
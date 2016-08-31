namespace TheaBlog.Models.ViewModels
{
    using System;

    public class CommentCreateViewModel
    {
        public string AuthorId { get; set; }

        public string Description { get; set; }

        public Guid PhotoId { get; set; }
    }
}
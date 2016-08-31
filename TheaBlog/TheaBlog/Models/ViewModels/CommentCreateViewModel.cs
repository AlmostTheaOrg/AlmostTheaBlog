namespace TheaBlog.Models.ViewModels
{
    using System;

    public class CommentCreateViewModel
    {
        public CommentCreateViewModel()
        {
        }

        public CommentCreateViewModel(Guid entityId)
        {
            this.EntityId = entityId;
        }

        public string AuthorId { get; set; }

        public string Description { get; set; }

        public Guid EntityId { get; set; }
    }
}
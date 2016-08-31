namespace TheaBlog.Models.ViewModels
{
    using System;

    public class CommentCreateViewModel
    {
        public CommentCreateViewModel()
        {
<<<<<<< HEAD
=======

>>>>>>> 297f4383976d4129b04ba46235b39519b3ae7172
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
namespace TheaBlog.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using System.Web.Mvc;

    public class CreatePhotoViewModel
    {
        [Required(ErrorMessage = "Title needed.")]
        [StringLength(100)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "What is a picture without image?")]
        [DisplayName("Image")]
        public HttpPostedFileBase Image { get; set; }

        [Required(ErrorMessage = "Photo must have a category!")]
        [StringLength(100)]
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Photo description might be useful!")]
        [StringLength(500)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Photo must belong to albmum useful!")]
        public string SelectedAlbumId { get; set; }

        public IEnumerable<SelectListItem> Albums { get; set; }
    }
}
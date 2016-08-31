namespace TheaBlog.Controllers
{
    using System.Web.Mvc;
    using TheaBlog.Models;

    public class FilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
    
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}
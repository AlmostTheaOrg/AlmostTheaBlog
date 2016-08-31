namespace TheaBlog.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using TheaBlog.Models;


    public class CategoriesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Photos
                .Include(p => p.Image)
                .Include(p => p.Album)
                .GroupBy(p => p.Category)
                .Select(group => 
                new Category
                {
                    Name = group.Key,
                    Photos = group.ToList()
                }).ToList();

            return View(categories);
        }
    }
}
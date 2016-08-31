namespace TheaBlog.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Net;

    using TheaBlog.Models;


    public class CategoriesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            //var categories = db.Photos
            //    .Include(p => p.Image)
            //    .Include(p => p.Album)
            //    .GroupBy(p => p.Category.Name)
            //    .Select(group => 
            //    new Category
            //    {
            //        Name = group.Key,
            //        Photos = group.ToList()
            //    }).ToList();

            //return View(categories);

            var categories = this.db.Categories.Include(c => c.Photos).ToList();

            return View(categories);
        }

        public ActionResult Details(string name)
        {


            Category category = db.Categories.Include(c => c.Photos).SingleOrDefault(a => a.Name == name);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
    }
}
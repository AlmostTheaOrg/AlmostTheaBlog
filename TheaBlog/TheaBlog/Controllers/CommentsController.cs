namespace TheaBlog.Controllers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Data.Entity;

    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Photo photo)
        {
            var updatedPhoto = db.Photos.Include(p => p.Image).Include(p => p.Album).SingleOrDefault(p => p.PhotoId == photo.PhotoId);

            return View(updatedPhoto);
        }
    }
}
namespace TheaBlog.Controllers
{
    using Models;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var photos = db.Photos.OrderByDescending(p => p.Date).Take(6).ToList();
            return View(photos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
namespace TheaBlog.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using TheaBlog.Models;
    using TheaBlog.Validator;

    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Comments1/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create        
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Title");

            var comment = new CommentCreateViewModel();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var user = UserManager.FindById(User.Identity.GetUserId());
            comment.AuthorId = user.Id;

            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Date,Description,PhotoId,AuthorId")] CommentCreateViewModel commentViewModel)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var user = UserManager.FindById(User.Identity.GetUserId());
            commentViewModel.AuthorId = user.Id;

            var commentValidator = new CommentViewModelValidator();
            var isModelValid = commentValidator.Validate(commentViewModel).IsValid;
            if (isModelValid)
            {
                Guid commentId = Guid.NewGuid();
                Comment comment = new Comment(commentId, commentViewModel.AuthorId, commentViewModel.Description, commentViewModel.PhotoId);

                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Details", "Photos", new { id = comment.PhotoId });
            }

            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Title", commentViewModel.PhotoId);
            return View(commentViewModel);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Include(m => m.Author).SingleOrDefault(m => m.Id == id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Title", comment.PhotoId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Description,PhotoId,AuthorId")] Comment comment)
        {

            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Title", comment.PhotoId);
            return View(comment);
        }

        // GET: Comments1/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // POST: Comments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Comment comment = db.Comments.Find(id);

            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}

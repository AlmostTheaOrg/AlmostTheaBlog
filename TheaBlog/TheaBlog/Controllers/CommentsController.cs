namespace TheaBlog.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TheaBlog.Models;
    using TheaBlog.Models.ViewModels;
    using TheaBlog.Validators;

    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Comments/Details/5
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
        public ActionResult Create([Bind(Include = "AuthorId,Description,EntityId")] CommentCreateViewModel commentViewModel)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var user = UserManager.FindById(User.Identity.GetUserId());
            commentViewModel.AuthorId = user.Id;

            var commentValidator = new CommentViewModelValidator();
            var isModelValid = commentValidator.Validate(commentViewModel).IsValid;
            if (isModelValid)
            {
                Guid commentId = Guid.NewGuid();
                Comment comment = new Comment(commentId, commentViewModel.AuthorId, commentViewModel.Description, commentViewModel.EntityId);

                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Details", "Photos", new { id = comment.PhotoId });
            }

            return View(commentViewModel);
        }

        // GET: Comments/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.SingleOrDefault(m => m.Id == id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            CommentEditViewModel commentEditVM = new CommentEditViewModel(comment);

            // ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Title", comment.PhotoId);
            return View(commentEditVM);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Description,AuthorId")] CommentEditViewModel commentVM)
        {
            var commentVMValidator = new CommentEditViewModelValidator();
            var result = commentVMValidator.Validate(commentVM);
            var isVMValid = result.IsValid;

            if (isVMValid)
            {
                var comment = this.db.Comments.Include(m => m.Author).SingleOrDefault(m => m.Id == commentVM.Id);
                comment.Description = commentVM.Description;
                comment.Date = commentVM.Date;

                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", "Photos", new { id = comment.PhotoId });
            }

            // ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Title", comment.PhotoId);
            return View(commentVM);
        }

        // GET: Comments/Delete/5
        [Authorize]
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

        // POST: Comments/Delete/5
        [Authorize]
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

namespace TheaBlog.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Drawing;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using TheaBlog.Models;
    using TheaBlog.Models.ViewModels;

    using Validators;

    public class PhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Photos
        public ActionResult Index()
        {

            return View(db.Photos.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = db.Photos.Include(p => p.Image).Include(p => p.Album).Include(p => p.Comments).Include(p => p.Comments.Select(c => c.Author)).SingleOrDefault(p => p.PhotoId == id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return View(photo);
        }

        // GET: Photos/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            CreatePhotoViewModel photoViewModel = new CreatePhotoViewModel()
            {
                Albums = GetAllAlbums()
            };

            return View(photoViewModel);
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Title,CategoryName,Description,SelectedAlbumId")] CreatePhotoViewModel photoViewModel, HttpPostedFileBase uploadedImage)
        {
            photoViewModel.Image = uploadedImage;
            PhotoViewModelValidator validator = new PhotoViewModelValidator();
            var result = validator.Validate(photoViewModel);
            bool isValid = result.IsValid;

            if (isValid)
            {
                File file = null;
                Guid photoId = Guid.NewGuid();
                if (uploadedImage != null && uploadedImage.ContentLength > 0)
                {
                    file = new File
                    {
                        FileName = System.IO.Path.GetFileName(uploadedImage.FileName),
                        ContentType = uploadedImage.ContentType,
                    };

                    file.Content = GetFileContent(uploadedImage);
                    file.FileLength = uploadedImage.ContentLength;
                }

                Photo photo = new Photo();
                photo.Title = photoViewModel.Title;
                Category category = null;
                if (db.Categories.Any(c => c.Name == photoViewModel.CategoryName))
                {
                    category = db.Categories.SingleOrDefault( c => c.Name == photoViewModel.CategoryName);
                }
                else
                {
                    category = new Category(photoViewModel.CategoryName);
                    //db.Categories.Add(category);
                }

                photo.Category = category;
                photo.Description = photoViewModel.Description;
                photo.Image = file;
                photo.FileId = photo.Image.FileId;
                photo.Date = DateTime.Now;
                photo.PhotoId = photoId;

                Album album = db.Albums.SingleOrDefault(a => a.AlbumId.ToString() == photoViewModel.SelectedAlbumId);
                photo.AlbumId = album.AlbumId;
                photo.Album = album;

                album.Photos.Add(photo);

                db.Photos.Add(photo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(photoViewModel);
        }

        // GET: Photos/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = db.Photos.Include(p => p.Image).Include(p => p.Album).SingleOrDefault(p => p.PhotoId == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "PhotoId,Title,CategoryName,AlbumId,FileId,Description,Date")] Photo photo, HttpPostedFileBase uploadedImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isModelValid = uploadedImage != null && uploadedImage.ContentLength > 0;
                    if (isModelValid)
                    {
                        if (photo.Image != null)
                        {
                            db.Files.Remove(photo.Image);
                        }

                        File image = this.db.Files.SingleOrDefault(f => f.FileId == photo.FileId);
                        image.Content = GetFileContent(uploadedImage);
                        image.FileLength = uploadedImage.ContentLength;
                    }

                    if (!db.Categories.Any(c => c.Name == photo.CategoryName))
                    {
                        db.Categories.Add(new Category(photo.CategoryName));
                    }

                    Album album = db.Albums.Find(photo.AlbumId);
                    photo.Album = album;

                    db.Entry(photo).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(photo);
        }

        // GET: Photos/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Include(path => path.Image).SingleOrDefault(p => p.PhotoId == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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

        private static byte[] GetFileContent(HttpPostedFileBase uploadedImage)
        {
            Image image = Image.FromStream(uploadedImage.InputStream, true, true);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            return memoryStream.ToArray();
        }

        private IEnumerable<SelectListItem> GetAllAlbums()
        {
            List<SelectListItem> slItem = new List<SelectListItem>();
            foreach (var album in db.Albums)
            {
                slItem.Add(new SelectListItem()
                {
                    Selected = album.AlbumId.ToString() == "Active",
                    Text = album.Title,
                    Value = album.AlbumId.ToString()
                });
            }

            return slItem;
        }
    }
}

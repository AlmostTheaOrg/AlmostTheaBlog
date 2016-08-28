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
    using Validator;
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
            Photo photo = db.Photos.Include(path => path.Image).SingleOrDefault(p => p.PhotoId == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
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
        public ActionResult Create([Bind(Include = "Title,Category,Description,SelectedAlbumId")] CreatePhotoViewModel photoViewModel, HttpPostedFileBase uploadedImage)
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
                        PhotoId = photoId
                    };

                    SaveImage(uploadedImage, file);
                }

                Photo photo = new Photo();
                photo.Title = photoViewModel.Title;
                photo.Category = photoViewModel.Category;
                photo.Description = photoViewModel.Description;
                photo.Image = file;
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
        public ActionResult Edit([Bind(Include = "PhotoId,Title,Category,AlbumId,Description,Date")] Photo photo, HttpPostedFileBase uploadedImage)
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

                        var newImage = new File
                        {
                            FileName = System.IO.Path.GetFileName(uploadedImage.FileName),
                            ContentType = uploadedImage.ContentType
                        };

                        SaveImage(uploadedImage, newImage);
                        photo.Image = newImage;
                    }
                    else
                    {
                        File oldImage = this.db.Files.SingleOrDefault(f => f.PhotoId == photo.PhotoId);
                        photo.Image = oldImage;
                    }
                    Album album = db.Albums.Find(photo.AlbumId);
                    photo.Album = album;
                    db.Entry(photo).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(photo);
        }

        // GET: Photos/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        private static void SaveImage(HttpPostedFileBase uploadedImage, File file)
        {
            Image image = Image.FromStream(uploadedImage.InputStream, true, true);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            file.Content = memoryStream.ToArray();
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

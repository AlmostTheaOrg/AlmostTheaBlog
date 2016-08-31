namespace TheaBlog.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TheaBlog.Models.Album> Albums { get; set; }

        public System.Data.Entity.DbSet<TheaBlog.Models.Photo> Photos { get; set; }

        public System.Data.Entity.DbSet<TheaBlog.Models.File> Files { get; set; }

        public System.Data.Entity.DbSet<TheaBlog.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<TheaBlog.Models.Category> Categories { get; set; }

    }
}
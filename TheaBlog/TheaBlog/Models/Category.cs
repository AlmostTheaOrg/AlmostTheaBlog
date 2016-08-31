namespace TheaBlog.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Photos = new List<Photo>();
        }

        public Category(string name)
        {
            this.Name = name;
            this.Photos = new List<Photo>();
        }

        [Key]
        public string Name { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
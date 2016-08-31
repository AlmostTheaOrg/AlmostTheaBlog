namespace TheaBlog.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public string Name { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
namespace TheaBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Category
    {
        public string Name { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
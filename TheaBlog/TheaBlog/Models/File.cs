namespace TheaBlog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class File
    {
        [Key]
        public int FileId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public long FileLength { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
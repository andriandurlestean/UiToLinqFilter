using System;

namespace DBConsoleNF.DataAccess.Models
{
    public class DocumentInfo
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public int Pages { get; set; }

        public virtual User Owner { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
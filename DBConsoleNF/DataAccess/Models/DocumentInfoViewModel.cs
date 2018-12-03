using System;

namespace DBConsoleNF.DataAccess.Models
{
    public class DocumentInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DocumentStatus { get; set; }

        public decimal Price { get; set; }

        public int Pages { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

    }
}
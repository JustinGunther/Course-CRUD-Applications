using System;

namespace SQLFundamentals.DataAccess.Models
{
    public class DocumentModel
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime LastUpdateDate { get; set; }
        public string Location { get; set; } = "";
    }
}
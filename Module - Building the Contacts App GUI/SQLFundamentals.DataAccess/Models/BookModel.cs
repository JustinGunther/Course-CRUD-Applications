namespace SQLFundamentals.DataAccess.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; } = "";
        public string Author { get; set; } = "";
        public int YearPublished { get; set; }
        public bool HaveRead { get; set; }
    }
}
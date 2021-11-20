namespace SQLFundamentals.DataAccess.Models
{
    public class MovieModel
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; } = "";
        public int Year { get; set; }
        public string Director { get; set; } = "";
        public string LeadActor { get; set; } = "";
        public int MyRating { get; set; }
    }
}
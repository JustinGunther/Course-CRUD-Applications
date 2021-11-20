namespace SQLFundamentals.DataAccess.Models
{
    public class PetModel
    {
        public int PetID { get; set; }
        public string PetName { get; set; } = "";
        public string Species { get; set; } = "";
        public int BirthYear { get; set; }
        public string Food { get; set; } = "";
        public string Veterinarian { get; set; } = "";
    }
}
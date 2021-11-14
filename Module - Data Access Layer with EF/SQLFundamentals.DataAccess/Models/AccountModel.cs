namespace SQLFundamentals.DataAccess.Models
{
    public class AccountModel
    {
        public int AccountID { get; set; }
        public string BankName { get; set; } = "";
        public int AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
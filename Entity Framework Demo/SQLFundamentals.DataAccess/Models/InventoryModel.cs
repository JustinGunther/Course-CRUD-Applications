namespace SQLFundamentals.DataAccess.Models
{
    public class InventoryModel
    {
        public int InventoryID { get; set; }
        public string Item { get; set; } = "";
        public string Brand { get; set; } = "";
        public int CountOnHand { get; set; }
        public string Location { get; set; } = "";
        public decimal Cost { get; set; }
    }
}
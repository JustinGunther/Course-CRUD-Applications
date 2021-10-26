using System;

namespace SQLFundamentals.DataAccess.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public string OrderStatus { get; set; } = "";
        public int OrderQuantity { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderDatePlaced { get; set; }
        public DateTime OrderDateShipped { get; set; }
    }
}
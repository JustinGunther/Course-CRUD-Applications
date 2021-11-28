using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CRUDApps.DataAccess.EF.Models
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public string Item { get; set; }
        public string Brand { get; set; }
        public int CountOnHand { get; set; }
        public string Location { get; set; }
        public decimal Cost { get; set; }
    }
}

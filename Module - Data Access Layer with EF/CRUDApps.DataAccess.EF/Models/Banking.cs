using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CRUDApps.DataAccess.EF.Models
{
    public partial class Banking
    {
        public int AccountId { get; set; }
        public string BankName { get; set; }
        public int AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
    }
}

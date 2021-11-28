using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CRUDApps.DataAccess.EF.Models
{
    public partial class Movies
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public short Year { get; set; }
        public string Director { get; set; }
        public string LeadActor { get; set; }
        public byte MyRating { get; set; }
    }
}

using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CRUDApps.DataAccess.EF.Models
{
    public partial class TeamRecords
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Location { get; set; }
        public string _2017season { get; set; }
        public string _2018season { get; set; }
        public string _2019season { get; set; }
        public string _2020season { get; set; }
        public string _2021season { get; set; }
    }
}

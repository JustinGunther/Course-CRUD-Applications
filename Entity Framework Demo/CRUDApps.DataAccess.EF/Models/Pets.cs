using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CRUDApps.DataAccess.EF.Models
{
    public partial class Pets
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public short BirthYear { get; set; }
        public string Food { get; set; }
        public string Veterinarian { get; set; }
    }
}

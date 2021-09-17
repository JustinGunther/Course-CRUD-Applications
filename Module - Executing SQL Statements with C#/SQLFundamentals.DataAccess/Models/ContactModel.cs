using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFundamentals.DataAccess.Models
{
    public class ContactModel
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMailAddress { get; set; }
        public DateTime DateInserted { get; set; }
    }
}

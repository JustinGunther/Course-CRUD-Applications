using CRUDApps.DataAccess.EF;
using CRUDApps.DataAccess.EF.Controllers;
using CRUDApps.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCContactsApp.Models
{
    public class ContactsViewModel
    {
        private ISQLFundamentalsConfigManager _configuration;

        public List<Contacts> ContactList { get; set; }

        public Contacts CurrentContact { get; set; }        

        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; }

        public ContactsViewModel(ISQLFundamentalsConfigManager configuration)
        {
            _configuration = configuration;
            ContactList = GetAllContacts();
            CurrentContact = ContactList.FirstOrDefault();
        }

        public ContactsViewModel(ISQLFundamentalsConfigManager configuration, int contactId)
        {
            _configuration = configuration;
            ContactList = new List<Contacts>();

            if (contactId > 0)
            {
                CurrentContact = GetContact(contactId);
            }
            else
            {
                CurrentContact = new Contacts();
            }
        }

        public List<Contacts> GetAllContacts()
        {
            return ContactController.GetAllContacts(_configuration);
        }

        public Contacts GetContact(int contactId)
        {
            return ContactController.GetContactByID(contactId, _configuration);
        }
    }
}

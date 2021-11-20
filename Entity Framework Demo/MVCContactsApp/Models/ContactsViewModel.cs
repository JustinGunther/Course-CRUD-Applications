using SQLFundamentals.DataAccess;
using SQLFundamentals.DataAccess.Controllers;
using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCContactsApp.Models
{
    public class ContactsViewModel
    {
        private ISQLFundamentalsConfigManager _configuration;

        public List<ContactModel> ContactList { get; set; }

        public ContactModel CurrentContact { get; set; }        

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
            ContactList = new List<ContactModel>();

            if (contactId > 0)
            {
                CurrentContact = GetContact(contactId);
            }
            else
            {
                CurrentContact = new ContactModel();
            }
        }

        public List<ContactModel> GetAllContacts()
        {
            return ContactController.GetAllContacts(_configuration);
        }

        public ContactModel GetContact(int contactId)
        {
            return ContactController.GetContactByID(contactId, _configuration);
        }
    }
}

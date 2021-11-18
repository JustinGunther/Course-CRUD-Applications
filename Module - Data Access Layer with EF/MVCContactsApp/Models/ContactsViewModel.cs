using CRUDApps.DataAccess.EF.Configuration;
using CRUDApps.DataAccess.EF.Repositories;
using CRUDApps.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCContactsApp.Models
{
    public class ContactsViewModel
    {
        private ContactRepository _repo;

        public List<Contacts> ContactList { get; set; }

        public Contacts CurrentContact { get; set; }        

        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; }

        public ContactsViewModel(ISQLFundamentalsConfigManager configuration)
        {
            _repo = new ContactRepository(configuration);
            ContactList = GetAllContacts();
            CurrentContact = ContactList.FirstOrDefault();
        }

        public ContactsViewModel(ISQLFundamentalsConfigManager configuration, int contactId)
        {
            _repo = new ContactRepository(configuration);
            ContactList = GetAllContacts();

            if (contactId > 0)
            {
                CurrentContact = GetContact(contactId);
            }
            else
            {
                CurrentContact = new Contacts();
            }
        }

        public void SaveContact(int contactID, string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            if (contactID > 0)
            {
                _repo.UpdateContact(contactID, firstName, lastName, phoneNumber, emailAddress);
            }
            else
            {
                contactID = _repo.CreateContact(firstName, lastName, phoneNumber, emailAddress);
            }

            ContactList = GetAllContacts();
            CurrentContact = GetContact(contactID);
        }

        public void RemoveContact(int contactID)
        {
            _repo.DeleteContact(contactID);
            ContactList = GetAllContacts();
            CurrentContact = ContactList.FirstOrDefault();
        }

        public List<Contacts> GetAllContacts()
        {
            return _repo.GetAllContacts();
        }

        public Contacts GetContact(int contactId)
        {
            return _repo.GetContactByID(contactId);
        }
    }
}

using CRUDApps.DataAccess.EF.Context;
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

        public ContactsViewModel(SQLFundamentalsContext context)
        {
            _repo = new ContactRepository(context);
            ContactList = GetAllContacts();
            CurrentContact = ContactList.FirstOrDefault();
        }

        public ContactsViewModel(SQLFundamentalsContext context, int contactId)
        {
            _repo = new ContactRepository(context);
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

        public void SaveContact(Contacts contact)
        {
            if (contact.ContactId > 0)
            {
                _repo.Update(contact);
            }
            else
            {
                contact.ContactId = _repo.Create(contact);
            }

            ContactList = GetAllContacts();
            CurrentContact = GetContact(contact.ContactId);
        }

        public void RemoveContact(int contactID)
        {
            _repo.Delete(contactID);
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

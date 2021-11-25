using CRUDApps.DataAccess.EF.Models;
using CRUDApps.DataAccess.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUDApps.DataAccess.EF.Repositories
{
    public class ContactRepository
    {
        private SQLFundamentalsContext _dbContext;

        public ContactRepository(SQLFundamentalsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateContact(Contacts contact)
        {

            _dbContext.Add(contact);
            _dbContext.SaveChanges();

            return contact.ContactId;
        }

        public int UpdateContact(Contacts contact)
        {
            Contacts existingContact = _dbContext.Contacts.Find(contact.ContactId);

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.EmailAddress = contact.EmailAddress;

            _dbContext.SaveChanges();

            return existingContact.ContactId;
        }

        public bool DeleteContact(int contactID)
        {
            Contacts contact = _dbContext.Contacts.Find(contactID);
            _dbContext.Remove(contact);
            _dbContext.SaveChanges();

            return true;
        }

        public List<Contacts> GetAllContacts()
        {
            List<Contacts> contactsList = _dbContext.Contacts.ToList();

            return contactsList;
        }

        public Contacts GetContactByID(int contactID)
        {
            Contacts contact = _dbContext.Contacts.Find(contactID);

            return contact;
        }
    }
}

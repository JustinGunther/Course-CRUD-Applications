using CRUDApps.DataAccess.EF.Context;
using CRUDApps.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDApps.DataAccess.EF.Repositories
{
    public class ContactRepository
    {
        private SQLFundamentalsContext _dbContext;

        public ContactRepository(SQLFundamentalsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Contacts contact)
        {
            _dbContext.Add(contact);
            _dbContext.SaveChanges();

            return contact.ContactId;
        }

        public int Update(Contacts contact)
        {
            Contacts existingContact = _dbContext.Contacts.Find(contact.ContactId);

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.EmailAddress = contact.EmailAddress;

            _dbContext.SaveChanges();

            return existingContact.ContactId;
        }

        public bool Delete(int contactID)
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

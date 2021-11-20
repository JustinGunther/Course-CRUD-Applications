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

        public int CreateContact(string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            Contacts contact = new Contacts()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                EmailAddress = emailAddress
            };

            _dbContext.Add(contact);
            _dbContext.SaveChanges();

            return contact.ContactId;
        }

        public int UpdateContact(int contactID, string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            Contacts contact = _dbContext.Contacts.Find(contactID);
            contact.FirstName = firstName;
            contact.LastName = lastName;
            contact.PhoneNumber = phoneNumber;
            contact.EmailAddress = emailAddress;

            _dbContext.SaveChanges();
            contactID = contact.ContactId;

            return contactID;
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
            List<Contacts> contactsList = _dbContext.Contacts.OrderByDescending(x => x.ContactId).ToList();

            return contactsList;
        }

        public Contacts GetContactByID(int contactID)
        {
            Contacts contact = _dbContext.Contacts.Find(contactID);

            return contact;
        }
    }
}

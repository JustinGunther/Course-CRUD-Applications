using CRUDApps.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUDApps.DataAccess.EF.Controllers
{
    public class ContactController
    {
        public static int CreateContact(string firstName, string lastName, string phoneNumber, string emailAddress, ISQLFundamentalsConfigManager configManager)
        {
            int contactId = 0;

            using (SQLFundamentalsContext context = new SQLFundamentalsContext(configManager))
            {
                Contacts contact = new Contacts()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    EmailAddress = emailAddress
                };

                context.Add(contact);
                context.SaveChanges();

                contactId = contact.ContactId;
            }

            return contactId;
        }

        public static int UpdateContact(int contactID, string firstName, string lastName, string phoneNumber, string emailAddress, ISQLFundamentalsConfigManager configManager)
        {
            using (SQLFundamentalsContext context = new SQLFundamentalsContext(configManager))
            {
                Contacts contact = context.Contacts.Find(contactID);
                contact.FirstName = firstName;
                contact.LastName = lastName;
                contact.PhoneNumber = phoneNumber;
                contact.EmailAddress = emailAddress;

                context.SaveChanges();
                contactID = contact.ContactId;
            }

            return contactID;
        }

        public static bool DeleteContact(int contactID, ISQLFundamentalsConfigManager configManager)
        {
            using (SQLFundamentalsContext context = new SQLFundamentalsContext(configManager))
            {
                Contacts contact = context.Contacts.Find(contactID);
                context.Remove(contact);
                context.SaveChanges();
            }

            return true;
        }

        public static List<Contacts> GetAllContacts(ISQLFundamentalsConfigManager configManager)
        {
            List<Contacts> contactsList = new List<Contacts>();

            using (SQLFundamentalsContext context = new SQLFundamentalsContext(configManager))
            {
                contactsList = context.Contacts.OrderByDescending(x => x.ContactId).ToList();
            }

            return contactsList;
        }

        public static Contacts GetContactByID(int contactID, ISQLFundamentalsConfigManager configManager)
        {
            Contacts contact = new Contacts();

            using (SQLFundamentalsContext context = new SQLFundamentalsContext(configManager))
            {
                contact = context.Contacts.Find(contactID);
            }

            return contact;
        }
    }
}

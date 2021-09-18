using SQLFundamentals.DataAccess.Controllers;
using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace SQLFundamentals.UI.CRUDConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ChooseOption();
        }

        static void ChooseOption()
        {
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine("Choose a Option:");
            Console.WriteLine("\t1 - Press 1 to create a Contact");
            Console.WriteLine("\t2 - Press 2 to update a Contact");
            Console.WriteLine("\t3 - Press 3 to delete a Contact");
            Console.WriteLine("\t4 - Press 4 to get all Contacts");
            Console.WriteLine("\t5 - Press 5 to get a Contact by ID");
            Console.WriteLine("\t(6) - Close Appliction");
            Console.Write("Your choice: ");
            string optionNo = Console.ReadLine();
            switch (optionNo)
            {
                case "1":
                    CreateContact();
                    break;
                case "2":
                    UpdateContact();
                    break;
                case "3":
                    DeleteContact();
                    break;
                case "4":
                    GetAllContacts();
                    break;
                case "5":
                    GetContactByID();
                    break;
                default:
                    break;
            }
        }

        private static void CreateContact()
        {
            Console.WriteLine("\t1 - Press 1 to enter the contact's details");
            Console.WriteLine("\t2 - Press 2 to insert a contact with the default data");
            Console.WriteLine("\t3 - Press 3 to back to the main menu");

            string optionNo = Console.ReadLine();
            switch (optionNo)
            {
                case "1":
                    Console.WriteLine("Contact's details:");
                    Console.Write("\nFirst Name:");
                    string firstName = Console.ReadLine();
                    Console.Write("\nLast Name:");
                    string lastName = Console.ReadLine();
                    Console.Write("\nPhone Number:");
                    string phoneNumber = Console.ReadLine();
                    Console.Write("\nE-Mail Address:");
                    string emailAddress = Console.ReadLine();
                    try
                    {
                        int createdContactId = ContactController.CreateContact(firstName, lastName, phoneNumber, emailAddress);
                        Console.WriteLine("Contact has been added successfully, ContactID = " + createdContactId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while trying to create new Contact");
                        Console.WriteLine(ex.Message);
                    }
                    ChooseOption();
                    break;
                case "2":

                    string defaultFirstName = "John";
                    string defaultLastName = "Smith";
                    string defaultPhoneNumber = "000-0000-0000";
                    string defaultEMailAddress = "john.smith";
                    try
                    {
                        int createdContactId = ContactController.CreateContact(defaultFirstName, defaultLastName, defaultPhoneNumber, defaultEMailAddress);
                        Console.WriteLine("Contact with the default data has been added successfully, ContactID = " + createdContactId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while trying to create new Contact with the default");
                        Console.WriteLine(ex.Message);
                    }
                    ChooseOption();
                    break;
                case "3":
                    ChooseOption();
                    break;
                default:
                    ChooseOption();
                    break;
            }
        }
        private static void UpdateContact()
        {
            Console.WriteLine("\t1 - Press 1 to enter the contact's details");
            Console.WriteLine("\t2 - Press 2 to back to the main menu");

            string optionNo = Console.ReadLine();
            switch (optionNo)
            {
                case "1":
                    Console.WriteLine("Contact's details:");
                    Console.Write("\nContact ID:");
                    int contactID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nFirst Name:");
                    string firstName = Console.ReadLine();
                    Console.Write("\nLast Name:");
                    string lastName = Console.ReadLine();
                    Console.Write("\nPhone Number:");
                    string phoneNumber = Console.ReadLine();
                    Console.Write("\nE-Mail Address:");
                    string emailAddress = Console.ReadLine();
                    try
                    {
                        int updatedContactId = ContactController.UpdateContact(contactID, firstName, lastName, phoneNumber, emailAddress);
                        Console.WriteLine("Contact has been updated successfully, ContactID = " + updatedContactId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while trying to update Contact");
                        Console.WriteLine(ex.Message);
                    }
                    ChooseOption();
                    break;
                case "2":
                    ChooseOption();
                    break;
                default:
                    ChooseOption();
                    break;
            }
        }
        private static void DeleteContact()
        {
            Console.WriteLine("\t1 - Press 1 to enter the contact's details");
            Console.WriteLine("\t2 - Press 2 to back to the main menu");

            string optionNo = Console.ReadLine();
            switch (optionNo)
            {
                case "1":
                    Console.Write("\nEnter Contact ID:");
                    int contactID = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        bool deleteResult = ContactController.DeleteContact(contactID);
                        Console.WriteLine("Contact has been deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while trying to update Contact");
                        Console.WriteLine(ex.Message);
                    }

                    ChooseOption();
                    break;
                case "2":
                    ChooseOption();
                    break;
                default:
                    ChooseOption();
                    break;
            }
        }

        private static void GetAllContacts()
        {
            try
            {
                List<ContactModel> contactModels = ContactController.GetAllContacts();
                Console.WriteLine(
                    String.Format("|{0,10}|{1,15}|{2,15}|{3,20}|{4,25}|", "Contact ID", "First Name", "Last Name", "Phone Number", "Email Address")
                    );

                foreach (var item in contactModels)
                {
                    Console.WriteLine(
                       String.Format("|{0,10}|{1,15}|{2,15}|{3,20}|{4,25}|", item.ContactID, item.FirstName, item.LastName, item.PhoneNumber, item.EMailAddress)
                       );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while trying to retrieving Contacts");
                Console.WriteLine(ex.Message);
            }
            ChooseOption();
        }

        private static void GetContactByID()
        {
            try
            {
                Console.Write("\nEnter Contact ID:");
                int contactID = Convert.ToInt32(Console.ReadLine());
                ContactModel contactModel = ContactController.GetContactByID(contactID);
                Console.WriteLine(
                    String.Format("|{0,10}|{1,15}|{2,15}|{3,20}|{4,25}|", "Contact ID", "First Name", "Last Name", "Phone Number", "Email Address")
                    );

                Console.WriteLine(
                    String.Format("|{0,10}|{1,15}|{2,15}|{3,20}|{4,25}|",
                    contactModel.ContactID, contactModel.FirstName, contactModel.LastName,
                    contactModel.PhoneNumber, contactModel.EMailAddress)
                   );

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while trying to retrieving a Contact");
                Console.WriteLine(ex.Message);
            }

            ChooseOption();
        }
    }
}

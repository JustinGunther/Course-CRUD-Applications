using SQLFundamentals.DataAccess.Controllers;
using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace SQLFundamentals.UI.CRUDConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("CRUD Operations Demo Application");
            ChooseOption();
            Console.WriteLine("test");
        }

        private static void ChooseOption()
        {
            bool validOption = false;

            while (!validOption)
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("Choose an Option:");
                Console.WriteLine("\t1 - Press 1 to create a Contact");
                Console.WriteLine("\t2 - Press 2 to update a Contact");
                Console.WriteLine("\t3 - Press 3 to delete a Contact");
                Console.WriteLine("\t4 - Press 4 to get all Contacts");
                Console.WriteLine("\t5 - Press 5 to get a Contact by ID");
                Console.WriteLine("\t6 - Close Appliction");
                Console.Write("Your choice: ");
                string optionNo = Console.ReadLine();
                switch (optionNo)
                {
                    case "1":
                        validOption = true;
                        CreateContact();
                        break;

                    case "2":
                        validOption = true;
                        UpdateContact();
                        break;

                    case "3":
                        validOption = true;
                        DeleteContact();
                        break;

                    case "4":
                        validOption = true;
                        GetAllContacts();
                        break;

                    case "5":
                        validOption = true;
                        GetContactByID();
                        break;

                    case "6":
                        validOption = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Entry.");
                        Console.WriteLine();
                        validOption = false;
                        break;
                }
            }
        }

        private static void CreateContact()
        {
            bool validOption = false;

            while (!validOption)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine("\t1 - Press 1 to enter the contact's details");
                Console.WriteLine("\t2 - Press 2 to insert a contact with the default data");
                Console.WriteLine("\t3 - Press 3 to back to the main menu");
                Console.Write("Your choice: ");
                string optionNo = Console.ReadLine();
                Console.WriteLine();
                switch (optionNo)
                {
                    case "1":
                        validOption = true;
                        Console.WriteLine("Contact's details:");
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Phone Number: ");
                        string phoneNumber = Console.ReadLine();
                        Console.Write("E-Mail Address: ");
                        string emailAddress = Console.ReadLine();
                        Console.WriteLine();
                        try
                        {
                            int createdContactId = ContactController.CreateContact(firstName, lastName, phoneNumber, emailAddress);
                            Console.WriteLine("Contact has been added successfully, ContactID = " + createdContactId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while trying to create new Contact: {ex.Message}");
                        }
                        ChooseOption();
                        break;

                    case "2":
                        validOption = true;
                        string defaultFirstName = "John";
                        string defaultLastName = "Smith";
                        string defaultPhoneNumber = "000-000-0000";
                        string defaultEMailAddress = "john.smith@yahoo.com";
                        try
                        {
                            int createdContactId = ContactController.CreateContact(defaultFirstName, defaultLastName, defaultPhoneNumber, defaultEMailAddress);
                            Console.WriteLine("Contact with the default data has been added successfully, ContactID = " + createdContactId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while trying to create new Contact: {ex.Message}");
                        }
                        ChooseOption();
                        break;

                    case "3":
                        validOption = true;
                        ChooseOption();
                        break;

                    default:
                        Console.WriteLine("Invalid Entry.");
                        Console.WriteLine();
                        validOption = false;
                        break;
                }
            }
        }

        private static void UpdateContact()
        {
            bool validOption = false;

            while (!validOption)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine("\t1 - Press 1 to enter the contact's details");
                Console.WriteLine("\t2 - Press 2 to back to the main menu");
                Console.Write("Your choice: ");
                string optionNo = Console.ReadLine();
                Console.WriteLine();
                switch (optionNo)
                {
                    case "1":
                        validOption = true;
                        Console.WriteLine("Contact's details:");
                        Console.Write("Contact ID: ");
                        int contactID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Phone Number: ");
                        string phoneNumber = Console.ReadLine();
                        Console.Write("E-Mail Address: ");
                        string emailAddress = Console.ReadLine();
                        Console.WriteLine();
                        try
                        {
                            int updatedContactId = ContactController.UpdateContact(contactID, firstName, lastName, phoneNumber, emailAddress);
                            Console.WriteLine("Contact has been updated successfully, ContactID = " + updatedContactId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while trying to update Contact: {ex.Message}");
                        }
                        ChooseOption();
                        break;

                    case "2":
                        validOption = true;
                        ChooseOption();
                        break;

                    default:
                        Console.WriteLine("Invalid Entry.");
                        Console.WriteLine();
                        validOption = false;
                        break;
                }
            }
        }

        private static void DeleteContact()
        {
            bool validOption = false;

            while (!validOption)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine("\t1 - Press 1 to enter the contact's details");
                Console.WriteLine("\t2 - Press 2 to back to the main menu");
                Console.Write("Your choice: ");
                string optionNo = Console.ReadLine();
                Console.WriteLine();
                switch (optionNo)
                {
                    case "1":
                        validOption = true;
                        Console.Write("Enter Contact ID: ");
                        int contactID = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            bool deleteResult = ContactController.DeleteContact(contactID);
                            Console.WriteLine("Contact has been deleted successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while trying to update Contact: {ex.Message}");
                        }

                        ChooseOption();
                        break;

                    case "2":
                        validOption = true;
                        ChooseOption();
                        break;

                    default:
                        Console.WriteLine("Invalid Entry.");
                        Console.WriteLine();
                        validOption = false;
                        break;
                }
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
                Console.WriteLine($"Error while trying to retrieve Contacts: {ex.Message}");
            }
            ChooseOption();
        }

        private static void GetContactByID()
        {
            try
            {
                Console.Write("\nEnter Contact ID: ");
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
                Console.WriteLine($"Error while trying to retrieve a Contact: {ex.Message}");
            }

            ChooseOption();
        }
    }
}
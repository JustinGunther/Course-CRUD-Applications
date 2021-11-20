using Microsoft.AspNetCore.Mvc;
using MVCContactsApp.Models;
using SQLFundamentals.DataAccess;
using SQLFundamentals.DataAccess.Models;
using SQLFundamentals.DataAccess.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCContactsApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ISQLFundamentalsConfigManager _configuration;

        public ContactsController(ISQLFundamentalsConfigManager configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ContactsViewModel model = new ContactsViewModel(_configuration);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int contactID, string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            if (contactID > 0)
            {
                ContactController.UpdateContact(contactID, firstName, lastName, phoneNumber, emailAddress, _configuration);
            }
            else
            {
                ContactController.CreateContact(firstName, lastName, phoneNumber, emailAddress, _configuration);
            }

            ContactsViewModel model = new ContactsViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Contact has been saved successfully";

            return View(model);
        }

        public IActionResult Update(int id)
        {
            ContactsViewModel model = new ContactsViewModel(_configuration, id);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                ContactController.DeleteContact(id, _configuration);
            }

            ContactsViewModel model = new ContactsViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Contact has been deleted successfully";
            return View("Index", model);
        }
    }
}

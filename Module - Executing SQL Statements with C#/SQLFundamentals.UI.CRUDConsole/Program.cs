using SQLFundamentals.DataAccess.Controllers;
using SQLFundamentals.DataAccess.Models;
using SQLFundamentals.UI.CRUDConsole.Tools;
using System;
using System.Collections.Generic;

namespace SQLFundamentals.UI.CRUDConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Demo().ChooseOption();
        }
    }
}
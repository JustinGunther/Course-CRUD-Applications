using CRUDApps.DataAccess.EF.Context;
using CRUDApps.DataAccess.EF.Repositories;
using CRUDApps.DataAccess.EF.Models;
using NSubstitute;
using FluentAssertions;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CRUDApps.DataAccess.EF.Tests
{
    public class ContactRepositoryTests
    {              

        [Theory]
        [InlineData(1)]
        public void GetContactByID_ShouldReturnContactIfExists(int contactId)
        {
            var contactsData = new List<Contacts>
            {
                new Contacts(1, "John", "Smith", "(407) 555-1212", "jsmith@gmail.com"),
                new Contacts(2, "Dave", "Johnson", "(407) 555-1212", "djohnson@gmail.com"),
                new Contacts(3, "Mary", "Miller", "(407) 555-1212", "mmiller@gmail.com")
            }.AsQueryable();

            var contactsList = Substitute.For<DbSet<Contacts>, IQueryable<Contacts>>();
            contactsList.Find(Arg.Any<int>()).Returns(x => contactsData.FirstOrDefault(c => c.ContactId == (int)((object[])x[0])[0]));
            contactsList.As<IQueryable<Contacts>>().Provider.Returns(contactsData.Provider);
            contactsList.As<IQueryable<Contacts>>().Expression.Returns(contactsData.Expression);
            contactsList.As<IQueryable<Contacts>>().ElementType.Returns(contactsData.ElementType);
            contactsList.As<IQueryable<Contacts>>().GetEnumerator().Returns(contactsData.GetEnumerator());

            var context = Substitute.For<SQLFundamentalsContext>();
            context.Contacts.Returns(contactsList);

            var sut = new ContactRepository(context);

            var contact = sut.GetContactByID(contactId);

            contact.Should().BeOfType<Contacts>()
                .Which.ContactId.Should().Be(contactId);
        }
    }
}

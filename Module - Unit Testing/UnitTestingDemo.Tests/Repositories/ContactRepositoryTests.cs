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
        [InlineData(2)]
        [InlineData(3)]
        public void GetContactByID_ShouldReturnContactIfExists(int contactId)
        {
            // Arrange
            var contactsData = new List<Contacts>
            {
                new Contacts(1, "John", "Smith", "(407) 555-1212", "jsmith@gmail.com"),
                new Contacts(2, "Dave", "Johnson", "(407) 555-1212", "djohnson@gmail.com"),
                new Contacts(3, "Mary", "Miller", "(407) 555-1212", "mmiller@gmail.com")
            }.AsQueryable();

            var contactsList = Substitute.For<DbSet<Contacts>, IQueryable<Contacts>>();
            contactsList.Find(contactId).Returns(contactsData.FirstOrDefault(c => c.ContactId == contactId));
            contactsList.As<IQueryable<Contacts>>().Provider.Returns(contactsData.Provider);
            contactsList.As<IQueryable<Contacts>>().Expression.Returns(contactsData.Expression);
            contactsList.As<IQueryable<Contacts>>().ElementType.Returns(contactsData.ElementType);
            contactsList.As<IQueryable<Contacts>>().GetEnumerator().Returns(contactsData.GetEnumerator());

            var context = Substitute.For<SQLFundamentalsContext>();
            context.Contacts.Returns(contactsList);
            
            var sut = new ContactRepository(context);

            // Act
            var contact = sut.GetContactByID(contactId);

            // Assert
            contact.Should().BeOfType<Contacts>()
                .Which.ContactId.Should().Be(contactId);
        }
    }
}

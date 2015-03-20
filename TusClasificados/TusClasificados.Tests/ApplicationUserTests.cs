using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusClasificados.Site.Models;

namespace TusClasificados.Tests
{
    [TestFixture]
    public class ApplicationUserTests
    {
        [Test]
        public void Test_ApplicationUser_Creates_10_Bronze_Tickets()
        {
            // Arrange

            // Act
            var user = new ApplicationUser()
            {
                Nombre = "test"
            };

            // Assert
            Assert.AreEqual(10, user.Tickets.Count);
            Assert.AreEqual("test", user.Tickets[0].Usuario.Nombre);
        }
    }
}

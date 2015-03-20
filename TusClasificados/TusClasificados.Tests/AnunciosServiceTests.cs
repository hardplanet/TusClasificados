using System;
using Moq;
using NUnit.Framework;
using TusClasificados.Site.Models;
using TusClasificados.Site.Infrastructure;
using TusClasificados.Site;
using Microsoft.AspNet.Identity;
using TusClasificados.Site.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;

namespace TusClasificados.Tests
{
    [TestFixture]
    public class AnunciosServiceTests
    {
        private AnunciosService GetService(Mock<IRepositorioGenerico<Anuncio>> repo)
        {
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new ApplicationUserManager(mockUserStore.Object);
            return new AnunciosService(repo.Object, userManager);
        }

        [Test]
        public void Test_GetAll_Method_Returns_List_Of_Objects()
        {
            // Arrange            
            var mockRepository = new Mock<IRepositorioGenerico<Anuncio>>();
            mockRepository.Setup(mock => mock.SelectAll()).Returns(TestHelpers.GetListAnuncios());
            var service = GetService(mockRepository);

            // Act
            var result = service.GetAll() as List<Anuncio>;

            // Assert
            Assert.That(result, Is.InstanceOf<List<Anuncio>>());
            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void Test_GetAllForDate_Method_Returns_List_Of_Ads_For_Specified_Day()
        {
            // Arrange
            var mockRepository = new Mock<IRepositorioGenerico<Anuncio>>();
            mockRepository.Setup(mock => mock.SelectAll()).Returns(TestHelpers.GetListAnuncios());
            var service = GetService(mockRepository);

            // Act
            var result = service.GetAllForDate(DateTime.Now).ToList() as List<Anuncio>;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void Test_NuevoAnuncio_Calls_Insert_When_User_Found()
        {
            // Arrange
            var userStore = new InMemoryUserStore();
            userStore._users.Add("", new ApplicationUser()
            {
                Nombre = "test",
                Apellido = "test",
                Email = "test@example.com"
            });
            var userManager = new ApplicationUserManager(userStore);


            var mockRepository = new Mock<IRepositorioGenerico<Anuncio>>();
            mockRepository.Setup(mock => mock.Insert(It.IsAny<Anuncio>()));
            mockRepository.Setup(mock => mock.Save());

            var service = new AnunciosService(mockRepository.Object, userManager);

            var anuncio = TestHelpers.GetListAnuncios()[0];
            anuncio.Anunciante = null;
            // Act
            service.AddAnuncio(anuncio, "test@example.com");

            // Assert
            mockRepository.Verify(m => m.Insert(It.IsAny<Anuncio>()));
            mockRepository.Verify(m => m.Save());
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException), ExpectedMessage = "User email not found")]
        public void Test_NuevoAnuncio_Throws_When_User_Not_Found()
        {
            // Arrange
            var userStore = new InMemoryUserStore();
            var userManager = new ApplicationUserManager(userStore);


            var mockRepository = new Mock<IRepositorioGenerico<Anuncio>>();
            mockRepository.Setup(mock => mock.Insert(It.IsAny<Anuncio>()));
            mockRepository.Setup(mock => mock.Save());

            var service = new AnunciosService(mockRepository.Object, userManager);

            var anuncio = TestHelpers.GetListAnuncios()[0];
            anuncio.Anunciante = null;
            // Act
            service.AddAnuncio(anuncio, "test@example.com");

            // Assert
        }

        [Test]
        public void ParseStringDatesToList_Separated_Commas_And_Space_Returns_List_Of_Dates()
        {
            // Arrange
            var mockRepository = new Mock<IRepositorioGenerico<Anuncio>>();
            mockRepository.Setup(mock => mock.SelectAll()).Returns(TestHelpers.GetListAnuncios());
            var service = GetService(mockRepository);

            // Act
            var result = service.ParseStringDatesToList("03/12/15, 03/11/15, 2/10/15");

            // Assert
            Assert.AreEqual(3, result.Count);
        }
    }
}

using System;
using Moq;
using NUnit.Framework;
using TusClasificados.Site.Controllers;
using TusClasificados.Site.Models;
using TusClasificados.Site.Infrastructure;
using System.Collections.Generic;
using System.Web.Mvc;
using TusClasificados.Site.Models.ViewModels;
using TusClasificados.Site.Infrastructure.Services;

namespace TusClasificados.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController GetController(Mock<IAnunciosService> service)
        {
            var mockReloj = new Mock<IReloj>();
            mockReloj.Setup(mock => mock.Now).Returns(DateTime.Now);

            var mockCuentas = new Mock<ICuentasService>();
            mockCuentas.Setup(mock => mock.UsarTicket(It.IsAny<TipoTicket>(), It.IsAny<String>()));

            return new HomeController(service.Object, mockCuentas.Object, mockReloj.Object);
        }

        private HomeController GetController(Mock<IAnunciosService> service, string UserName)
        {
            var mockContext = new Mock<ControllerContext>();
            mockContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(UserName);
            mockContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var mockReloj = new Mock<IReloj>();
            mockReloj.Setup(mock => mock.Now).Returns(DateTime.Now);

            var mockCuentas = new Mock<ICuentasService>();
            mockCuentas.Setup(mock => mock.UsarTicket(It.IsAny<TipoTicket>(), It.IsAny<String>()));

            var controller = new HomeController(service.Object, mockCuentas.Object, mockReloj.Object);
            controller.ControllerContext = mockContext.Object;

            return controller;
        }

        [Test]
        public void Test_Index_Returns_List_Of_AnuncioViewModel()
        {
            // Arrange         
            var mockService = new Mock<IAnunciosService>();
            mockService.Setup(mock => mock.GetAllForDate(It.IsAny<DateTime>())).Returns(TestHelpers.GetListAnuncios());

            var controller = GetController(mockService);

            // Act
            var result = controller.Index() as ViewResult;             

            // Assert
            mockService.Verify(m => m.GetAllForDate(It.IsAny<DateTime>()));
            Assert.That(result.ViewData.Model, Is.InstanceOf<IEnumerable<AnuncioViewModel>>());
        }
           
        [Test]
        public void Test_NuevoAnuncio_Returns_ViewResult_When_Model_Fails()
        {
            // Arrange
            var mockService = new Mock<IAnunciosService>();
            mockService.Setup(mock => mock.AddAnuncio(It.IsAny<Anuncio>(), "test"));

            var controller = GetController(mockService);
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.NuevoAnuncio(new NuevoAnuncioViewModel()) as ViewResult;

            // Assert
            Assert.AreEqual("NuevoAnuncio", result.ViewName);
        }

        [Test]
        public void Test_NuevoAnuncio_Returns_RouteResult_When_Model_Is_Valid()
        {
            // Arrange
            var mockService = new Mock<IAnunciosService>();
            mockService.Setup(mock => mock.AddAnuncio(It.IsAny<Anuncio>(), It.IsAny<String>()));

            var controller = GetController(mockService, "test@example.com");
            controller.ModelState.Clear();

            // Act
            var result = controller.NuevoAnuncio(new NuevoAnuncioViewModel()) as RedirectToRouteResult;

            // Assert
            mockService.Verify(mock => mock.AddAnuncio(It.IsAny<Anuncio>(), It.IsAny<String>()));
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

    
        [Test]
        public void Test_NuevoAnuncio_Returns_To_View_When_Exception_Occurs()
        {
            // Arrange
            var mockService = new Mock<IAnunciosService>();
            mockService.Setup(mock => mock.AddAnuncio(It.IsAny<Anuncio>(), It.IsAny<String>())).Throws<KeyNotFoundException>();

            var mockContext = new Mock<ControllerContext>();
            mockContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("test@example.com");
            mockContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = GetController(mockService, "test@example.com");
            controller.ModelState.Clear();

            // Act
            var result = controller.NuevoAnuncio(new NuevoAnuncioViewModel()) as ViewResult;

            // Assert
            Assert.AreEqual("NuevoAnuncio", result.ViewName);
        }
    }
}

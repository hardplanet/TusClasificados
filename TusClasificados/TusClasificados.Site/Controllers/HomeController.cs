using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TusClasificados.Site.Infrastructure;
using TusClasificados.Site.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TusClasificados.Site.Models;

namespace TusClasificados.Site.Controllers
{
    public class HomeController : BaseController
    {
        private readonly RepositorioGenerico<Anuncio> _repoAnuncios;

        public HomeController()
        {
            _repoAnuncios = new RepositorioGenerico<Anuncio>(ApplicationDbContext.GetDBContext());
        }

        public ActionResult Index()
        {
            var model = _repoAnuncios.SelectAll()
                .OrderByDescending(anuncio => anuncio.Anunciante.TipoCuenta)
                .Select(anuncio => new AnuncioViewModel()
                {
                    Titulo = anuncio.Titulo,
                    Detalles = anuncio.Detalles,
                    ExtraDetalles = anuncio.ExtraDetalles,
                    NumeroContacto = anuncio.NumeroTelefono,
                    FechaPublicacion = anuncio.FechaCreacion,
                    NombreAnunciante = String.Format("{0} {1}", anuncio.Anunciante.Nombre, anuncio.Anunciante.Apellido),
                    Precio = anuncio.Precio.ToString(".00"),
                    TipoCuenta = anuncio.Anunciante.TipoCuenta
                })
                .ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult NuevoAnuncio()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult NuevoAnuncio(NuevoAnuncioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = UserManager.FindByEmail(User.Identity.GetUserName());
            var nuevoAnuncio = new Anuncio()
            {
                Titulo = model.Titulo,
                Detalles = model.Detalles,
                ExtraDetalles = model.ExtraDetalles,
                NumeroTelefono = model.NumeroContacto,
                Precio = model.Precio,
                Anunciante = user
            };

            try
            {
                _repoAnuncios.Insert(nuevoAnuncio);
                _repoAnuncios.Save();

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", "Ha ocurrido un error, intenta luego nuevamente.");
                return View(model);
            }
        }

    }
}
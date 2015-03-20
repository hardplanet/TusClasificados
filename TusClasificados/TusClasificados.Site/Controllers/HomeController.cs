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
using TusClasificados.Site.Infrastructure.Services;

namespace TusClasificados.Site.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAnunciosService _anunciosService;
        private readonly ICuentasService _cuentasService;
        private readonly IReloj _reloj;

        public HomeController(IAnunciosService anuncios, ICuentasService cuentas, IReloj reloj)
        {
            _anunciosService = anuncios;
            _cuentasService = cuentas;
            _reloj = reloj;
        }

        public ActionResult Index() 
        {
            var todayDate = _reloj.Now;

            var model = _anunciosService.GetAllForDate(todayDate)
                .OrderByDescending(anuncio => anuncio.TipoTicketUsado)
                .Select(anuncio => new AnuncioViewModel()
                {
                    Titulo = anuncio.Titulo,
                    Detalles = anuncio.Detalles,
                    ExtraDetalles = anuncio.ExtraDetalles,
                    NumeroContacto = anuncio.NumeroTelefono,
                    NombreAnunciante = String.Format("{0} {1}", anuncio.Anunciante.Nombre, anuncio.Anunciante.Apellido),
                    Precio = anuncio.Precio.ToString(".00"),
                    TipoAnuncio = anuncio.TipoTicketUsado
                })
                .ToList();

            return View(model);
        }
             
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TusClasificados.Site.Infrastructure;
using TusClasificados.Site.Infrastructure.Services;
using TusClasificados.Site.Models;
using TusClasificados.Site.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace TusClasificados.Site.Controllers
{
    public class AnunciosController : BaseController
    {
        private readonly IAnunciosService _anunciosService;
        private readonly ICuentasService _cuentasService;
        private readonly IReloj _reloj;

        public AnunciosController(IAnunciosService anuncios, ICuentasService cuentas, IReloj reloj)
        {
            _anunciosService = anuncios;
            _cuentasService = cuentas;
            _reloj = reloj;
        }

        [HttpGet]
        [Authorize]
        public ActionResult NuevoAnuncio()
        {
            var currUser = UserManager.FindByEmail(User.Identity.Name);

            var model = new NuevoAnuncioViewModel
            {
                TicketsDisponibles = new TicketCountViewModel
                {
                    GoldTickets = currUser.Tickets.Where(t => t.TipoTicket == TipoTicket.Oro).ToList().Count,
                    SilverTickets = currUser.Tickets.Where(t => t.TipoTicket == TipoTicket.Plata).ToList().Count,
                    BronzeTickets = currUser.Tickets.Where(t => t.TipoTicket == TipoTicket.Bronze).ToList().Count,
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult NuevoAnuncio(NuevoAnuncioViewModel model)
        {
            if (!ModelState.IsValid)
                return View("NuevoAnuncio", model);

            var fechasParsed = _anunciosService.ParseStringDatesToList(model.FechasAnuncio);

            var nuevoAnuncio = new Anuncio()
            {
                Titulo = model.Titulo,
                Detalles = model.Detalles,
                ExtraDetalles = model.ExtraDetalles,
                NumeroTelefono = model.NumeroContacto,
                Precio = model.Precio,
                TipoTicketUsado = model.TicketUsar,
                FechasAnuncio = fechasParsed
            };

            try
            {
                var currUser = UserManager.FindByEmail(User.Identity.Name);

                _anunciosService.AddAnuncio(nuevoAnuncio, currUser);
                _cuentasService.UsarTickets(nuevoAnuncio.TipoTicketUsado, nuevoAnuncio.FechasAnuncio.Count, currUser);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Ha ocurrido un error, intenta luego nuevamente.");
                return View("NuevoAnuncio", model);
            }
        }
    }
}
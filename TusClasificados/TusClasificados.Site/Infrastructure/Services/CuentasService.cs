using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TusClasificados.Site.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace TusClasificados.Site.Infrastructure.Services
{
    public class CuentasService : ICuentasService
    {
        private readonly IRepositorioGenerico<Ticket> _tickets;

        public CuentasService(IRepositorioGenerico<Ticket> tickets)
        {
            _tickets = tickets;
        }

        public void UsarTickets(TipoTicket tipo, int cantidad, ApplicationUser user)
        {
            if (user == null)
                throw new Exception("Usuario no encontrado");

            var tickets = user.Tickets.Where(t => t.TipoTicket == tipo).Take(cantidad).ToList(); // El salado

            tickets.ForEach(t => _tickets.Delete(t.Id));
            _tickets.Save();
        }


        public void AgregarTickets(TipoTicket tipo, int cantidad, ApplicationUser user)
        {
            if (user == null)
                throw new Exception("Usuario no encontrado");
            
            for (int ind = 1; ind <= cantidad; ind++)
            {
                _tickets.Insert(new Ticket()
                {
                    TipoTicket = tipo,
                    Usuario = user
                });
            }

            _tickets.Save();
        }
    }
}
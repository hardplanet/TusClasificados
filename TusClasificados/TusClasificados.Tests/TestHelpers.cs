using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusClasificados.Site.Models;

namespace TusClasificados.Tests
{
    public class TestHelpers
    {
        public static List<Anuncio> GetListAnuncios()
        {
            var anunciante = new ApplicationUser()
            {
                Nombre = "Carlos",
                Apellido = "Martinez"
            };

            var fechas = new List<DateTime>{ DateTime.Now };
            var fechas2 = new List<DateTime> { new DateTime(2013, 12, 12) };
            return new List<Anuncio>()
            {
                new Anuncio { Id = 1, Titulo = "Anuncio 1", Precio = 123, Anunciante = anunciante, TipoTicketUsado = TipoTicket.Bronze, FechasAnuncio = fechas },
                new Anuncio { Id = 2, Titulo = "Anuncio 2", Precio = 123, Anunciante = anunciante, TipoTicketUsado = TipoTicket.Bronze, FechasAnuncio = fechas },
                new Anuncio { Id = 3, Titulo = "Anuncio 3", Precio = 123, Anunciante = anunciante, TipoTicketUsado = TipoTicket.Plata, FechasAnuncio = fechas2 },
                new Anuncio { Id = 4, Titulo = "Anuncio 4", Precio = 123, Anunciante = anunciante, TipoTicketUsado = TipoTicket.Plata, FechasAnuncio = fechas2 },
            };
        }
    }
}

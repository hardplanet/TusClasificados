using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusClasificados.Site.Models;

namespace TusClasificados.Site.Infrastructure.Services
{
    public interface ICuentasService
    {
        void UsarTickets(TipoTicket tipo, int cantidad, ApplicationUser user);
        void AgregarTickets(TipoTicket tipo, int cantidad, ApplicationUser user);
    }
}

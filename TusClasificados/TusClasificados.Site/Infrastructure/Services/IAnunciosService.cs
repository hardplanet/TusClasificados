using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusClasificados.Site.Models;

namespace TusClasificados.Site.Infrastructure.Services
{
    public interface IAnunciosService
    {
        IEnumerable<Anuncio> GetAll();
        IEnumerable<Anuncio> GetAllForDate(DateTime date);
        void AddAnuncio(Anuncio anuncio, ApplicationUser user);
        List<DateTimeWrapper> ParseStringDatesToList(string dates);
    }
}

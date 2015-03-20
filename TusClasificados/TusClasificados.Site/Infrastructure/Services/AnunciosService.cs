using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TusClasificados.Site.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TusClasificados.Site.Infrastructure.Services
{
    public class AnunciosService : IAnunciosService
    {
        private readonly IRepositorioGenerico<Anuncio> _repo;

        public AnunciosService(IRepositorioGenerico<Anuncio> repo)
        {
            _repo = repo;
        }
        
        public IEnumerable<Anuncio> GetAll()
        {
            return _repo.SelectAll();
        }

        public void AddAnuncio(Anuncio anuncio, ApplicationUser user)
        {
            if (user != null)
            {
                anuncio.Anunciante = user;
                
                _repo.Insert(anuncio);
                _repo.Save();
            }
            else
            {
                throw new KeyNotFoundException("User email not found");
            }
        }


        public IEnumerable<Anuncio> GetAllForDate(DateTime date)
        {
            // Shuffles all the ads; It's an O(n log(n)) algorithm though, and it's easy to implement an O(n)
            // http://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm.
            var r = new Random();

            return _repo.SelectAll()
                    .Where(anuncio => anuncio.FechasAnuncio.Select(f => f.Value.Date).Contains(date.Date))
                    .OrderBy(x => r.Next());
        }


        public List<DateTimeWrapper> ParseStringDatesToList(string dates)
        {
            var datesList = new List<DateTimeWrapper>();

            var strings = dates.Split(',').ToList();

            strings.ForEach(s => datesList.Add(new DateTimeWrapper() { Value = DateTime.Parse(s) }));

            return datesList;
        }
    }
}
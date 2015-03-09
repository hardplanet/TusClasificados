using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TusClasificados.Site.Models
{
    public class Etiqueta
    {
        public int Id { get; set; }
        public String Texto { get; set; }

        public virtual IEnumerable<Anuncio> Anuncios { get; set; }
    }
}
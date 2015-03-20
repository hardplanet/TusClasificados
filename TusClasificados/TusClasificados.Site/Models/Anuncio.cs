using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TusClasificados.Site.Models
{
    public class Anuncio
    {
        public int Id { get; set; }
        public String Titulo { get; set; }
        public String Detalles { get; set; }
        public String NumeroTelefono { get; set; }
        public double Precio { get; set; }
        public String ExtraDetalles { get; set; }
        public TipoTicket TipoTicketUsado { get; set; }

        [Required]
        public virtual ApplicationUser Anunciante { get; set; }
        public virtual IEnumerable<Etiqueta> Etiquetas { get; set; }
        public virtual List<DateTimeWrapper> FechasAnuncio { get; set; }
    }

    public class DateTimeWrapper
    {
        public int Id { get; set; }
        public DateTime Value { get; set; }
    }
}
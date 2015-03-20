using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TusClasificados.Site.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public TipoTicket TipoTicket { get; set; }

        [Required]
        public virtual ApplicationUser Usuario { get; set; }
    }

    public enum TipoTicket
    {
        Bronze,
        Plata,
        Oro
    }
}
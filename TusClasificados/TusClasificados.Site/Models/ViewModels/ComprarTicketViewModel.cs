using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TusClasificados.Site.Models.ViewModels
{
    public class ComprarTicketViewModel
    {
        [Required]
        [Display(Name="Tipo de ticket a comprar")]
        public TipoTicket TipoTicket { get; set; }
        [Required]
        [Display(Name="Cantidad de tickets")]
        public int CantidadTickets { get; set; }

        public int PrecioTicketBronze { get; set; }
        public int PrecioTicketPlata { get; set; }
        public int PrecioTicketOro { get; set; }
    }
}
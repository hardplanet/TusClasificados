using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TusClasificados.Site.Models.ViewModels
{
    public class AnuncioViewModel
    {
        public String Titulo { get; set; }
        public String Detalles { get; set; }
        public String NumeroContacto { get; set; }
        public String Precio { get; set; }
        public String ExtraDetalles { get; set; }
        public String NombreAnunciante { get; set; }
        public TipoTicket TipoAnuncio { get; set; }
    }

    public class NuevoAnuncioViewModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Titulo { get; set; }
        [Required(ErrorMessage="Este campo es obligatorio")]
        [DataType(DataType.MultilineText)]
        public String Detalles { get; set; }
        [Required(ErrorMessage="Este campo es obligatorio")]
        [Display(Name="Numero de contacto")]
        public String NumeroContacto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name="Información extra")]
        public String ExtraDetalles { get; set; }
        [Required(ErrorMessage = "Debes especificar el tipo de ticket que vas a utilizar")]
        [Display(Name="Tipo de ticket a usar")]
        public TipoTicket TicketUsar { get; set; }
        [Required(ErrorMessage="Debes especificar al menos una fecha para el anuncio")]
        [Display(Name="Fechas para anunciar")]
        public String FechasAnuncio { get; set; }

        public TicketCountViewModel TicketsDisponibles { get; set; }
    }

    public class TicketCountViewModel
    {
        public int BronzeTickets { get; set; }
        public int SilverTickets { get; set; }
        public int GoldTickets { get; set; }
    }
}
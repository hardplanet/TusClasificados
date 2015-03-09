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
        public DateTime FechaPublicacion { get; set; }
        public String NombreAnunciante { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
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
    }
}
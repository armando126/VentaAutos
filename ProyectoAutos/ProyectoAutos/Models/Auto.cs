using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAutos.Models
{
    public class Auto
    {
        [Key]
        public int idAuto { get; set; }
        [Display(Name = "Nombre"), Required(ErrorMessage = "Campo obligatorio")]
        public String nombreAuto { get; set; }
        [Display(Name = "Marca"), Required(ErrorMessage = "Campo obligatorio")]
        public String marca { get; set; }
        [Display(Name = "Modelo"), Required(ErrorMessage = "Campo obligatorio")]
        public String modelo { get; set; }
        [Display(Name = "Precio"), Required(ErrorMessage = "Campo obligatorio")]
        public String precio { get; set; }

        public int estado { get; set; }
        public virtual List<Archivo> archivos { get; set; }
        public virtual List<Pedido> pedidos { get; set; }
    }
}
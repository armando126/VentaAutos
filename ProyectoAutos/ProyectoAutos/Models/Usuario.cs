using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAutos.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        [Display(Name = "Nombre"), Required(ErrorMessage = "Campo obligatorio")]
        public String nombreUsuario { get; set; }
        [Display(Name = "Usuario"), Required(ErrorMessage = "Campo obligatorio")]
        public String user { get; set; }
        [Display(Name = "Email"), Required(ErrorMessage = "Campo obligatorio"),DataType(DataType.EmailAddress)]
        public String email { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Campo obligatorio"),DataType(DataType.Password)]
        public String password { get; set; }

        public int idRol { get; set; }
        public virtual Rol rol { get; set; }
    }
}
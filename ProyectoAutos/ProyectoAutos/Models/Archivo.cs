using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAutos.Models
{
    public class Archivo
    {
        [Key]
        public int idArchivo { get; set; }
        public String nombreArchivo { get; set; }
        public String ContentType { get; set; }
        public FileType tipo { get; set; }
        public byte[] contenido { get; set; }

        public int idAuto { get; set; }
        public virtual Auto auto { get; set; }
    }
}
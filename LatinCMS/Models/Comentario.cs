using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace LatinCMS.Models
{

    public class Comentario
    {
        public virtual int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual EstadoComen Estadocomen { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual DateTime? Fecha { get; set; }

    }
}


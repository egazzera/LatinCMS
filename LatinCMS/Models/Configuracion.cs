﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LatinCMS.Models
{

    public class Configuracion
    {
        public virtual int Id { get; set; }
        public virtual int? Clave { get; set; }
        public virtual int? Valor { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int? CantPost { get; set; }
    }
}

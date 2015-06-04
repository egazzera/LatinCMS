﻿using System;
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
        public virtual Estadocomentario Estadocomentario { get; set; }
        public virtual string Descripcion { get; set; }
    }
}


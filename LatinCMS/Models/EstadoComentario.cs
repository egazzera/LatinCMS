﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LatinCMS.Models
{

    public class Estadocomentario
    {
        public Estadocomentario() { }
        public virtual int Id { get; set; }
        public virtual string Descripcion { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public class Config
    {
        public virtual int Id { get; set; }
        public virtual int Clave { get; set; }
        public virtual int Valor { get; set; }

    }
}
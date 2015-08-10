using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public class UsuarioPost
    {
        public virtual Usuario Usuario { get; set; }
        public virtual int Contador { get; set; }

    }

}
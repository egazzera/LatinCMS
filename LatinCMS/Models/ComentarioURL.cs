using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public class ComentarioURL
    {
        public virtual Comentario Comentario { get; set; }
        public virtual string URL { get; set; }

    }
}
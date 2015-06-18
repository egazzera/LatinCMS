using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{

    public class Post
    {
        public Post() { }
        public virtual int Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual TipoPost TipoPost { get; set; }
        public virtual DateTime? Fecha { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Titulo { get; set; }
    }
}

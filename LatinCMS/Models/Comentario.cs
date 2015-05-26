using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public class Comentario
    {
        public virtual int Id { get; set; }
        public virtual int Post_Id { get; set; }
        public virtual int Estado_Comentario { get; set; }

    }
}
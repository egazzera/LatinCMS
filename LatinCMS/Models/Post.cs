using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual int Usuario_Id { get; set; }
        public virtual int Tipo_Post_Id { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public class Menu
    {
        public virtual int Id { get; set; }
        public virtual int Post_Id { get; set; }
        public virtual int Padre_Id { get; set; }
        public virtual string Titulo { get; set; }

    }
}
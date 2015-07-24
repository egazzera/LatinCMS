using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LatinCMS.Models
{
    public class PostComen
    {
        public virtual Post Post { get; set; }
        public virtual int Contador { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LatinCMS.Models
{
    public class PostURL{
        public virtual Post Post { get; set; }
        public virtual string URL { get; set; }
    }

}

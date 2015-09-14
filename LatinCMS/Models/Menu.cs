using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LatinCMS.Models
{

    public class Menu
    {
        public Menu() { }
        public virtual int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual Menu MenuPadre { get; set; }
        public virtual string Texto { get; set; }
        public virtual bool? Principal { get; set; }
        public virtual bool? Secundario { get; set; }
        public virtual string Url { get; set; }
        //public virtual Menu[] children { get; set; }
        public virtual IList<Menu> children { get; set; }
        public virtual int Id_Post { get; set; }


        
    }
}

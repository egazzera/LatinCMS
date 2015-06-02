using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public class Usuario
    {
        public virtual int Id { get; set; }

        public virtual int Rol_Id { get; set; }


        [StringLength(50)]
        public virtual string Nombre { get; set; }

        [StringLength(50)]
        public virtual string Apellido { get; set; }
        
        
        public virtual string Email { get; set; }


        [StringLength(50)]
        public virtual string Apodo { get; set; }
        
        [StringLength(10)]
        public virtual string Password { get; set; }


    }
}
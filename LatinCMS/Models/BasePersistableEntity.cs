using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.Models
{
    public abstract class BasePersistableEntity
    {

        public virtual int Id { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? FechaCreacion { get; set; }
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual DateTime? FechaUltimaEdicion { get; set; }
        public virtual Usuario UsuarioUltimaEdicion { get; set; }

    }

}
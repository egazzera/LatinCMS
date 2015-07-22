using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LatinCMS.DAOs
{
    public class EstadoComentarioDAO : GenericDAO<EstadoComen>
    {
        public EstadoComen GetEstadoComenByDescripcion(string descripcion)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                EstadoComen registro = session.CreateCriteria<EstadoComen>()
                    .Add(Restrictions.Eq("Descripcion", descripcion))
                    .UniqueResult<EstadoComen>();

                return registro;
            }

        }

    }
}

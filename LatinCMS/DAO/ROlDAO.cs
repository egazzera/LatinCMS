using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAO
{
    public class RolDAO : GenericDAO<Rol>
    {

        public Rol GetRolByDescripcion(string descripcion)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Rol registro = session.CreateCriteria<Rol>()
                    .Add(Restrictions.Eq("Descripcion", descripcion))
                    .UniqueResult<Rol>();
               
                return registro;
            }

        }



    }
}
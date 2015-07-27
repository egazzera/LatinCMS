using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAOs
{
    public class TipoPostDAO: GenericDAO<TipoPost>
    {

        public TipoPost GetTipoPostByDescripcion(string descripcion)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var tipoPost = session.CreateCriteria<TipoPost>()
                    .Add(Restrictions.Eq("Descripcion", descripcion))
                    .UniqueResult<TipoPost>();

                session.Close();
                return tipoPost;
            }

        }


    }
}
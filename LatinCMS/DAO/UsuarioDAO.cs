using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAO
{
    public class UsuarioDAO : GenericDAO<Usuario>
    {

        public Usuario GetUsuarioByApodoPass(string apodo, string pass)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Usuario registro = session.CreateCriteria<Usuario>()
                    .Add(Restrictions.Eq("Apodo", apodo))
                    .Add(Restrictions.Eq("Password", pass))
                    .UniqueResult<Usuario>();

                return registro;
            }

        }




    }
}
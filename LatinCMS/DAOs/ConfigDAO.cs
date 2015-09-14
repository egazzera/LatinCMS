using LatinCMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAOs
{
    public class ConfigDAO : GenericDAO<Configuracion>
    {
        
        public Configuracion GetAllConfig()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria<Configuracion>().UniqueResult<Configuracion>();
            }

        }



    }
}
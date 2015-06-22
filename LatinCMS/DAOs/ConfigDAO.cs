using LatinCMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAOs
{
    public class ConfigDAO : GenericDAO<Config>
    {
        
        public Config GetAllConfig()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria<Config>().UniqueResult<Config>();
            }

        }



    }
}
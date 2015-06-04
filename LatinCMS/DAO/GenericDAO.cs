using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatinCMS.DAO
{
    public class GenericDAO<T>
    {

        public void Add(T modelo)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(modelo);
                transaction.Commit();
            }
        }



    }
}

using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatinCMS.DAO
{
    public class GenericDAO<T>
    {

        public T GetByDescripcion(string descripcion){
            ISession session = GetSession();

            return session.Get<T>(descripcion);
        }

        public T GetById(int id)
        {
            ISession session = GetSession();

            return session.Get<T>(id);
        }


        public T SaveOrUpdate(T entity)
        {
            using (ISession session = GetSession())
            using (ITransaction transaction = session.BeginTransaction()) { 
                session.SaveOrUpdate(entity);
                transaction.Commit();
                session.Close();
            }

            return entity;
        }


        public void Delete(T entity)
        {
            using (ISession session = GetSession()) { 
                session.Delete(entity);
                session.Close();
            }

        }


        private ISession GetSession()
        {
            return NHibernateHelper.OpenSession();
        }







    }
}

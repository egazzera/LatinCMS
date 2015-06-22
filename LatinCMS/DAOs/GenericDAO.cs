using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatinCMS.DAOs
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


        public T Guardar(T entity)
        {
            using (ISession session = GetSession())
            using (ITransaction transaction = session.BeginTransaction()) {
                try
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                    session.Close();
                }
                catch (Exception e) 
                {
                    transaction.Rollback();
                    Console.WriteLine("Se produjo una excepción. El mensaje fue: " + e.Message);
                }
            }

            return entity;
        }


        public void Eliminar(T entity)
        {
            using (ISession session = GetSession()) {
                try
                {
                    session.Delete(entity);
                    session.Transaction.Commit();
                    session.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Se produjo una excepción. El mensaje fue: " + e.Message);
                }
            }

        }


        private ISession GetSession()
        {
            return NHibernateHelper.OpenSession();
        }







    }
}

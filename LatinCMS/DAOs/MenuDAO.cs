using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAOs
{
    public class MenuDAO : GenericDAO<Menu>
    {

        public IList<Post> GetAllPageTitulos(){

            using (ISession session = NHibernateHelper.OpenSession()) 
            {

                var tipo_post = session.CreateCriteria<TipoPost>()
                    .Add(Restrictions.Eq("Descripcion", "Pagina"))
                    .UniqueResult();

                var posts = session.CreateCriteria<Post>()
                    .Add(Restrictions.Eq("TipoPost", tipo_post))
                    .Add(Restrictions.Eq("Eliminado", false))
                    .List<Post>();

                session.Close();
                return posts;
            
            }

        }


        public IList<Menu> GetAllTitulosPrincipal()
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                var menus = session.CreateCriteria<Menu>()
                    .Add(Restrictions.Eq("Principal", true))
                    .List<Menu>();

                session.Close();
                return menus;

            }

        }


        public IList<Menu> GetAllTitulosSecundario()
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                var menus = session.CreateCriteria<Menu>()
                    .Add(Restrictions.Eq("Secundario", true))
                    .List<Menu>();

                session.Close();
                return menus;

            }

        }








    }
}
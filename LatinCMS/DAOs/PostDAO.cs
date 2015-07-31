using LatinCMS.DAOs;
using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAOs
{
    public class PostDAO : GenericDAO<Post>
    {
        public IList<Post> GetAllPosts()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var posts = session.CreateCriteria<Post>("p")
                    .CreateCriteria("p.TipoPost", JoinType.InnerJoin)
                    .Add(Restrictions.Eq("Descripcion", "Post"))
                    .AddOrder(Order.Desc("p.Fecha"))
                    .List<Post>();

                session.Close();
                return posts;
            }

        }

        public IList<Comentario> GetAllComentsFromPostID(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var post = session.Get<Post>(id);

                var comentarios = session.CreateCriteria<Comentario>("c")
                    .Add(Restrictions.Eq("c.Post", post))
                    .CreateCriteria("c.Estadocomen", JoinType.InnerJoin)
                    .Add(Restrictions.Eq("Descripcion", "Aprobado"))
                    .AddOrder(Order.Desc("c.Fecha"))
                    .List<Comentario>();

                session.Close();
                return comentarios;
            }

        }

        public IList<Post> GetAllPostForTree(int mes, TipoPost tipo_post) {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var posts = session.CreateCriteria<Post>()
                    .Add(Restrictions.Eq("TipoPost", tipo_post))
                    .Add(Expression.Eq(Projections.SqlFunction("month", NHibernateUtil.DateTime, Projections.Property("Fecha")), mes))
                    .AddOrder(Order.Asc("Titulo"))
                    .List<Post>();

                session.Close();
                return posts;
            }

        }

        public IList<PostComen> GetAllPostTabla(TipoPost tipo_post) {

            using (ISession session = NHibernateHelper.OpenSession()) 
            {
                var posts = session.CreateCriteria<Post>()
                    .Add(Restrictions.Eq("Eliminado", false))
                    .Add(Restrictions.Eq("TipoPost", tipo_post))
                    .AddOrder(Order.Desc("Fecha"))
                    .List<Post>();

                List<PostComen> ListaPostComen = new List<PostComen>();

                foreach (var post in posts)
                {
                    int cant = (int) session.CreateCriteria<Comentario>()
                        .Add(Restrictions.Eq("Post", post))
                        .SetProjection(Projections.RowCount())
                        .UniqueResult(); 
                    
                    PostComen postComen = new PostComen();
                    postComen.Post = post;
                    postComen.Contador = cant;

                    ListaPostComen.Add(postComen);
                }

                session.Close();
                return ListaPostComen;
            }

            
        } 


    }
}
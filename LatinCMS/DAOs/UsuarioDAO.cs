using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAOs
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

        public IList<UsuarioPost> GetAllUserTabla(TipoPost tipo_post)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var usuarios = session.CreateCriteria<Usuario>()
                    .List<Usuario>();

                List<UsuarioPost> ListaUserPost = new List<UsuarioPost>();

                foreach (var user in usuarios)
                {
                    int cant = (int)session.CreateCriteria<Post>()
                        .Add(Restrictions.Eq("Eliminado", false))
                        .Add(Restrictions.Eq("TipoPost", tipo_post))
                        .Add(Restrictions.Eq("Usuario", user))
                        .SetProjection(Projections.RowCount())
                        .UniqueResult();

                    UsuarioPost postComen = new UsuarioPost();
                    postComen.Usuario = user;
                    postComen.Contador = cant;

                    ListaUserPost.Add(postComen);
                }

                session.Close();
                return ListaUserPost;
            }
            
        } 





    }
}
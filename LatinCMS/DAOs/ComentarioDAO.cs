using LatinCMS.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LatinCMS.DAOs
{
    public class ComentarioDAO : GenericDAO<Comentario>
    {

        public IList<ComentarioURL> GetAllComentarioTabla()
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var comentarios = session.CreateCriteria<Comentario>()
                    .AddOrder(Order.Desc("Fecha"))
                    .List<Comentario>();

                List<ComentarioURL> ListaComen = new List<ComentarioURL>();

                foreach (var comentario in comentarios)
                {
                    ComentarioURL comenURL = new ComentarioURL();

                    comenURL.Comentario = comentario;

                    ListaComen.Add(comenURL);
                }

                session.Close();
                return ListaComen;
            }


        }


        public IList<Comentario> GetAllComentarioPendienteTabla()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var estado_comen = session.CreateCriteria<EstadoComen>()
                    .Add(Restrictions.Eq("Descripcion", "Pendiente"))
                    .UniqueResult();

                var comentarios = session.CreateCriteria<Comentario>()
                    .Add(Restrictions.Eq("Estadocomen", estado_comen))
                    .AddOrder(Order.Desc("Fecha"))
                    .List<Comentario>();

                session.Close();
                return comentarios;
            }


        }


        public IList<Comentario> GetAllComentarioAprobadosTabla()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var estado_comen = session.CreateCriteria<EstadoComen>()
                    .Add(Restrictions.Eq("Descripcion", "Aprobado"))
                    .UniqueResult();

                var comentarios = session.CreateCriteria<Comentario>()
                    .Add(Restrictions.Eq("Estadocomen", estado_comen))
                    .AddOrder(Order.Desc("Fecha"))
                    .List<Comentario>();

                session.Close();
                return comentarios;
            }


        } 








    }
}
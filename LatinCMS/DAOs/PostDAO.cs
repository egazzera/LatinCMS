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
                    .AddOrder(Order.Desc("p.Fecha"))
                    .CreateCriteria("p.TipoPost", JoinType.InnerJoin)
                    .Add(Restrictions.Eq("Descripcion", "Post"))
                    .List<Post>();
                
                return posts;
            }

        }


    }
}
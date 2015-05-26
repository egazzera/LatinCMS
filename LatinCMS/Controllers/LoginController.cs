using LatinCMS.Models;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LatinCMS.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Config).Assembly);
            ISession session = cfg.BuildSessionFactory().OpenSession();
            session.BeginTransaction();
            List<Config> configs = (List<Config>)session.CreateCriteria(typeof(Config)).List<Config>();
            session.Transaction.Commit();
            session.Close();

            return View(); //Home
        }

    }
}

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
            //session.BeginTransaction();
            List<Config> configs = (List<Config>)session.CreateCriteria(typeof(Config)).List<Config>();
            //session.Transaction.Commit();
            session.Close();

            if (ValidarUsuario(Request["usuario"], Request["password"]))
            return View(); //Home

            return View(); //login fail

                        
        }

        public bool ValidarUsuario(string user, string pass)
        {
            try
            {
                Configuration cfg = new Configuration();
                cfg.Configure();
                cfg.AddAssembly(typeof(Usuario).Assembly);
                ISession session = cfg.BuildSessionFactory().OpenSession();
                List<Usuario> usuarios = (List<Usuario>)session.CreateCriteria(typeof(Usuario)).List<Usuario>();
                session.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Se produjo una excepción. El mensaje fue: {0}", e.Message);
            }

            return true;
        
        }


    }
}

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
            return View();
        }

        public ActionResult ValidarUsuario()
        {
            string apodo = Request["apodo"];
            string password = Request["password"];

            try
            {
                Configuration cfg = new Configuration();
                cfg.Configure();
                cfg.AddAssembly(typeof(Usuario).Assembly);
              
                ISession session = cfg.BuildSessionFactory().OpenSession();
                IQuery query = session.CreateQuery("FROM Usuario WHERE apodo = :nuser  AND password = :pass ");
                query.SetString("nuser", apodo);
                query.SetString("pass", password);
                
                IList<Usuario> registro = query.List<Usuario>();
                session.Close();
                
                if (registro.Count == 0){
                    ViewBag.Error = "El usuario o la clave ingresada no son correctas.";
                    return View("Index");
                }
                else
                {
                    ViewBag.Apodo = apodo;
                    return RedirectToAction("Index", "Home"); 
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("Se produjo una excepción. El mensaje fue: {0}", e.Message);
                return View(e.Message); //TODO: enviar a Index
            }

        }


    }
}

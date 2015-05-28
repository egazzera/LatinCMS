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
    public class SignupController : Controller
    {
        //
        // GET: /Signup/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NuevoUsuario()
        {
            Usuario usuario = new Usuario();

            usuario.Rol_Id = 6; // Suscriptor --TODO: Administrador
            usuario.Apodo = Request["apodo"];
            usuario.Nombre = Request["nombre"];
            usuario.Apellido = Request["apellido"];
            usuario.Email = Request["email"];
            usuario.Password = Request["password"];

            Configuration cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Usuario).Assembly);

            try
            {
                ISession session = cfg.BuildSessionFactory().OpenSession();
                session.BeginTransaction();
                session.Save(usuario);
                session.Transaction.Commit();
                session.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Se produjo una excepción. El mensaje fue: {0}", e.Message);
            }

            return RedirectToAction("Index", "Home");

            //return View();

        }

    }
}

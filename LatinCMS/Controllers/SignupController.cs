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

            usuario.Rol.Descripcion = "Suscriptor"; // Suscriptor --TODO: Administrador
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
                ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                Console.WriteLine("Se produjo una excepción. El mensaje fue: {0}", e.Message);
            }

            ViewBag.Apodo = Request["apodo"];
            return RedirectToAction("Index", "Home"); // Post

        }

    }
}

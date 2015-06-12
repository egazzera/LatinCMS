using LatinCMS.DAO;
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
            GenericDAO<Rol> util = new GenericDAO<Rol>();

            usuario.Rol = util.GetByDescripcion("Suscriptor"); // Suscriptor --TODO: Administrador query
            usuario.Apodo = Request["apodo"];
            usuario.Nombre = Request["nombre"];
            usuario.Apellido = Request["apellido"];
            usuario.Email = Request["email"];
            usuario.Password = Request["password"];

            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(usuario);
                    session.Transaction.Commit();
                    session.Close();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                return RedirectToAction("Index", "Signup"); //Registracion
            }

            ViewBag.Apodo = Request["apodo"];
            return RedirectToAction("Index", "Home"); // Post

        }

    }
}

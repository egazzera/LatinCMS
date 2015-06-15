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
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View();
        }

        public ActionResult NuevoUsuario()
        {
            Usuario usuario = new Usuario();
            RolDAO util = new RolDAO();

            usuario.Rol = util.GetRolByDescripcion("Suscriptor"); //TODO: Administrador
            usuario.Apodo = Request["apodo"];
            usuario.Nombre = Request["nombre"];
            usuario.Apellido = Request["apellido"];
            usuario.Email = Request["email"];
            usuario.Password = Request["password"];

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Save(usuario);
                    transaction.Commit();
                    session.Close();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción. El mensaje fue: " + e.Message;
                    return RedirectToAction("Index", "Signup"); //Registracion
                }
            }

            ViewBag.Apodo = Request["apodo"];
            return RedirectToAction("Index", "Home"); // Post

        }

    }
}

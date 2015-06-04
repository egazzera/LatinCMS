using LatinCMS.DAO;
using LatinCMS.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping;
using System;
using System.Collections;
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
                //Configuration cfg = new Configuration();
                //cfg.Configure();
                //cfg.AddAssembly(typeof(Usuario).Assembly);
              
                //ISession session = cfg.BuildSessionFactory().OpenSession();

                ISession session = NHibernateHelper.OpenSession();
                Usuario regitro_usuario = session.CreateCriteria(typeof(Usuario)).UniqueResult<Usuario>();
                
                //IList<Usuario> registro = query.List<Usuario>();

                //u.Rol.Descripcion;
                
                session.Close();

                if (regitro_usuario.Equals(typeof(Usuario))){
                    ViewBag.Error = "El usuario o la clave ingresada no son correctas.";
                    return View("Index");
                }
                else
                {

                    if(regitro_usuario.Rol.Descripcion == "Suscriptor")
                    {
                        TempData["Apodo"] = apodo;
                        TempData["Tipo_Usuario"] = "Suscriptor";
                        return RedirectToAction("Index", "Home");
                    }

                    if (regitro_usuario.Rol.Descripcion == "Adminstrador")
                    { 
                        TempData["Apodo"] = apodo;
                        TempData["Tipo_Usuario"] = "Administrador";
                        return RedirectToAction("Admin", "Home");
                    }

                    ViewBag.Error = "Se produjo un error inesperado en el Login, llame al 911.";
                    return View("Index");

                }

            }
            catch(Exception e)
            {
                ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                return View("Index");
            }

        }


    }
}

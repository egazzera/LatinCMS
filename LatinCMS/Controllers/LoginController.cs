using LatinCMS.DAOs;
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

            UsuarioDAO util = new UsuarioDAO();

            ISession session = NHibernateHelper.OpenSession();

            try{

                Usuario registro_usuario = util.GetUsuarioByApodoPass(apodo, password);

                if (registro_usuario == null){
                    ViewBag.Error = "El usuario o la clave ingresada no son correctos.";
                    session.Close();
                    return View("Index");
                }
                else
                {
                    if(registro_usuario.Rol.Descripcion != null)
                    {
                        session.Close();
                        return RedirectToAction("Index", "Home", new { id_usuario = registro_usuario.Id });
                    }

                    ViewBag.Error = "Se produjo un error inesperado en el Login, llame al 911.";
                    session.Close();
                    return View("Index");
                }
            }
            catch(Exception e)
            {
                ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                session.Close();
                return View("Index");
            }
                

        }






    }

}



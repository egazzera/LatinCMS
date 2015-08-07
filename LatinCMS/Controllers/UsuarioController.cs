using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LatinCMS.DAOs;
using LatinCMS.Models;
using NHibernate;

namespace LatinCMS.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View();
        }

        
         //
        // GET: /Usuario/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                GenericDAO<Usuario> util = new GenericDAO<Usuario>();

                Usuario registro_usuario = util.GetById(id);

                return View("Edit", registro_usuario);
            }
            catch(Exception e){
                ViewBag.Error = "Se produjo una excepción en /Usuario/Edit. El mensaje fue: " + e.Message;
                return View("Edit");
            }
        }

     

        public ActionResult GuardarUsuario(int idUsuario){
            Usuario usuario = new Usuario();
            RolDAO util = new RolDAO();

            usuario.Id = idUsuario;
            usuario.Rol = util.GetRolByDescripcion("Suscriptor"); 
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
                    session.SaveOrUpdate(usuario);
                    transaction.Commit();
                    session.Close();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción en /Usuario/GuardarUsuario. El mensaje fue: " + e.Message;
                    return RedirectToAction("Edit", "Usuario"); //Edicion
                }
            }

            TempData["Apodo"] = Request["apodo"];
            TempData["Tipo_Usuario"] = "Suscriptor";

            return RedirectToAction("Index", "Home"); // Ver Post

        }


        public ActionResult VerUsuario(int id_usuario)
        {
            try
            {
                GenericDAO<Usuario> util = new GenericDAO<Usuario>();

                Usuario registro_usuario = util.GetById(id_usuario);

                return View("VerUsuario", registro_usuario);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Se produjo una excepción en /Usuario/VerUsuario. El mensaje fue: " + e.Message;
                return View("VerUsuario");
            }
        }




    }
}

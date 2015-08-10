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


        public JsonResult AllUser()
        {
            UsuarioDAO util = new UsuarioDAO();
            TipoPostDAO util_tipo_post = new TipoPostDAO();

            try
            {
                IList<UsuarioPost> TablaUsuarioPost = util.GetAllUserTabla(util_tipo_post.GetTipoPostByDescripcion("Post"));

                return Json(TablaUsuarioPost, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Post/AllPost. El mensaje fue: " + e.Message });
            }

        }


        public ActionResult NewUser(int id_usuario)
        {
            Usuario user = new Usuario();
            RolDAO util_rol = new RolDAO();

            user.Rol = util_rol.GetRolByDescripcion(Request["rol"]);
            user.Nombre = Request["nombre"];
            user.Apellido = Request["apellido"];
            user.Email = Request["email"];
            user.Apodo = Request["apodo"];
            user.Password = Request["password"];

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                    session.Close();
                    TempData["SaveOK"] = "Se guardo correctamente el nuevo usuario.";
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción en /Usuario/NewUser. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
            }

        }


        public JsonResult EditUser(int id_user)
        {
            try
            {
                GenericDAO<Usuario> util = new GenericDAO<Usuario>();

                Usuario registro_user = util.GetById(id_user);

                return Json(registro_user, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Usuario/EditUser. El mensaje fue: " + e.Message });
            }
        }


        public JsonResult DeleteUser(int id_user)
        {
            GenericDAO<Usuario> util = new GenericDAO<Usuario>();

            Usuario registro_user = util.GetById(id_user);

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Delete(registro_user);
                    transaction.Commit();
                    session.Close();

                    return Json("Se elimino correctamente al usuario.");
                }
                catch (Exception e)
                {
                    return Json(new { errMsg = "Se produjo una excepción en /Usuario/DeleteUser. El mensaje fue: " + e.Message });
                }
            }
        }


        public ActionResult SaveEditUser(int id_usuario)
        {
            Usuario userEdit = new Usuario();
            RolDAO util_rol = new RolDAO();

            userEdit.Rol = util_rol.GetRolByDescripcion(Request["rol"]); 
            userEdit.Nombre = Request["nombre"];
            userEdit.Apellido = Request["apellido"];
            userEdit.Email = Request["email"];
            userEdit.Apodo = Request["apodo"];
            userEdit.Password = Request["password"];

            int usuario_id = id_usuario;

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(userEdit);
                    transaction.Commit();
                    session.Close();
                    TempData["SaveOK"] = "Se guardo correctamente la modificación al usuario.";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción en /Usuario/SaveEdituser. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id });
                }
            }

            return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id });

        }




    }
}

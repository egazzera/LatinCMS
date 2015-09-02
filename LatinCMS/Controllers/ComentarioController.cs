using LatinCMS.DAOs;
using LatinCMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LatinCMS.Controllers
{
    public class ComentarioController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult NuevoComentatio(int id_post, int id_user)
        {
            Comentario comentario = new Comentario();
            GenericDAO<Post> util_post = new GenericDAO<Post>();
            GenericDAO<Usuario> util_user = new GenericDAO<Usuario>();
            EstadoComentarioDAO util_EC = new EstadoComentarioDAO();

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    comentario.Descripcion = Request["comentario"].Trim();
                    comentario.Post = util_post.GetById(id_post);
                    comentario.Usuario = util_user.GetById(id_user);
                    comentario.Estadocomen = util_EC.GetEstadoComenByDescripcion("Pendiente");
                    comentario.Fecha = DateTime.Now;

                    session.SaveOrUpdate(comentario);
                    transaction.Commit();
                    session.Close();

                    TempData["Save_Comen_Ok"] = "Se grabo correctamente el comentario.";

                    return RedirectToAction("Index", "Home", new { id_usuario = id_user });
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción en /Comentario/NuevoComentatio. El mensaje fue: " + e.Message;
                    return RedirectToAction("Index", "Post"); //Post con info del error
                }
            }

        }


        public JsonResult AllComentarios()
        {
            ComentarioDAO util = new ComentarioDAO();

            try
            {
                IList<ComentarioURL> TablaAllComen = util.GetAllComentarioTabla();

                foreach (var item in TablaAllComen)
                {
                    item.URL = Url.Action("VerUsuario", "Usuario", new { id_usuario = item.Comentario.Usuario.Id });
                }

                return Json(TablaAllComen, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Comentario/AllComentarios. El mensaje fue: " + e.Message });
            }

        }
        

        public JsonResult AprobarComen(int id_comen)
        {
           GenericDAO<Comentario> util = new GenericDAO<Comentario>();
           EstadoComentarioDAO util_EC = new EstadoComentarioDAO();

           Comentario registro_comen = util.GetById(id_comen);
           
           using (ISession session = NHibernateHelper.OpenSession())
           using (ITransaction transaction = session.BeginTransaction()) 
           { 
                try
                {
                    registro_comen.Estadocomen = util_EC.GetEstadoComenByDescripcion("Aprobado");

                    session.SaveOrUpdate(registro_comen);
                    transaction.Commit();
                    session.Close();

                    return Json("Se grabo correctamente la aprobación del comentario.");
                }
                catch(Exception e){
                    return Json(new { errMsg = "Se produjo una excepción en /Comentario/AprobarComen. El mensaje fue: " + e.Message });
                }
           }
        }


        public JsonResult EliminarComen(int id_comen)
        {
           GenericDAO<Comentario> util = new GenericDAO<Comentario>();

           Comentario registro_comen = util.GetById(id_comen);
           
           using (ISession session = NHibernateHelper.OpenSession())
           using (ITransaction transaction = session.BeginTransaction()) 
           { 
                try
                {
                    session.Delete(registro_comen);
                    transaction.Commit();
                    session.Close();

                    return Json("Se elimino correctamente el comentario.");
                }
                catch(Exception e){
                    return Json(new { errMsg = "Se produjo una excepción en /Comentario/EliminarComen. El mensaje fue: " + e.Message });
                }
           }
        }

        public JsonResult AllComentariosPendientes()
        {
            ComentarioDAO util = new ComentarioDAO();

            try
            {
                IList<Comentario> TablaAllComenPend = util.GetAllComentarioPendienteTabla();

                List<ComentarioURL> listaComenURL = new List<ComentarioURL>();
                
                foreach (var item in TablaAllComenPend)
                {
                    ComentarioURL comenURL = new ComentarioURL();
                    comenURL.Comentario = item;
                    comenURL.URL = Url.Action("VerUsuario", "Usuario", new { id_usuario = item.Usuario.Id });
                    
                    listaComenURL.Add(comenURL);
                }

                return Json(listaComenURL, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Comentario/AllComentariosPendientes. El mensaje fue: " + e.Message });
            }

        }


        public JsonResult AllComentariosAprobados()
        {
            ComentarioDAO util = new ComentarioDAO();

            try
            {
                IList<Comentario> TablaAllComenAprob = util.GetAllComentarioAprobadosTabla();

                List<ComentarioURL> listaComenURL = new List<ComentarioURL>();

                foreach (var item in TablaAllComenAprob)
                {
                    ComentarioURL comenURL = new ComentarioURL();
                    comenURL.Comentario = item;
                    comenURL.URL = Url.Action("VerUsuario", "Usuario", new { id_usuario = item.Usuario.Id });

                    listaComenURL.Add(comenURL);
                }

                return Json(listaComenURL, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Comentario/AllComentariosAprobados. El mensaje fue: " + e.Message });
            }

        }


    }
}

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
    public class PostController : Controller
    {

        public ActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View();
        }

        public ActionResult IrAPost(int id)
        {
            PostDAO util = new PostDAO();
            GenericDAO<Post> utilPost = new GenericDAO<Post>();
            
            try
            {
                IList<Comentario> comentarios_post = util.GetAllComentsFromPostID(id);

                Post post = utilPost.GetById(id);

                Session["Titulo_Post"] = post.Titulo;
                Session["Descripcion_Post"] = post.Descripcion;
                Session["Fecha_Post"] = post.Fecha;
                Session["Post_Id"] = post.Id;

                return View("Index", comentarios_post);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                return View("Index");
            }
        }


        public JsonResult AllPost()
        {
            PostDAO util = new PostDAO();

            try
            {
                IList<PostComen> TablaPostComen = util.GetAllPostTabla();
                return Json(TablaPostComen, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción. El mensaje fue: " + e.Message });
            }

        }

        public ActionResult NewPost(int id_usuario)
        {
            Post post = new Post();
            GenericDAO<Usuario> util_usuario = new GenericDAO<Usuario>();
            TipoPostDAO util_tipo_post = new TipoPostDAO();

            post.Usuario = util_usuario.GetById(id_usuario);
            post.TipoPost = util_tipo_post.GetTipoPostByDescripcion("Post");
            post.Fecha = DateTime.Now;
            post.Titulo = Request["titulo"];
            post.Descripcion = Request["descripcion"];
            post.Eliminado = false;

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(post);
                    transaction.Commit();
                    session.Close();
                    TempData["Save_NewPost_OK"] = "Se guardo correctamente el nuevo post.";
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario }); 
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario }); 
                }
            }

            
        }



    }
}

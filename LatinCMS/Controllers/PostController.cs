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
            TipoPostDAO util_tipo_post = new TipoPostDAO();

            try
            {
                IList<PostComen> TablaPostComen = util.GetAllPostTabla(util_tipo_post.GetTipoPostByDescripcion("Post"));
                
                foreach(var item in TablaPostComen){
                    item.URLVer = Url.Action("IrAPost", "Post", new { id = item.Post.Id }); 
                }
                
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
                    TempData["Error"] = null;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario }); 
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Save_NewPost_OK"] = null;
                    TempData["Error"] = "Se produjo una excepción. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario }); 
                }
            }
            
        }


        public JsonResult EditPost(int id_post)
        {
            try
            {
                GenericDAO<Post> util = new GenericDAO<Post>();

                Post registro_post = util.GetById(id_post);

                return Json(registro_post, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e){
                return Json(new { errMsg = "Se produjo una excepción. El mensaje fue: " + e.Message });
            }
        }


        public ActionResult SaveEditPost()
        {
            Post postEdit = new Post();
            GenericDAO<Post> util = new GenericDAO<Post>();

            postEdit.Id = Convert.ToInt32(Request["id_post"]);
            
            Post reg_original = util.GetById(postEdit.Id);

            postEdit.Usuario = reg_original.Usuario;
            postEdit.TipoPost = reg_original.TipoPost;
            postEdit.Fecha = reg_original.Fecha;
            postEdit.Titulo = Request["titulo"];
            postEdit.Descripcion = Request["descripcion"];
            postEdit.Eliminado = reg_original.Eliminado;

            int usuario_id = Convert.ToInt32(Request["id_usuario"]);

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(postEdit);
                    transaction.Commit();
                    session.Close();
                    TempData["SaveOK"] = "Se guardo correctamente la modificación al post.";
                    TempData["Error"] = null;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["SaveOK"] = null;
                    TempData["Error"] = "Se produjo una excepción. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id }); 
                }
            }

            TempData["Apodo"] = reg_original.Usuario.Nombre;
            TempData["Tipo_Usuario"] = reg_original.Usuario.Rol.Descripcion;

            return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id }); 

        }


        public JsonResult DeletePopUpPost(int id_post)
        {
            try
            {
                GenericDAO<Post> util = new GenericDAO<Post>();

                Post registro_post = util.GetById(id_post);

                return Json(registro_post, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción. El mensaje fue: " + e.Message });
            }
        }


        public ActionResult DeletePost() {

            Post postDelete = new Post();
            GenericDAO<Post> util = new GenericDAO<Post>();

            postDelete.Id = Convert.ToInt32(Request["id_post"]);

            Post reg_original = util.GetById(postDelete.Id);

            postDelete.Usuario = reg_original.Usuario;
            postDelete.TipoPost = reg_original.TipoPost;
            postDelete.Fecha = reg_original.Fecha;
            postDelete.Titulo = reg_original.Titulo;
            postDelete.Descripcion = reg_original.Descripcion;
            postDelete.Eliminado = true;

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(postDelete);
                    transaction.Commit();
                    session.Close();
                    TempData["SaveOK"] = "Se elimino correctamente el post.";
                    TempData["Error"] = null;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["SaveOK"] = null;
                    TempData["Error"] = "Se produjo una excepción. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = postDelete.Id });
                }
            }

            TempData["Apodo"] = reg_original.Usuario.Nombre;
            TempData["Tipo_Usuario"] = reg_original.Usuario.Rol.Descripcion;

            return RedirectToAction("Admin", "Home", new { id_usuario = postDelete.Id }); 
        
        }





        public JsonResult AllPages()
        {
            PostDAO util = new PostDAO();
            TipoPostDAO util_tipo_post = new TipoPostDAO();

            try
            {
                IList<PostComen> TablaPostComen = util.GetAllPostTabla(util_tipo_post.GetTipoPostByDescripcion("Pagina"));
                return Json(TablaPostComen, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción. El mensaje fue: " + e.Message });
            }

        }


        public ActionResult NewPage(int id_usuario)
        {
            Post page = new Post();
            GenericDAO<Usuario> util_usuario = new GenericDAO<Usuario>();
            TipoPostDAO util_tipo_post = new TipoPostDAO();

            page.Usuario = util_usuario.GetById(id_usuario);
            page.TipoPost = util_tipo_post.GetTipoPostByDescripcion("Pagina");
            page.Fecha = DateTime.Now;
            page.Titulo = Request["titulo"];
            page.Descripcion = Request["descripcion"];
            page.Eliminado = false;

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(page);
                    transaction.Commit();
                    session.Close();
                    TempData["SaveOK"] = "Se guardo correctamente la nueva pagina.";
                    TempData["Error"] = null;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["SaveOK"] = null;
                    TempData["Error"] = "Se produjo una excepción. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
            }

        }



    }
}

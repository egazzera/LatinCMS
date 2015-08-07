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


        //POST
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
                ViewBag.Error = "Se produjo una excepción en /Post/IrAPost. El mensaje fue: " + e.Message;
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
                return Json(new { errMsg = "Se produjo una excepción en /Post/AllPost. El mensaje fue: " + e.Message });
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
                    TempData["Error"] = "Se produjo una excepción en /Post/NewPost. El mensaje fue: " + e.Message;
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
                return Json(new { errMsg = "Se produjo una excepción en /Post/EditPost. El mensaje fue: " + e.Message });
            }
        }


        public ActionResult SaveEditPost(int id_usuario)
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

            int usuario_id = id_usuario;

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
                    TempData["Error"] = "Se produjo una excepción en /Post/SaveEditPost. El mensaje fue: " + e.Message;
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
                return Json(new { errMsg = "Se produjo una excepción /Post/DeletePopUpPost. El mensaje fue: " + e.Message });
            }
        }


        public ActionResult DeletePost(int id_usuario)
        {
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
                    TempData["Error"] = "Se produjo una excepción en /Post/DeletePost. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
            }

            TempData["Apodo"] = reg_original.Usuario.Nombre;
            TempData["Tipo_Usuario"] = reg_original.Usuario.Rol.Descripcion;

            return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario }); 
        
        }




        //PAGE
        public ActionResult IrAPage(int id)
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
                ViewBag.Error = "Se produjo una excepción en /Post/IrAPage. El mensaje fue: " + e.Message;
                return View("Index");
            }
        }


        public JsonResult AllPage()
        {
            PostDAO util = new PostDAO();
            TipoPostDAO util_tipo_post = new TipoPostDAO();

            try
            {
                IList<PostComen> TablaPageComen = util.GetAllPostTabla(util_tipo_post.GetTipoPostByDescripcion("Pagina"));

                foreach (var item in TablaPageComen)
                {
                    item.URLVer = Url.Action("IrAPage", "Post", new { id = item.Post.Id });
                }

                return Json(TablaPageComen, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Post/AllPage. El mensaje fue: " + e.Message });
            }

        }

        public ActionResult NewPage(int id_usuario)
        {
            Post post = new Post();
            GenericDAO<Usuario> util_usuario = new GenericDAO<Usuario>();
            TipoPostDAO util_tipo_post = new TipoPostDAO();

            post.Usuario = util_usuario.GetById(id_usuario);
            post.TipoPost = util_tipo_post.GetTipoPostByDescripcion("Pagina");
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
                    TempData["Save_NewPost_OK"] = "Se guardo correctamente la nueva pagina.";
                    TempData["Error"] = null;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Save_NewPost_OK"] = null;
                    TempData["Error"] = "Se produjo una excepción en /Post/NewPage. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
            }

        }


        public JsonResult EditPage(int id_post)
        {
            try
            {
                GenericDAO<Post> util = new GenericDAO<Post>();

                Post registro_post = util.GetById(id_post);

                return Json(registro_post, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Post/EditPage. El mensaje fue: " + e.Message });
            }
        }


        public ActionResult SaveEditPage(int id_usuario)
        {
            Post postEdit = new Post();
            GenericDAO<Post> util = new GenericDAO<Post>();

            postEdit.Id = Convert.ToInt32(Request["id_page"]);

            Post reg_original = util.GetById(postEdit.Id);

            postEdit.Usuario = reg_original.Usuario;
            postEdit.TipoPost = reg_original.TipoPost;
            postEdit.Fecha = reg_original.Fecha;
            postEdit.Titulo = Request["titulo"];
            postEdit.Descripcion = Request["descripcion"];
            postEdit.Eliminado = reg_original.Eliminado;

            int usuario_id = id_usuario;

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(postEdit);
                    transaction.Commit();
                    session.Close();
                    TempData["SaveOK"] = "Se guardo correctamente la modificación a la pagina.";
                    TempData["Error"] = null;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["SaveOK"] = null;
                    TempData["Error"] = "Se produjo una excepción en /Post/SaveEditPage. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id });
                }
            }

            TempData["Apodo"] = reg_original.Usuario.Nombre;
            TempData["Tipo_Usuario"] = reg_original.Usuario.Rol.Descripcion;

            return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id });

        }


        public JsonResult DeletePopUpPage(int id_post)
        {
            try
            {
                GenericDAO<Post> util = new GenericDAO<Post>();

                Post registro_post = util.GetById(id_post);

                return Json(registro_post, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Post/DeletePopUpPage. El mensaje fue: " + e.Message });
            }
        }


        public ActionResult DeletePage(int id_usuario)
        {
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
                    TempData["SaveOK"] = "Se elimino correctamente la pagina.";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción en /Post/DeletePage. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });
                }
            }

            TempData["Apodo"] = reg_original.Usuario.Nombre;
            TempData["Tipo_Usuario"] = reg_original.Usuario.Rol.Descripcion;

            return RedirectToAction("Admin", "Home", new { id_usuario = id_usuario });

        }




    }
}

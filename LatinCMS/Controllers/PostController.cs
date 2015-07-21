using LatinCMS.DAOs;
using LatinCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LatinCMS.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Index()
        {
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

                return View("Index", comentarios_post);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                return View("Index");
            }
        }



    }
}

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

        public ActionResult IrAPost(int id, string titulo, string descripcion)
        {
            try
            {
                PostDAO util = new PostDAO();

                IList<Comentario> comentarios_post = util.GetAllComentsFromPostID(id);

                Session["Titulo_Post"] = titulo;
                Session["Descripcion_Post"] = descripcion;

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

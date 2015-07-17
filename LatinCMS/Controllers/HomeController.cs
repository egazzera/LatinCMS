using LatinCMS.DAOs;
using LatinCMS.Models;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LatinCMS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (TempData["Apodo"] != null)
            {
                //ViewBag.Apodo = TempData["Apodo"];
                //ViewBag.TipoUsuario = TempData["Tipo_Usuario"];
                //ViewBag.Id = TempData["Id"];

                try
                {
                    ConfigDAO utils = new ConfigDAO();
                    Config registro_config = utils.GetAllConfig();

                    Session["Apodo"] = TempData["Apodo"];
                    Session["Tipo_Usuario"] = TempData["Tipo_Usuario"];
                    Session["Id"] = TempData["Id"];
                    Session["Titulo_Home"] = registro_config.Titulo;
                    Session["Descripcion_Home"] = registro_config.Descripcion;
                    Session["Cant_Post"] = registro_config.CantPost;

                    PostDAO utilPost = new PostDAO();
                    IList<Post> posts = utilPost.GetAllPosts();
                    return View(posts);
                }
                catch (Exception e) {
                    ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                }

            }

            return View();
        }

        public ActionResult Admin()
        {
            if (TempData["Apodo"] != null)
            {
                try
                {
                    ConfigDAO utils = new ConfigDAO();
                    Config registro_config = utils.GetAllConfig();

                    Session["Apodo"] = TempData["Apodo"];
                    Session["Tipo_Usuario"] = TempData["Tipo_Usuario"];
                    Session["Id"] = TempData["Id"];
                    Session["Titulo_Home"] = registro_config.Titulo;
                    Session["Descripcion_Home"] = registro_config.Descripcion;
                    Session["Cant_Post"] = registro_config.CantPost;

                    PostDAO utilPost = new PostDAO();
                    IList<Post> posts = utilPost.GetAllPosts();
                    return View(posts);
                }
                catch (Exception e)
                {
                    ViewBag.Error = "Se produjo una excepción. El mensaje fue: " + e.Message;
                }

            }

            return View();
        }

        public JsonResult GetTitulosByMesArbol(int mes, int tipo_post_id)
        {
            PostDAO util = new PostDAO();
            GenericDAO<TipoPost> util_tipo_post = new GenericDAO<TipoPost>();

            TipoPost tipo_post = util_tipo_post.GetById(tipo_post_id);

            try
            {
                IList<Post> treePost = util.GetAllPostForTree(mes, tipo_post);

                if(treePost.Count() == 0)
                    return Json(mes);

                return Json(treePost, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción. El mensaje fue: " + e.Message });
            }

        }


    }
}

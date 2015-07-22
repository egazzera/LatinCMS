﻿using LatinCMS.DAOs;
using LatinCMS.Models;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections;
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

        public ActionResult Index(int id_usuario)
        {
            if (id_usuario > 0)
            {
                try
                {
                    if (TempData["Save_Comen_Ok"] != null)
                        ViewBag.Save_Comen_Ok = TempData["Save_Comen_Ok"];

                    ConfigDAO util_config = new ConfigDAO();
                    Config registro_config = util_config.GetAllConfig();
                    GenericDAO<Usuario> util_user = new GenericDAO<Usuario>();

                    Usuario usuario = util_user.GetById(id_usuario);

                    Session["Apodo"] = usuario.Apodo;
                    Session["Tipo_Usuario"] = usuario.Rol.Descripcion;
                    Session["Id"] = id_usuario;
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
            else
            {
                TempData["Error"] = "Se produjo un error. El ID de Usuario: " + id_usuario + " no existe.";
                return RedirectToAction("Index", "Signup"); 
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

                List<PostURL> listaPostURL = new List<PostURL>();

                for (var i = 0; i < treePost.Count(); i++)
                {
                    PostURL postUrl = new PostURL();
                    postUrl.Post = treePost[i];
                    postUrl.URL = Url.Action("IrAPost", "Post", new { id = treePost[i].Id });
                    listaPostURL.Add(postUrl);
                }

                return Json(listaPostURL, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción. El mensaje fue: " + e.Message });
            }

        }


    }
}

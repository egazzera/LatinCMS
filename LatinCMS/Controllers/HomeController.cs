using LatinCMS.DAO;
using LatinCMS.Models;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

                ConfigDAO utils = new ConfigDAO();
                Config registro_config = utils.GetAllConfig();

                Session["Apodo"] = TempData["Apodo"];
                Session["Tipo_Usuario"] = TempData["Tipo_Usuario"];
                Session["Id"] = TempData["Id"].ToString();
                Session["Titulo_Home"] = registro_config.Titulo;
                Session["Descripcion_Home"] = registro_config.Descripcion;
                Session["Cant_Post"] = registro_config.CantPost;
            }

            return View();
        }

        public ActionResult Admin()
        {
            if (TempData["Apodo"] != null)
            {
                ViewBag.Apodo = TempData["Apodo"];
                ViewBag.TipoUsuario = TempData["Tipo_Usuario"];
                ViewBag.Id = TempData["Id"];
            }

            return View();
        }


    }
}

﻿using LatinCMS.Models;
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
                ViewBag.Apodo = TempData["Apodo"];
                ViewBag.TipoUsuario = TempData["Tipo_Usuario"];
                ViewBag.Id = TempData["Id"];

                Session["Apodo"] = TempData["Apodo"];
                Session["Tipo_Usuario"] = TempData["Tipo_Usuario"];
                Session["Id"] = TempData["Id"].ToString();
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

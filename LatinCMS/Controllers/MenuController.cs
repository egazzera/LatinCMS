using LatinCMS.DAOs;
using LatinCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LatinCMS.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult AllEditarMenu() {

            MenuDAO util = new MenuDAO();

            try 
            {
                IList<Post> titulosPage = util.GetAllPageTitulos();

                return Json(titulosPage, JsonRequestBehavior.AllowGet);
            
            }
            catch(Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Menu/AllEditarMenu. El mensaje fue: " + e.Message });
            }
        
        }

        public JsonResult GetAllMenuPrimario(){

            MenuDAO util = new MenuDAO();

            try
            {
                IList<Menu> listaMenu = util.GetAllTitulosPrincipal();

                return Json(listaMenu, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e) 
            {
                return Json(new { errMsg = "Se produjo una excepción en /Menu/GetAllMenuPrimario. El mensaje fue: " + e.Message });
            }
        
        }


        public JsonResult GetAllMenuSecundario()
        {

            MenuDAO util = new MenuDAO();

            try
            {
                IList<Menu> listaMenu = util.GetAllTitulosSecundario();

                return Json(listaMenu, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Menu/GetAllMenuSecundario. El mensaje fue: " + e.Message });
            }

        }




    }
}
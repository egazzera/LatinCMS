using LatinCMS.DAOs;
using LatinCMS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Mapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public JsonResult SaveMenuApariencia(string tap, string menu)
        {
            bool principal = false;
            bool secundario = false;

            if (tap == "#AllMenuPrimario")
                principal = true;

            if (tap == "#AllMenuSecundario")
                secundario = true;

                try
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    List<Menu> jsonObject = serializer.Deserialize<List<Menu>>(menu);

                    int cant = jsonObject.Count;

                    MenuDAO util_menu = new MenuDAO();

                    util_menu.DeleteMenu(); 

                    foreach (var item in jsonObject)
                    {
                        ISession session = NHibernateHelper.OpenSession();
                        ITransaction transaction = session.BeginTransaction();

                        item.Principal = principal;
                        item.Secundario = secundario;

                        if (item.Id_Post != 0)
                        {
                            PostDAO util_post = new PostDAO();
                            item.Post = util_post.GetById(item.Id_Post);
                        }
                        
                        session.SaveOrUpdate(item);
                        transaction.Commit();

                        if (item.children != null) { 
                            Menu id_padre = util_menu.GetByNewLastId();
                            IList<Menu> hijos = item.children;
                                
                            SaveChildrens(id_padre, hijos, principal, secundario);
                        }

                    }

                    //session.Close();
                    return Json("Se guardo correctamente la modificación al menu.");
                }
                catch (Exception e)
                {
                    //transaction.Rollback();
                    //session.Close();
                    return Json(new { errMsg = "Se produjo una excepción en /Menu/SaveMenuApariencia. El mensaje fue: " + e.Message });
                }

        }


        public void SaveChildrens(Menu id_padre, IList<Menu> hijos, bool principal, bool secundario)
        {
            using (ISession session2 = NHibernateHelper.OpenSession())
            using (ITransaction transaction2 = session2.BeginTransaction())
            { 
                for (int i = 0; i < hijos.Count; i++)
                {
                    var item = hijos[i]; 

                    item.Principal = principal;
                    item.Secundario = secundario;

                    if (item.Id_Post != 0)
                    {
                        PostDAO util_post = new PostDAO();
                        item.Post = util_post.GetById(item.Id_Post);
                    }

                    item.MenuPadre = id_padre;

                    session2.SaveOrUpdate(item);
                    transaction2.Commit();

                    if (item.children != null)
                    {
                        MenuDAO util_menu = new MenuDAO();
                        Menu id_padre2 = util_menu.GetByNewLastId();
                        IList<Menu> hijos2 = item.children;

                        SaveChildrens(id_padre2, hijos, principal, secundario);
                    }

                }
                
            }
        
        
        }


    }
}
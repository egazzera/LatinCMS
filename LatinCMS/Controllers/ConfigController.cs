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
    public class ConfigController : Controller
    {
        //
        // GET: /Config/

        public ActionResult Index()
        {
            return View();
        }



        public JsonResult EditAjustes()
        {
            try
            {
                ConfigDAO util = new ConfigDAO();

                Configuracion registro_config = util.GetAllConfig();

                return Json(registro_config, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errMsg = "Se produjo una excepción en /Config/EditAjustes. El mensaje fue: " + e.Message });
            }
        }


        public ActionResult SaveAjustes(int id_usuario) {

            GenericDAO<Configuracion> util = new GenericDAO<Configuracion>();
            Configuracion configNew = new Configuracion();
            
            Configuracion config_old = util.GetById(Convert.ToInt32(Request["id_ajuste"]));

            configNew.Id = config_old.Id;
            configNew.Titulo = Request["titulo"].Trim();
            configNew.Descripcion = Request["descripcion"].Trim();
            configNew.CantPost = Convert.ToInt32(Request["cant"]);
            configNew.Clave = config_old.Clave;
            configNew.Valor = config_old.Valor;

            int usuario_id = id_usuario;

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(configNew);
                    transaction.Commit();
                    session.Close();
                    TempData["SaveOK"] = "Se guardo correctamente la modificación a los ajustes.";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción en /Config/SaveAjustes. El mensaje fue: " + e.Message;
                    return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id });
                }
            }

            return RedirectToAction("Admin", "Home", new { id_usuario = usuario_id });
        
        
        }


    }
}

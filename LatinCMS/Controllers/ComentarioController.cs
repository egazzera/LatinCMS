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
    public class ComentarioController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult NuevoComentatio(int id_post, int id_user)
        {
            Comentario comentario = new Comentario();
            GenericDAO<Post> util_post = new GenericDAO<Post>();
            GenericDAO<Usuario> util_user = new GenericDAO<Usuario>();
            EstadoComentarioDAO util_EC = new EstadoComentarioDAO();

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    comentario.Descripcion = Request["comentario"];
                    comentario.Post = util_post.GetById(id_post);
                    comentario.Usuario = util_user.GetById(id_user);
                    comentario.Estadocomen = util_EC.GetEstadoComenByDescripcion("Pendiente");
                    comentario.Fecha = DateTime.Now;

                    session.SaveOrUpdate(comentario);
                    transaction.Commit();
                    session.Close();

                    TempData["Save_Comen_Ok"] = "Se grabo correctamente el comentario.";

                    return RedirectToAction("Index", "Home", new { id_usuario = id_user });
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Se produjo una excepción. El mensaje fue: " + e.Message;
                    return RedirectToAction("Index", "Post"); //Post con info del error
                }
            }

        }



    }
}

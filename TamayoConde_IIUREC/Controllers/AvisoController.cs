using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamayoConde_IIUREC.Models;

namespace TamayoConde_IIUREC.Controllers
{
    public class AvisoController : Controller
    {
        Aviso objAviso = new Aviso();
        Categoria objcategoria = new Categoria();
        // GET: Aviso
        public ActionResult RegistrarAviso(int categoria_id)
        {
            ViewBag.categoria_id = categoria_id;
            return View();
        }

        public ActionResult EditarAviso(int aviso_id)
        {
            ViewBag.aviso = objAviso.AvisoDetalle(aviso_id);
            return View();
        }
        public ActionResult EliminarAviso(int aviso_id,int categoria_id)
        {
            return RedirectToAction("Index",new { categoria_id = categoria_id });
        }

        public ActionResult AvisoPorCategoria()
        {
            ViewBag.listarAvisoCategoria = objcategoria.ListarCategoriaAviso();
            return View();
        }

        public ActionResult ActualizarEstadoAviso(int aviso_id, int estado)
        {
            bool respuesta = objAviso.ActualizarEstadoAviso(aviso_id, estado);
            return Json(new { respuesta = respuesta });
        }
        [HttpPost]
        public ActionResult RegistrarAviso(Aviso obj)
        {
            string strDateTime = System.DateTime.Now.ToString("ddMMyyyyHHMMss");
            string ruta = "\\assets\\archivos\\" + strDateTime + obj.fileimagen.FileName;
            obj.fileimagen.SaveAs(Server.MapPath("~") + ruta);
            if(obj.tipo == "IMAGEN")
            {
                obj.imagen = ruta;
                obj.thumbail = ruta;
            }
            else
            {
                obj.urlvideo = ruta;
            }

            obj.RegistrarAviso(obj);
            return RedirectToAction("VerCategoria", "Categoria", new { categoria_id = obj.categoria_id });
        }

        [HttpPost]
        public ActionResult EditarAviso(Aviso obj)
        {
            var avisodetalle = obj.AvisoDetalle(obj.aviso_id);
            var rutaGuardada = "";
            if(avisodetalle.tipo == "IMAGEN")
            {
                rutaGuardada = avisodetalle.imagen;
            }
            else
            {
                rutaGuardada = avisodetalle.urlvideo;
            }
            if (System.IO.File.Exists(rutaGuardada))
            {
                System.IO.File.Delete(rutaGuardada);
            }


            string strDateTime = System.DateTime.Now.ToString("ddMMyyyyHHMMss");
            string ruta = "\\assets\\archivos\\" + strDateTime + obj.fileimagen.FileName;
            obj.fileimagen.SaveAs(Server.MapPath("~") + ruta);
            if (obj.tipo == "IMAGEN")
            {
                obj.imagen = ruta;
                obj.thumbail = ruta;
                obj.urlvideo = null;
            }
            else
            {
                obj.urlvideo = ruta;
                obj.imagen = null;
                obj.thumbail = null;
            }
            obj.EditarAviso(obj);

            return RedirectToAction("VerCategoria", "Categoria", new { categoria_id = obj.categoria_id });

        }

    }
}
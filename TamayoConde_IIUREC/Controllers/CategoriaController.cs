using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TamayoConde_IIUREC.Models;

namespace TamayoConde_IIUREC.Controllers
{
    
    public class CategoriaController : Controller
    {
        Categoria objcategoria = new Categoria();
        Aviso objAviso = new Aviso();
        // GET: Categoria
        public ActionResult Index()
        {
            ViewBag.listarCategoria = objcategoria.ListarCategorias();
            return View();
        }
        public ActionResult RegistrarCategoria()
        {
            return View();
        }

        public ActionResult EditarCategoria(int categoria_id)
        {
            ViewBag.categoria = objcategoria.CategoriaDetalle(categoria_id);
            return View();
        }

        public ActionResult VerCategoria(int categoria_id)
        {
            ViewBag.categoria_id = categoria_id;
            ViewBag.listarAviso = objAviso.ListarAvisos(categoria_id);
            return View();
        }

        public ActionResult ActualizarEstadoCategoria(int categoria_id,int estado)
        {
            bool respuesta = objcategoria.ActualizarEstadoCategoria(categoria_id, estado);
            return Json(new { respuesta = respuesta });
        }

        [HttpPost]
        public ActionResult RegistrarCategoria(Categoria obj)
        {
            bool respuesta = objcategoria.RegistrarCategoria(obj);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditarCategoria(Categoria obj)
        {
            bool respuesta = objcategoria.EditarCategoria(obj);
            return RedirectToAction("Index");
        }

        public ActionResult EliminarCategoria(int categoria_id)
        {
            bool respuesta = objcategoria.EliminarCategoria(categoria_id);
            return RedirectToAction("Index");
        }
    }
}
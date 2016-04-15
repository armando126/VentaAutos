using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Entity;
using ProyectoAutos.Models;

namespace ProyectoAutos.Controllers
{
    public class PedidoController : Controller
    {
        private DB_AUTOS db = new DB_AUTOS();
        //
        // GET: /Pedido/
        public ActionResult Nuevo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.auto.Find(id);
            Usuario usuario = db.usuario.Find((int)Session["idUsuario"]);
            Pedido pedido = new Pedido();
            pedido.auto = auto;
            pedido.usuario = usuario;
            return View(pedido);
        }

        public ActionResult Ordenar(int? id){
            Auto auto = db.auto.Find(id);
            Usuario usuario = db.usuario.Find((int)Session["idUsuario"]);
            Pedido pedido = new Pedido();
            pedido.idAuto = (int)id;
            pedido.idUsuario = usuario.idUsuario;
            pedido.usuario = usuario;
            pedido.auto = auto;
            auto.estado = 1;
            db.Entry(auto).State = EntityState.Modified;
            db.pedido.Add(pedido);
            db.SaveChanges();
            return RedirectToAction("../Home/Index");
        }

        public ActionResult MisPedidos()
        {

            return View(db.pedido.ToList());
        }
        public ActionResult VerPedidos(int?  id)

        {
            Usuario u = db.usuario.Find(id);
            ViewBag.usuario = u.nombreUsuario;
            ViewBag.id = u.idUsuario;
            return View(db.pedido.ToList());
        }
	}
}
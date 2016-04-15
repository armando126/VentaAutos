using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAutos.Models;

namespace ProyectoAutos.Controllers
{
    public class CuentaController : Controller
    {
        private DB_AUTOS db = new DB_AUTOS();
        //
        // GET: /Cuenta/
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var log = db.usuario.FirstOrDefault(u => u.user == usuario.user && u.password == usuario.password);
            if (log != null)
            {
                Session["idUsuario"] = log.idUsuario;
                Session["nombreUsuario"] = log.nombreUsuario;
                Session["usuarioUsuario"] = log.user;
                return VerificarSesion();
            }
            else
            {
                ModelState.AddModelError("","Verifique sus credenciales.");
            }
            return View();
        }

        public ActionResult VerificarSesion()
        {
            if (Session["idUsuario"] != null)
            {
                if (Session["usuarioUsuario"].Equals("admin"))
                {
                    return RedirectToAction("../Admin/Index");
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
                
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        public ActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var existe = db.usuario.FirstOrDefault(u => u.user == usuario.user);
                if (existe == null) { 
                var rol = db.rol.FirstOrDefault(r => r.idRol == 2);
                usuario.rol = rol;
                db.usuario.Add(usuario);
                db.SaveChanges();
                ViewBag.mensaje = "Usuario "+usuario.nombreUsuario+" ha sido registrado correctamente.";
                }
                else
                {
                    ViewBag.mensaje = "Usuario " + usuario.user + " ya esta registrado.";
                }
            }
            return View();
        }

        public ActionResult Salir()
        {
            Session.Remove("idUsuario");
            Session.Remove("nombreUsuario");
            Session.Remove("usuarioUsuario");
            return RedirectToAction("Login");
        }

        public ActionResult Perfil()
        {
            int id = (int) Session["idUsuario"];
            Usuario usuario = db.usuario.Find(id);
            return View(usuario);
        }
	}
}
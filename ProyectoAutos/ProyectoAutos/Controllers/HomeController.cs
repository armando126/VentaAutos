using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAutos.Models;

namespace ProyectoAutos.Controllers
{
    public class HomeController : Controller
        
    {
        private DB_AUTOS db = new DB_AUTOS();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(db.auto.ToList());
        }
	}
}
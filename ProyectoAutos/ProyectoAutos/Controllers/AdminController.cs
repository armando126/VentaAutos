using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoAutos.Models;
using System.Data.Entity.Infrastructure;

namespace ProyectoAutos.Controllers
{
    public class AdminController : Controller
    {
        private DB_AUTOS db = new DB_AUTOS();

        // GET: /Admin/
        public ActionResult Index()
        {
            ViewBag.numAutos = db.auto.Count();
            ViewBag.numUsuario = db.usuario.Count();
            return View(db.auto.ToList());
        }

        // GET: /Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.auto.Find(id);
            if (auto == null)
            {
                return HttpNotFound();
            }
            return View(auto);
        }

        // GET: /Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idAuto,nombreAuto,marca,modelo,precio")] Auto auto, HttpPostedFileBase archivo)
        {
            if (ModelState.IsValid)
            {
                if (archivo != null && archivo.ContentLength > 0)
                {
                    var imagen = new Archivo
                    {
                        nombreArchivo = System.IO.Path.GetFileName(archivo.FileName),
                        tipo = FileType.Imagen,
                        ContentType = archivo.ContentType
                        
                    };
                    using(var read = new System.IO.BinaryReader(archivo.InputStream))
                    {
                        imagen.contenido = read.ReadBytes(archivo.ContentLength);
                    };
                    auto.archivos = new List<Archivo> { imagen };
                    auto.estado = 0;
                    
                }                
                db.auto.Add(auto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(auto);
        }

        // GET: /Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.auto.Find(id);
            if (auto == null)
            {
                return HttpNotFound();
            }
            return View(auto);
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, HttpPostedFileBase archivo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.auto.Find(id);
            if (TryUpdateModel(auto, "", new String[] { "idAuto,nombreAuto,marca,modelo,precio" }))
            {
                try{
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        if (auto.archivos.Any(a => a.tipo == FileType.Imagen))
                        {
                            db.archivo.Remove(auto.archivos.First(a => a.tipo == FileType.Imagen));
                        }
                        var imagen = new Archivo
                        {
                            nombreArchivo = System.IO.Path.GetFileName(archivo.FileName),
                            tipo = FileType.Imagen,
                            ContentType = archivo.ContentType

                        };
                        using (var read = new System.IO.BinaryReader(archivo.InputStream))
                        {
                            imagen.contenido = read.ReadBytes(archivo.ContentLength);
                        };
                        auto.archivos = new List<Archivo> { imagen };
                    }
                    db.Entry(auto).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("","Error modificando el auto");
                }
                
            }
            return View(auto);
            
        }

        // GET: /Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.auto.Find(id);
            if (auto == null)
            {
                return HttpNotFound();
            }
            return View(auto);
        }

        // POST: /Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auto auto = db.auto.Find(id);
            db.auto.Remove(auto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

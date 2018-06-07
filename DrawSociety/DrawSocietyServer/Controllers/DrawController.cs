using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrawSocietyServer.Models;

namespace DrawSocietyServer.Controllers
{
    public class DrawController : Controller
    {
        private DrawSocietyServerContext db = new DrawSocietyServerContext();

        // GET: Draw
        public ActionResult Index()
        {
            return View(db.Shapes.ToList());
        }

        // GET: Draw/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shape shape = db.Shapes.Find(id);
            if (shape == null)
            {
                return HttpNotFound();
            }
            return View(shape);
        }

        // GET: Draw/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Draw/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type")] Shape shape)
        {
            if (ModelState.IsValid)
            {
                db.Shapes.Add(shape);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shape);
        }

        // GET: Draw/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shape shape = db.Shapes.Find(id);
            if (shape == null)
            {
                return HttpNotFound();
            }
            return View(shape);
        }

        // POST: Draw/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Type")] Shape shape)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shape).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shape);
        }

        // GET: Draw/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shape shape = db.Shapes.Find(id);
            if (shape == null)
            {
                return HttpNotFound();
            }
            return View(shape);
        }

        // POST: Draw/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Shape shape = db.Shapes.Find(id);
            db.Shapes.Remove(shape);
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

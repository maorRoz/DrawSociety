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

        // GET: Draw
        public ActionResult Index(string board)
        {
            if (board == null)
            {

            }
            return View();
        }

        //[HttpPost]
        public ActionResult CreateShape(string color,object[] edges)
        {
            return RedirectToAction("index");
        }

        // GET: Draw/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
               // return HttpNotFound();
            return View();
        }

        // POST: Draw/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Color")] Shape shape)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(shape);
        }

        // GET: Draw/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: Draw/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}

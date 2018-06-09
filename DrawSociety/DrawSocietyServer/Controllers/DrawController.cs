using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using DrawSocietyServer.DrawSocietyData;
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
                return RedirectToAction("index",new {board = "home"});
            }
            return View(new User{Board = board});
        }

        [HttpPost]
        public ActionResult CreateShape(string color,string board,object[] edges)
        {
            if (edges != null)
            {
                ShapesData.AddShape(new Shape {Color = color, Board = board}, Edge.ParseJsonArray(edges));
            }

            return RedirectToAction("index");
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

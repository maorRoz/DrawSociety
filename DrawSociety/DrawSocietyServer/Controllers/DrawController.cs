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
        public ActionResult Index(string board,string username)
        {
            if (board == null)
            {
                return RedirectToAction("index",new {board = "home",username});
            }

            if (username != null)
            {
                return RedirectToAction("DrawBoard", new {board, username});
            }
            return View(new User { Board = board});
        }

        public ActionResult DrawBoard(string board,string username)
        {
            if (username == null || board == null)
            {
                return RedirectToAction("Index", new {board,username});
            }

            ViewBag.isOwner = BoardData.CheckOwnershipOrCreate(board,username);

            return View(new User { Board = board, Address = username });
        }

        [HttpPost]
        public ActionResult CreateShape(string color,string board,string username,object[] edges)
        {
            if (edges != null)
            {
                ShapesData.AddShape(new Shape {Color = color, Board = board}, Edge.ParseJsonArray(edges));
            }

            return RedirectToAction("DrawBoard",new{board,username});
        }

        public ActionResult DeleteLatestShape(string board,string username)
        {
            ShapesData.RemoveLatestShape(board);
            return RedirectToAction("DrawBoard", new { board, username });
        }

        public ActionResult WipeBoard(string board,string username)
        {
            ShapesData.RemoveBoard(board);
            return RedirectToAction("DrawBoard", new { board, username });
        }


    }
}

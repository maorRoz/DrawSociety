﻿using System;
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
            MemberData.SetIntervalMaxShape();
            MemberData.UpdateMaxShapeIfNeeded();
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

        public ActionResult SubmitMember(string board, string username)
        {
            if (username == null || board == null)
            {
                return RedirectToAction("Index", new { board, username });
            }
            MemberData.AddMemberIfNotExist(username);
            return RedirectToAction("DrawBoard", new { board, username });
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

        public ActionResult AdminControl(string username)
        {
            if (username != "Admin")
            {
                return RedirectToAction("Index", new {username});
            }

            ViewBag.Items = BoardData.GetAllBoards();
            return View(new User { Address = username });

        }

        public ActionResult SubmitNewMaxShape(int maxSlotEntry)
        {
            MemberData.UpdateMembersShapesSlots(maxSlotEntry);
            return RedirectToAction("AdminControl", new {username = "Admin"});
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

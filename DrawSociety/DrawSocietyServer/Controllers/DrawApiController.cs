using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DrawSocietyServer.DrawSocietyData;

namespace DrawSocietyServer.Controllers
{
    public class DrawApiController : ApiController
    {
        
        public IHttpActionResult GetShapes(string board)
        {
            var shapes = ShapesData.GetShapes(board);
            return Ok(shapes);
        }

        public IHttpActionResult GetMaxShape(string username)
        {
            var maxShape = MemberData.GetMemberShapeSlot(username);
            return Ok(maxShape);
        }

        public IHttpActionResult PostDecreaseMaxShape(string username)
        {
            MemberData.DecreaseMemberShapeSlot(username);
            return Ok();
        }

        public IHttpActionResult GetShapeEdges(int shapeId)
        {
            var edges = ShapesData.GetShapeEdges(shapeId);
            if (edges.Length == 0)
            {
                return NotFound();
            }
            return Ok(edges);
        }
    }
}

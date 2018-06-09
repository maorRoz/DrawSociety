using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrawSocietyServer.Models
{
    public class Edge
    {
        public int ShapeId { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public static Edge[] ParseJsonArray(object[] rawEdgesData)
        {
            var parsedEdges = new List<Edge>();
            foreach (string edgeData in rawEdgesData)
            {
                var parsedData = edgeData.Split(new[] {"{\"startX\":", ",\"startY\":", ",\"endX\":", ",\"endY\":","}" }, StringSplitOptions.RemoveEmptyEntries);
                var edge = new Edge
                {
                    StartX = int.Parse(parsedData[0]),
                    StartY = int.Parse(parsedData[1]),
                    EndX = int.Parse(parsedData[2]),
                    EndY = int.Parse(parsedData[3])
                };

                parsedEdges.Add(edge);

            }
            return parsedEdges.ToArray();
        }
    }
}
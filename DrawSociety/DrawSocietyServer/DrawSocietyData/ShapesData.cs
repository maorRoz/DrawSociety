using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrawSocietyServer.Models;

namespace DrawSocietyServer.DrawSocietyData
{
    public static class ShapesData
    {
        public static void AddShape(Shape shape,Edge[] edges)
        {
            DrawSocietyDataLayer.Instance.InsertTable("Shapes","Id,Color,BoardUrl",
                new[]{"@idParam","@colorParam","@boardParam"},
                new object[]{shape.Id,shape.Color,shape.BoardUrl});
            AddEdges(shape.Id,edges);
        }

        public static Shape[] GetShapes()
        {
            var shapes = new List<Shape>();
            using (var dbReader = DrawSocietyDataLayer.Instance.SelectFromTable("Shapes", "*"))
            {
                while (dbReader.Read())
                {
                    var shape = new Shape
                    {
                        Id = dbReader.GetInt32(0),
                        Color = dbReader.GetString(1),
                        BoardUrl = dbReader.GetString(2)
                    };
                    shapes.Add(shape);
                }
            }
            return shapes.ToArray();
        }

        private static void AddEdges(int shapeId,Edge[] edges)
        {
            foreach (var edge in edges)
            {
                DrawSocietyDataLayer.Instance.InsertTable("Edges",
                    "ShapeId,StartX,StartY,EndX,EndY",
                    new[] {"@idParam", "@startXParam", "@startYParam","@endXParam","endYParam"},
                    new object[] { shapeId, edge.StartX,edge.StartX,edge.EndX,edge.EndY});
            }
        }

        public static Edge[] GetShapeEdges(Shape shape)
        {
            var edges = new List<Edge>();
            using (var dbReader = DrawSocietyDataLayer.Instance.SelectFromTableWithCondition("Edges", "*","ShapeId = "+shape.Id))
            {
                while (dbReader.Read())
                {
                    var edge = new Edge
                    {
                        ShapeId = dbReader.GetInt32(0),
                        StartX = dbReader.GetInt32(1),
                        StartY = dbReader.GetInt32(2),
                        EndX = dbReader.GetInt32(3),
                        EndY = dbReader.GetInt32(4)
                    };
                    edges.Add(edge);
                }
            }
            return edges.ToArray();
        }
    }
}
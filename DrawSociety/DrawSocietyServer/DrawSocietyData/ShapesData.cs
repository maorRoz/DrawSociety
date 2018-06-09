using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrawSocietyServer.Models;

namespace DrawSocietyServer.DrawSocietyData
{
    public static class ShapesData
    {
        private static int maxShapeId;

        private static void GetMaxShapeId()
        {
            if (maxShapeId == 0)
            {
                using (var dbReader = DrawSocietyDataLayer.Instance.FreeStyleSelect("Select Max(Id) From Shapes"))
                {
                    if (dbReader.Read() && !dbReader.IsDBNull(0))
                    {
                        maxShapeId = dbReader.GetInt32(0);
                    }
                }
            }

            maxShapeId++;
        }
        public static void AddShape(Shape shape,Edge[] edges)
        {
            GetMaxShapeId();
            DrawSocietyDataLayer.Instance.InsertTable("Shapes","Id,Color,Board",
                new[]{"@idParam","@colorParam","@boardParam"},
                new object[]{maxShapeId,shape.Color,shape.Board});
            AddEdges(maxShapeId,edges);
        }

        public static Shape[] GetShapes(string board)
        {
            var shapes = new List<Shape>();
            using (var dbReader = DrawSocietyDataLayer.Instance.SelectFromTableWithCondition("Shapes", "*","Board = '"+board+"'"))
            {
                while (dbReader.Read())
                {
                    var shape = new Shape
                    {
                        Id = dbReader.GetInt32(0),
                        Color = dbReader.GetString(1),
                        Board = dbReader.GetString(2)
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
                    new[] {"@idParam", "@startXParam", "@startYParam","@endXParam","@endYParam"},
                    new object[] { shapeId, edge.StartX,edge.StartY,edge.EndX,edge.EndY});
            }
        }

        public static Edge[] GetShapeEdges(int shapeId)
        {
            var edges = new List<Edge>();
            using (var dbReader = DrawSocietyDataLayer.Instance.SelectFromTableWithCondition("Edges", "*","ShapeId = "+shapeId))
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

        public static void RemoveBoard(string board)
        {
            DrawSocietyDataLayer.Instance.DeleteFromTable("Shapes","Board = '"+board+"'");
        }

        public static void RemoveLatestShape(string board)
        {
            DrawSocietyDataLayer.Instance.FreeStyleExecute
                ("DELETE MAX(Id) FROM Shapes WHERE Board = '"+board+"'");
        }

    }
}
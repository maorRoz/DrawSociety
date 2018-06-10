using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrawSocietyServer.Models;

namespace DrawSocietyServer.DrawSocietyData
{
    public static class BoardData
    {
        public static Board[] GetAllBoards()
        {
            var boards = new List<Board>();
            using (var dbReader =
                DrawSocietyDataLayer.Instance.SelectFromTable("Boards", "*"))
            {
                while (dbReader.Read())
                {
                    boards.Add(new Board
                        { Url = dbReader.GetString(0), HostAddress = dbReader.GetString(1)});
                }
            }

            return boards.ToArray();
        }
        public static bool CheckOwnershipOrCreate(string board, string hostAddress)
        {
            if (IsBoardExist(board))
            {
                return IsOwner(board, hostAddress);
            }

            CreateBoard(board,hostAddress);
            return true;
        }

        private static bool IsBoardExist(string board)
        {
            using (var dbReader =
                DrawSocietyDataLayer.Instance.
                    SelectFromTableWithCondition("Boards", "*", "Url = '" + board + "'"))
            {
                return dbReader.Read() && !dbReader.IsDBNull(0);
            }
        }

        private static bool IsOwner(string board, string hostAddress)
        {
            if (hostAddress == "Admin")
            {
                return true;
            }
            using (var dbReader =
                DrawSocietyDataLayer.Instance.SelectFromTableWithCondition("Boards", "*",
                    "Url = '" + board + "' AND HostAddress = '" + hostAddress + "'"))
            {
                return dbReader.Read() && !dbReader.IsDBNull(0);
            }
        }

        private static void CreateBoard(string board,string hostAddress)
        {
            DrawSocietyDataLayer.Instance.InsertTable("Boards","Url,HostAddress",
                new []{"@urlParam","@addressParam"},new object[]{board,hostAddress});
        }
    }
}
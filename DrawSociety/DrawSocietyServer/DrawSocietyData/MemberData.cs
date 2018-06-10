using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DrawSocietyServer.DrawSocietyData
{
    public static class MemberData
    {
        public static void AddMemberIfNotExist(string address)
        {
            if (IsMemberExist(address))
            {
                return;
            }
            DrawSocietyDataLayer.Instance.FreeStyleExecute("INSERT INTO Member " +
                "(Address,RestrictedShapes)" +
                " VALUES ('"+address+"',(SELECT MaxShapeSlot FROM MaxShape))");
        }

        private static bool IsMemberExist(string address)
        {
            using (var dbReader = DrawSocietyDataLayer.Instance.SelectFromTableWithCondition("Member", "Address",
                 "Address = '" + address + "'"))
            {
                return dbReader.Read() && !dbReader.IsDBNull(0);
            }
        }

        public static void RefreshMembersShapesSlots(int maxSlot)
        {
            RefreshGlobalMaxShape(maxSlot);
            DrawSocietyDataLayer.Instance.UpdateTable("Member","Address != 'Admin'",new[]{"RestrictedShapes"},
                new []{"@restrictedParam"},new object[]{maxSlot});
        }

        private static void RefreshGlobalMaxShape(int maxSlot)
        {
            DrawSocietyDataLayer.Instance.UpdateTable("MaxShape",null, new[] { "MaxShapeSlot","DateSubmitted" },
                new[] { "@maxParam","@dateParam" }, new object[] { maxSlot,DateTime.Now });
        }

        public static void DecreaseMemberShapeSlot(string address)
        {
            DrawSocietyDataLayer.Instance.FreeStyleExecute(
                "UPDATE Member SET RestrictedShapes = RestrictedShapes - 1 WHERE Address ='" + address+"' AND Address != 'Admin'");
        }

        public static int GetMemberShapeSlot(string address)
        {
            using (var dbReader = DrawSocietyDataLayer.Instance.SelectFromTableWithCondition("Member",
                "RestrictedShapes",
                "Address = '" + address + "'"))
            {
                if (dbReader.Read())
                {
                    return dbReader.GetInt32(0);
                }
            }

            return -1;
        }
    }
}
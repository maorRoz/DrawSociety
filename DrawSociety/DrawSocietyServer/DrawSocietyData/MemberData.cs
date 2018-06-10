using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Timers;
using System.Web;

namespace DrawSocietyServer.DrawSocietyData
{
    public static class MemberData
    {
        private static Timer intervalMaxShape;

        public static void SetIntervalMaxShape()
        {
            if (intervalMaxShape != null){ return;}
            intervalMaxShape = new Timer(1000 * 60 * 60 * 24);
            intervalMaxShape.Elapsed += CheckUpdateMaxShape;
            intervalMaxShape.Enabled = true;
        }

        private static void CheckUpdateMaxShape(object source, ElapsedEventArgs e)
        {
            UpdateMaxShapeIfNeeded();
        }

        public static void UpdateMaxShapeIfNeeded()
        {
            if (DateTime.Now > LastSubmittedMaxShapeDate())
            {
                RefreshMemberShapesSlots();
            }
        }

        private static DateTime LastSubmittedMaxShapeDate()
        {
            using (var dbReader = DrawSocietyDataLayer.Instance.SelectFromTable("MaxShape", "DateSubmitted"))
            {
                dbReader.Read();
                return dbReader.GetDateTime(0);
            }

        }

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

        public static void RefreshMemberShapesSlots()
        {
            RefreshGlobalMaxShape();
            DrawSocietyDataLayer.Instance.FreeStyleExecute
                ("Update Member SET RestrictedShapes = " +
                 "(SELECT MaxShapeSlot FROM MaxShape) WHERE Address != 'Admin'");
        }

        private static void RefreshGlobalMaxShape()
        {
            DrawSocietyDataLayer.Instance.UpdateTable("MaxShape", null, new[] { "DateSubmitted" },
                new[] { "@dateParam" }, new object[] { DateTime.Now });
        }

        public static void UpdateMembersShapesSlots(int maxSlot)
        {
            UpdateGlobalMaxShape(maxSlot);
            DrawSocietyDataLayer.Instance.UpdateTable("Member","Address != 'Admin'",new[]{"RestrictedShapes"},
                new []{"@restrictedParam"},new object[]{maxSlot});
        }

        private static void UpdateGlobalMaxShape(int maxSlot)
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawSocietyServer.DrawSocietyData
{
    public class MemeberData
    {
        public static void AddMember(string address,int restrictedShapes)
        {
            DrawSocietyDataLayer.Instance.InsertTable("Member", "Address,RestrictedShapes",
                new[] { "@addressParam", "@restrictedParam"},
                new object[] { address,restrictedShapes });
        }

        public static void RefreshMembersShapesSlots(int maxSlot)
        {
            DrawSocietyDataLayer.Instance.UpdateTable("Member",null,new[]{"RestrictedShapes"},
                new []{"@restrictedParam"},new object[]{maxSlot});
        }

        public static void DecreaseMemberShapeSlot(string address)
        {
            DrawSocietyDataLayer.Instance.FreeStyleExecute(
                "UPDATE Member SET RestrictedShapes = RestrictedShapes - 1 WHERE Address ='" + address+"'");
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
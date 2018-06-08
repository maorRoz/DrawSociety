using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawSocietyServer.Models
{
    public class User
    {
        public string Board { get; set; }
        public string Address { get; set; }
        public int AvailableShapes { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace DrawSocietyServer.Models
{
    public class Shape
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Board { get; set; }
    }
}
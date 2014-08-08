using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RestFulNetDroid.Models
{
    public class Treasure
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public String Url { get; set; }


        public List<Coordinate> Coordinates { get; set; }

    }

    public class Coordinate
    {

        public int Id { get; set; }
        public double Data { get; set; }

        [ForeignKey("Treasure")] //explicit definition of Foreign key  mapped to the Treasure Table
       
        public String TreasureId { get; set; }
         [JsonIgnore]                             //prevents a circular error in json serialization, ie prevents treasure => coords => treasure
        public Treasure Treasure { get; set; }
    }

   // public class TreasureEntityMapper : IEntityMapper
}

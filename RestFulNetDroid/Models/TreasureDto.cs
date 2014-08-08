using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Routing.Constraints;

namespace RestFulNetDroid.Models
{
    public class TreasureDto
    {
        public String address { get; set; }
        public List<Double> coordinates { get; set; }
        public String Id { get; set; }
        public String Name { get; set; }
        

        public String url { get; set; }
        

        //public List<Double> Coordinates { get; set; }

        public TreasureDto(Treasure treasure)
        {
            
            this.Id = treasure.Id;
            this.Name = treasure.Name;
            this.address = treasure.Address;
            this.url = treasure.Url;
            var Coordinates = treasure.Coordinates;  //list of co-ordinates
            coordinates = new List<double>();
            foreach (var data in Coordinates)

                coordinates.Add(data.Data);
        }

            // need lamda to form a list of doubles from coordinate data

        

    
    };
}
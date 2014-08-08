using System;
using System.Collections.Generic;
using System.Web.Mvc.Routing.Constraints;

namespace RestFulNetDroid.Models
{
    public class TreasureDto
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public String Url { get; set; }
        public List<Double> Coordinates { get; set; }

        //public List<Double> Coordinates { get; set; }

        public TreasureDto(Treasure treasure)
        {
            
            this.Id = treasure.Id;
            this.Name = treasure.Name;
            this.Address = treasure.Address;
            this.Url = treasure.Url;
            // need lamda to form a list of doubles from coordinate data

        }

    
    };
}
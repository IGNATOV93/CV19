using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }

        public Point Location { get; set; }

        public IEnumerable<ConfirmedCount> Counts { get; set; } 
    }

    internal class CountryInfo:PlaceInfo
    {
   public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
    }
    internal class ProvinceInfo :PlaceInfo
    {

    }
        internal struct ConfirmedCount
        {
            public DateTime Data { get; set; }
         
           public int Count { get; set; }
        }
    internal struct DataPoints
    {
        public double Xvalue { get; set; }
        public double Yvalue { get; set; }

    }
    
}

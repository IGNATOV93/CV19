using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows;


namespace CV19.Infrastructure.Converters
{
    [ValueConversion(typeof(Point),typeof(string))]
    internal class LocationPointToStr:Converter
    {
        public override object Convert(object value, Type t, object p, CultureInfo c)
        {
            if(!(value is Point point)) return null;
            return $"Lat:{point.X};Lon:{point.Y}";
        }

        public override object ConvertBack(object value, Type y, object p, CultureInfo c)
        {
            if (!(value is string str)) return null;
           var components = str.Split(';');
           var lat_str = components[0].Split(':')[1];
           var lon_str = components[1].Split(':')[1];

           var lat = double.Parse(lat_str);
           var lon = double.Parse(lon_str);
           return new Point(lat, lon);
        }
    }
}

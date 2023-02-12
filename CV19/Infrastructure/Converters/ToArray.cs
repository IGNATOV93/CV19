using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Infrastructure.Converters
{
    internal class ToArray:MultiConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c) => vv;
        //public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c)=> v as object[];
    }
}

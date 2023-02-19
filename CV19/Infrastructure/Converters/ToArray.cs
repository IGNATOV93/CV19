using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(ToArray))]
    internal class ToArray:MultiConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c)
        {
            var collection = new CompositeCollection();
            foreach (var v in vv)
                collection.Add(v);
            return collection;
        }
        //public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c)=> v as object[];
    }
}

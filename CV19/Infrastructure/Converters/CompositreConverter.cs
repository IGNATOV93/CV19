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
    [MarkupExtensionReturnType(typeof(CompositreConverter))]
    internal class CompositreConverter:Converter
    {
        [ConstructorArgument("First")]
        public IValueConverter First { get; set; }
        [ConstructorArgument("Second ")]
        public IValueConverter Second { get; set; }

        public CompositreConverter() { }

        public CompositreConverter(IValueConverter First) =>this.First = First;
       

        public CompositreConverter(IValueConverter First,IValueConverter Second): this(First)=>this.Second = Second;
        

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var result1 = First?.Convert(v, t, p, c)?? v;
            var result2 = Second?.Convert(result1, t, p, c) ?? result1;
            return result2;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var result2 = Second?.ConvertBack(v, t, p, c) ?? v;
            var result1 = First?.ConvertBack(result2, t, p, c) ?? result2;
            return result1;
        }
    }

    
}

using System.Windows.Ink;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Axes;
using System.Windows.Markup;
using OxyPlot.Series;

namespace CV19.ViewModels
{
    [MarkupExtensionReturnType(typeof(MyModelNew))]
    internal class MyModelNew : ViewModel
    {
        public PlotModel MyModel { get; private set; }

        public MyModelNew()
        {

            MyModel = new PlotModel { Title = "График!" };
            //MyModel.Series.Add(new FunctionSeries(Math.Cos,0,10,0.1,"cos(x)"));

            MyModel.Axes.Add( new LinearAxis { Position = AxisPosition.Left, Title = "Число", MajorGridlineStyle =(LineStyle.Solid),MinorGridlineStyle = (LineStyle.Dash)});
            MyModel.Axes.Add(new DateTimeAxis{ Position = AxisPosition.Bottom, Title = "Дата", MajorGridlineStyle = (LineStyle.Solid), MinorGridlineStyle = (LineStyle.Dash) });
            
        }
    }
}

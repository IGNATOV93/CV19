using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV19.ViewModels;
using CV19.ViewModels.Base;

namespace CV19.View
{
    internal class CountriesStatisticViewModel:ViewModel
    {
        private readonly MainWindowViewModel _MainModel;

        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            _MainModel = _MainModel;
        }
    }
}

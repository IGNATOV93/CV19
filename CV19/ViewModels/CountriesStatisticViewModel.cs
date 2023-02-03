using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels;
using CV19.ViewModels.Base;

namespace CV19.View
{
    internal class CountriesStatisticViewModel:ViewModel
    {
        private DataService _DataService;

        private  MainWindowViewModel _MainModel { get; }

        #region Contries : IEnumerable<CountryInfo> - Статистика по странам
        /// <summary> Статистика по странам </summary>
        private IEnumerable<CountryInfo> _Contries;


        /// <summary> Статистика по странам</summary>
        public IEnumerable<CountryInfo> Contries
        {
            get=> _Contries;
          private  set => Set(ref _Contries, value);
        }

        #endregion

        #region Команды
        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Contries = _DataService.GetData();
        }

        #endregion

        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            MainModel = MainModel;
            _DataService = new DataService();

            #region Команды

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}

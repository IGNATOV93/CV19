using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region TestDataPoints :Ienumerable<DataPoint> -DESCRIPTION - Тестовый набор данных для визуализации графиков
        /// <summary>DESCRIPTION   /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;
        public IEnumerable<DataPoint> TestDataPoints 
        { get => _TestDataPoints;
            set=>Set(ref _TestDataPoints , value); 
        }
        /// <summary> DESCRIPTION  /// </summary>
        #endregion
        #region Заголовок окна
        private string _Title="Анализ статистики CV19";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            set=> Set(ref _Title, value);
        }

        #endregion
        #region Status: string -Статус программы
        /// <summary>
        /// Статус программы
        /// </summary>
        private string _Status="Готов!";
        public string Status { get=> _Status; set=>Set(ref _Status, value); }
        #endregion
        #region Команды
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get;}
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
         Application.Current.Shutdown();
        }
        
        #endregion
        #endregion
        public MainWindowViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion
            var data_points= new List<DataPoint>((int)(360/0.1));
            for (var x= 0d;x<=360;x+=0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x* to_rad);   
                data_points.Add(new DataPoint(x,y));
            }
             TestDataPoints= data_points;
        }

    }
}

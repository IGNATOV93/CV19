using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using OxyPlot;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xaml.Schema;
using CV19.Models;
using DataPoint = CV19.Models.DataPoint;
using OxyPlot.Series;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using CV19.Models.Decanat;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        //-----------------------------------------------------------------------------------------------
        public ObservableCollection<Group> Groups { get; }
        #region выбор вкладки
        /// <summary>   Номер выбранной вкладки     /// </summary>
        private int _SelectedPageIndex;
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);

        }


        #endregion
        #region TestDataPoints :Ienumerable<DataPoint> -DESCRIPTION - Тестовый набор данных для визуализации графиков
        /// <summary>DESCRIPTION   /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;
        public IEnumerable<DataPoint> TestDataPoints
        { get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        /// <summary> DESCRIPTION  /// </summary>
        #endregion
        #region Заголовок окна
        private string _Title = "Анализ статистики CV19";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion
        #region Status: string -Статус программы
        /// <summary>
        /// Статус программы
        /// </summary>
        private string _Status = "Готов!";
        public string Status { get => _Status; set => Set(ref _Status, value); }
        #endregion
        //====================================================================================================
        #region Команды
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion
        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if(p is null) return;
            SelectedPageIndex+=Convert.ToInt32(p);
        }

        #endregion
        //=====================================================================================================
        public MainWindowViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            #endregion
            //var data_points = new List<DataPoint>((int)(360 / 0.1));
            MyModel = new PlotModel { Title = "График" };
            var line1 = new OxyPlot.Series.LineSeries()
            {
                Title = "Series",
                Color = OxyPlot.OxyColors.Blue

            };
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                //data_points.Add(new DataPoint { XValue = x, YValue = y });
                line1.Points.Add(new OxyPlot.DataPoint(x, y));
            }
            MyModel.Series.Add(line1);
            //TestDataPoints = data_points;

            var student_index = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name= $"Name{student_index}",
                Surname=$"Surname{student_index}",
                Patronymic= $"Patronymic{student_index++}",
                Birthday =  DateTime.Now,
                Rating = 0
                
            });
            var groups = Enumerable.Range(1, 20).Select(i => new Group
            { 
                Name = $"Группа{i}",
            Students = new ObservableCollection<Student>(students)
            }) ;

            Groups = new ObservableCollection<Group>(groups);
              
        }
        public PlotModel MyModel { get; private set; }
    }

} 

    


    


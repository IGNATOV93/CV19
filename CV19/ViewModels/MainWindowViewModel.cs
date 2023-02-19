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
using System.ComponentModel;
using CV19.Models.Decanat;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using CV19.View;
using Group = CV19.Models.Decanat.Group;

namespace CV19.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel : ViewModel
    {
        
        //-----------------------------------------------------------------------------------------------
      public  CountriesStatisticViewModel CountriesStatistic { get; }

        //-----------------------------------------------------------------------------------------------

        #region SelectedGroup : Group - Выбранная группа
        /// <summary>Выбрнанная группа /// </summary>
        private Group _SelectedGroup;
        /// <summary>Выбрнанная группа /// </summary>
        public Group SelectedGroup {
            get => _SelectedGroup;
            set
            {
                if (!Set(ref _SelectedGroup, value)) return ;
                _SelectedGroupStudents.Source = value?.Students;
               OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }
        #endregion

        #region SelectedGroupStudents
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        private void OnStudentFiltred(object Sender, FilterEventArgs E)
        {
            if (!(E.Item is Student student ))
            {
                E.Accepted = false;return;
            }

            var filter_text = _StudentFilterText;
            if(string.IsNullOrWhiteSpace(filter_text)) return;
            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                E.Accepted = false;
                return;
            }
            if(student.Name.Contains((filter_text))) return;
            if (student.Surname.Contains((filter_text))) return;
            if (student.Patronymic.Contains((filter_text))) return;
            E.Accepted = false;

        }
        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View; 
        #endregion

        #region StudentFilterText : string - Текст фильтра студентов
        /// <summary> Текст фильтра студентов </summary>
        private string _StudentFilterText;
        /// <summary> Текст фильтра студентов </summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText,value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
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

        public IEnumerable<Student> TeStudents => Enumerable.Range(1, App.IsDesingMode ? 10: 100_000)
            .Select(i => new Student
            {
              Name = $"Имя {i}",
              Surname = $"Фамилия{i}"
            });

        //====================================================================================================
        #region Команды
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            (RootObject as Window)?.Close();
           // Application.Current.Shutdown();
        }

        #endregion
        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }
       
        #endregion
        #endregion
        
        //=====================================================================================================
        public MainWindowViewModel()
        {
            CountriesStatistic = new CountriesStatisticViewModel(this);

            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
                // ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
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

        }

       
        public PlotModel MyModel { get; private set; }
    }

} 

    


    


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
using Group = CV19.Models.Decanat.Group;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        //-----------------------------------------------------------------------------------------------
        public ObservableCollection<Group> Groups { get; }
        public object[] CompositeCollection { get; }

        #region SelectedCompositeValue : object - Выбранный непонятный элемент
        /// <summary> Выбранный непонятный элемент</summary>
        private object _SelectedCompositeValue;
        ///<summary>Выбранный непонятный элемент</summary>
        public object SelectedCompositeValue { get => _SelectedCompositeValue; set => Set(ref _SelectedCompositeValue,value); }
        #endregion
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

        #region выбор вкладки
        /// <summary>   Номер выбранной вкладки     /// </summary>
        private int _SelectedPageIndex=1;
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);

        }
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

        public DirectoryViewModel DiskRootDir { get; }= new DirectoryViewModel("c:\\");
             
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
        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if(p is null) return;
            SelectedPageIndex+=Convert.ToInt32(p);
        }
        #endregion
        #region  CreateGroupCommand
        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;
        private void OnCreateGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion
        #region DeleteGroupCommand
        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommmandExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index= Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index<Groups.Count)
               SelectedGroup = Groups[group_index];
           
        }
        #endregion
        #endregion
        //=====================================================================================================
        public MainWindowViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommmandExecute);

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
            var data_list = new List<object>();

            data_list.Add("Hello world");
            data_list.Add(42);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);
            CompositeCollection = data_list.ToArray();

            _SelectedGroupStudents.Filter += OnStudentFiltred;
            //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name",ListSortDirection.Descending));
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        }

       
        public PlotModel MyModel { get; private set; }
    }

} 

    


    


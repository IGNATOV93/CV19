using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CV19.Models.Decanat;

namespace CV19
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void GroupCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Group group)) return;
            if(group.Name is null) return;

            var filter_text = GroupFilterText.Text;
            if(filter_text.Length==0) return;
            if(group.Name.Contains(filter_text)) return;
            if(group.Description != null && group.Description.Contains(filter_text)) return;
            e.Accepted = false;
        }

        private void OnGroupsFilterTextChanged(object sender, EventArgs e)
        {
            var  text_box = (TextBox)sender;
            var collection = (CollectionViewSource)text_box.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}

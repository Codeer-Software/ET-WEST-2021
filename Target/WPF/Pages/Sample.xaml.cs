using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace etwest.Pages
{
    /// <summary>
    /// Sample.xaml の相互作用ロジック
    /// </summary>
    public partial class Sample : UserControl
    {
        public Sample()
        {
            InitializeComponent();
            _listView.ItemsSource = GetListViewItems();


            var data = new ObservableCollection<Person>(
                Enumerable.Range(1, 10).Select(i => new Person
                {
                    Name = i % 2 == 0 ? "James Smith" + i : "Mary Johnson" + i,
                    Gender = i % 2 == 0 ? Gender.Men : Gender.Women,
                    AuthMember = i % 5 == 0,
                    Link = new Uri("https://github.com/Codeer-Software/TestAssistantPro.Manual"),
                }));
            // DataGridに設定する
            this.dataGrid.ItemsSource = data;
        }

        public List<object> GetListViewItems()
        {
            var listViewViewModels = new List<object>
            {
                new ListView1ViewModel()
                {
                    CheckBoxData = true,
                    ComboBoxData = 0,
                    TextData = "Text1",
                },
                new ListView2ViewModel()
                {
                    ComboBoxData = 2,
                    TextData = "Text2",
                    DateData = new DateTime(2020,8,1),
                },
                new ListView3ViewModel()
                {
                    TextData = "Text3",
                    DateData = new DateTime(2020,8,14),
                    SliderData = 10,
                },
                new ListView1ViewModel()
                {
                    CheckBoxData = false,
                    ComboBoxData = 3,
                    TextData = "Text4",
                },
            };
            return listViewViewModels;
        }
    }
}

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using etwest.Models;
using SelectionChangedEventArgs = etwest.Controls.SelectionChangedEventArgs;

namespace etwest.Pages
{
    /// <summary>
    /// Monitor.xaml の相互作用ロジック
    /// </summary>
    public partial class Monitor : UserControl
    {
        public Monitor()
        {
            InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            var strings = App.Client
                .Data
                .Where(d => args.Start <= d.DateTime && d.DateTime <= args.End)
                .Select(d => $"[{d.DateTime}] {d.Lux}");
            TextBox.Text = string.Join("\n", strings);
            if (args.Start != DateTime.MinValue)
            {
                pause.IsChecked = true;
            }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            chart.Clear();
            logName.Text = "";
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            App.Client.Logs.Add(new Log
            {
                Name = logName.Text,
                Content = TextBox.Text
            });
            chart.Clear();
            logName.Text = "";
        }
    }
}

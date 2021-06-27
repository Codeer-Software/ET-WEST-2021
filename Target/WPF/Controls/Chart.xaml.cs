using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using etwest.Models;

namespace etwest.Controls
{
    /// <summary>
    /// Chart.xaml の相互作用ロジック
    /// </summary>
    public partial class Chart : UserControl
    {
        private DateTime _start;
        private DateTime _end;
        private readonly List<Data> _data;

        public event SelectionChangedEventHandler SelectionChanged;

        #region Dependency Property

        public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register(
            "IsPaused", typeof(bool), typeof(Chart), new PropertyMetadata(default(bool), OnPauseChange));

        public bool IsPaused
        {
            get { return (bool)GetValue(IsPausedProperty); }
            set { SetValue(IsPausedProperty, value); }
        }

        private static void OnPauseChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                ((Chart)d).Reset();
            }
        }
        #endregion

        public Chart()
        {
            InitializeComponent();
            _data = new List<Data>();
            graph.Data = _data;
            App.Client.Data.CollectionChanged += Data_CollectionChanged;
        }

        private void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsPaused) return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _data.Add((Data)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    _data.RemoveAt(e.OldStartingIndex);
                    break;
            }
            graph.InvalidateVisual();
        }

        #region Event Handlers

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.MouseDevice.Capture((Grid)sender);
            _start = graph.GetValueForPixel(e.GetPosition(this).X);
            _end = graph.GetValueForPixel(e.GetPosition(this).X);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            _end = graph.GetValueForPixel(e.GetPosition(this).X);
            UpdateSelection();
        }

        void UpdateSelection()
        { 
            var startPixels = graph.GetPixelForValue(_start);
            var endPixels = graph.GetPixelForValue(_end);
            selection.Width = Math.Abs(startPixels - endPixels);
            ((TranslateTransform)selection.RenderTransform).X = Math.Min(startPixels, endPixels);
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs { Start = Min(_start, _end), End = Max(_start, _end) });
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.MouseDevice.Capture(null);
        }

        #endregion

        public void Clear()
        {
            selection.Width = 0;
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs { Start = DateTime.MinValue, End = DateTime.MinValue });
        }

        public void Reset()
        {
            Clear();
            _data.Clear();
            _data.AddRange(App.Client.Data);
            graph.InvalidateVisual();
        }

        public void Select(DateTime start, DateTime end)
        {
            var startPixels = graph.GetPixelForValue(start);
            var endPixels = graph.GetPixelForValue(end);
            selection.Width = Math.Abs(startPixels - endPixels);
            ((TranslateTransform)selection.RenderTransform).X = Math.Min(startPixels, endPixels);
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs { Start = Min(start, end), End = Max(start, end) });
        }

        #region Private Utilities

        private DateTime Max(DateTime d1, DateTime d2)
        {
            return d1 > d2 ? d1 : d2;
        }

        private DateTime Min(DateTime d1, DateTime d2)
        {
            return d1 < d2 ? d1 : d2;
        }

        #endregion
    }

    public class SelectionChangedEventArgs
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs args);

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using etwest.Models;

namespace etwest
{
    public class Graph : Control
    {
        private Pen _pen = new Pen(Brushes.PaleVioletRed, 1.2);

        public List<Data> Data { get; set; }

        public double PixelsPerSecond => ActualWidth / 500;

        public double GetPixelForValue(DateTime d)
        {
            return GetPixelForValue(d, Data.Last().DateTime);
        }

        public DateTime GetValueForPixel(double x)
        {
            if (Data.Count == 0) return DateTime.MaxValue;
            var ticksForPixel = (ActualWidth - x) / PixelsPerSecond * TimeSpan.TicksPerSecond;
            var latestTicks = Data.Last().DateTime.Ticks;
            var maxValue = latestTicks;
            var minValue = latestTicks - (500 * TimeSpan.TicksPerSecond);
            return new DateTime(Math.Min(maxValue, Math.Max(minValue, (long)(latestTicks - ticksForPixel))));
        }

        private double GetPixelForValue(DateTime d, DateTime reference)
        {
            var latestTicks = reference.Ticks;
            return ActualWidth - (latestTicks - d.Ticks) / (double)TimeSpan.TicksPerSecond * PixelsPerSecond;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Data == null || Data.Count == 0) return;
            var maxLux = Data.Select(a => a.Lux).Max();
            var pixelsPerLux = (ActualHeight * 0.95) / maxLux;

            var latestDateTime = Data.Last().DateTime;
            Point? oldPoint = null;
            drawingContext.PushClip(new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight)));
            foreach (var d in ((IEnumerable<Data>)Data).Reverse())
            {
                var x = GetPixelForValue(d.DateTime, latestDateTime);
                var y = ActualHeight - d.Lux * pixelsPerLux;
                if (oldPoint == null)
                {
                    oldPoint = new Point(x, y);
                }
                else
                {
                    var currentPoint = new Point(x, y);
                    drawingContext.DrawLine(_pen, oldPoint.Value, currentPoint);
                    oldPoint = currentPoint;
                }
                drawingContext.DrawEllipse(Brushes.PaleVioletRed, _pen, oldPoint.Value, 3, 3);
            }
        }
   }
}
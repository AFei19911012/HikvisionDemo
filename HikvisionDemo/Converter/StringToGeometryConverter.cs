using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace HikvisionDemo.Converter
{
    ///
    /// ----------------------------------------------------------------
    /// Copyright @BigWang 2023 All rights reserved
    /// Author      : BigWang
    /// Created Time: 2023/11/5 16:52:14
    /// Description :
    /// ----------------------------------------------------------------
    /// Version      Modified Time              Modified By     Modified Content
    /// V1.0.0.0     2023/11/5 16:52:14                     BigWang         首次编写         
    ///
    public class StringToGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ResourceDictionary geometry = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HikvisionDemo;component/Theme/Geometry.xaml")
            };

            List<string> names = new List<string>();
            foreach (object item in geometry.Keys)
            {
                names.Add((string)item);
            }

            List<Geometry> paths = new List<Geometry>();
            foreach (object item in geometry.Values)
            {
                paths.Add((Geometry)item);
            }

            int idx = names.IndexOf((string)value);
            PathGeometry path = new PathGeometry();
            path.AddGeometry(paths[idx]);
            path.FillRule = FillRule.Nonzero;
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
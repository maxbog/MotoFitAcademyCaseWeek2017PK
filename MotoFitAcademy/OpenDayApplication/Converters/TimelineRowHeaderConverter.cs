#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace OpenDayApplication.Converters
{
    public class TimelineRowHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetRowHeader((value as DataGridRow).GetIndex());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

        private string GetRowHeader(int index)
        {
            return string.Format("{0}:00", index + 8);
        }
    }
}

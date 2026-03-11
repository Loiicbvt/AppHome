using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace HomeAppLBO.Converters
{
    public sealed class BoolToThumbPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOn = (bool)value;

            if (isOn)
            {
                return 22; // position ON
            }
            else
            {
                return 0; // position OFF
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
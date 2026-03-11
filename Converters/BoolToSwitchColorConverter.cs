using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace HomeAppLBO.Converters
{
    public sealed class BoolToSwitchColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOn = (bool)value;

            if (isOn)
            {
                return Color.FromArgb("#FFD27F"); // jaune premium
            }
            else
            {
                return Color.FromArgb("#555555"); // gris Off
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}

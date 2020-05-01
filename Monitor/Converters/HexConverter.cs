using System;
using System.Globalization;
using System.Windows.Data;

namespace Monitor.Converters
{
    public class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case byte _: return $"0x{value:X2}";
                case ushort _: return $"0x{value:X4}";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueToParse = value?.ToString().Replace("0x", "");
            if (targetType == typeof(byte))
            {
                if (byte.TryParse(valueToParse, NumberStyles.HexNumber, null, out var result))
                {
                    return result;
                }
            }
            else if (targetType == typeof(ushort))
            {
                if (ushort.TryParse(valueToParse, NumberStyles.HexNumber, null, out var result))
                {
                    return result;
                }
            }

            return null;
        }
    }
}

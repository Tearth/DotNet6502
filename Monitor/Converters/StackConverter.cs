using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Monitor.Converters
{
    public class StackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi\deff0");
            builder.Append(@"{\fonttbl {\f0 Consolas;}}");
            builder.Append(@"{\colortbl;\red255\green255\blue255;\red150\green150\blue150;}");
            builder.Append(@"\fs18");

            var bytes = (byte[]) value;
            for (var i = bytes.Length - 1; i >= 0; i--)
            {
                builder.Append(@"\cf2 0x");
                builder.Append((i + 0x100).ToString("X2"));
                builder.Append(@": \cf1 0x");
                builder.Append(bytes[i].ToString("X2"));
                builder.Append(@"\line");
            }

            builder.Append(@"}");
            return builder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

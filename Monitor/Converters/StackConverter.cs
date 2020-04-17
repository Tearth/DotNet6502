using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Monitor.ViewModels;

namespace Monitor.Converters
{
    public class StackConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = (byte[]) values[0];
            var viewModel = (MainWindowViewModel) values[1];
                
            if (bytes == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi\deff0");
            builder.Append(@"{\fonttbl {\f0 Consolas;}}");
            builder.Append(@"{\colortbl;\red255\green255\blue255;\red150\green150\blue150;}");
            builder.Append(@"\fs18");

            for (var i = bytes.Length - 1; i >= 0; i--)
            {
                var realAddress = i + 0x100;
                var stackPointerAddress = viewModel.Registers.Sp + 0x100;

                builder.Append(@"\cf2 0x");
                builder.Append(realAddress.ToString("X4"));
                builder.Append(@": \cf1 0x");
                builder.Append(bytes[i].ToString("X2"));

                if (realAddress == stackPointerAddress)
                {
                    builder.Append(@" <-- SP");
                }

                builder.Append(@"\line");
            }

            builder.Append(@"}");
            return builder.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

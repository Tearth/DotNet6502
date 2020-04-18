using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Monitor.Instructions;
using Monitor.ViewModels;

namespace Monitor.Converters
{
    public class MemoryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = (byte[])values[0];
            var viewModel = (MainWindowViewModel)values[1];

            if (bytes == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi\deff0");
            builder.Append(@"{\fonttbl {\f0 Consolas;}}");
            builder.Append(@"{\colortbl;\red255\green255\blue255;\red150\green150\blue150;}");
            builder.Append(@"\fs18");

            var line = 0;
            var bytesPerLine = 32;
            for (var i = 0; i < bytes.Length; i += bytesPerLine, line++)
            {
                builder.Append(@"\cf2 0x");
                builder.Append((viewModel.MemoryAddress + line * bytesPerLine).ToString("X4"));
                builder.Append(@": \cf1 ");

                var memoryPart = bytes.Skip(line * bytesPerLine).Take(bytesPerLine);
                var bytesString = string.Join(" ", memoryPart.Select(p => $"{p:X2}"));
                var charsString = string.Join("", memoryPart.Select(p => char.IsControl((char)p) ? '.' : (char)p));

                builder.Append(bytesString);
                builder.Append(@" \cf2; ");
                builder.Append(charsString);
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
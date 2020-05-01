using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Monitor.Converters
{
    public class MemoryConverter : IMultiValueConverter
    {
        private const int BytesPerLine = 32;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = (byte[])values[0];
            var memoryAddress = (ushort)values[1];

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
            for (var i = 0; i < bytes.Length; i += BytesPerLine, line++)
            {
                builder.Append(@"\cf2 0x");
                builder.Append((memoryAddress + line * BytesPerLine).ToString("X4"));
                builder.Append(@": \cf1 ");

                var memoryPart = bytes.Skip(line * BytesPerLine).Take(BytesPerLine).ToList();
                var formattedBytes = memoryPart.Select(p => $"{p:X2}");
                var formattedChars = memoryPart.Select(p => char.IsControl((char)p) ? '.' : (char)p);

                var bytesString = string.Join(" ", formattedBytes);
                var charsString = string.Join("", formattedChars);

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
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Monitor.Instructions;
using Monitor.ViewModels;

namespace Monitor.Converters
{
    public class CodeConverter : IMultiValueConverter
    {
        private readonly InstructionsContainer _instructions;

        public CodeConverter()
        {
            _instructions = new InstructionsContainer("Instructions.json");
        }

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

            var index = 0;
            var first = true;
            while (index < bytes.Length)
            {
                (InstructionData instruction, byte[] data) = GetNextInstruction(bytes, index);

                if (first) builder.Append(@"\b");
                builder.Append(@"\cf2 0x");
                builder.Append(((ushort)(viewModel.Registers.Pc + index)).ToString("X4"));
                builder.Append(@": 0x");
                builder.Append(data[0].ToString("X2"));
                builder.Append(@" \cf1 ");
                builder.Append(instruction?.Name ?? "???");
                builder.Append(" ");

                var argumentsString = string.Join(" ", data.Skip(1).Select(p => $"0x{p:X2}"));
                var paddedArgumentsString = argumentsString.PadRight(15);

                builder.Append(paddedArgumentsString);
                builder.Append(@"\cf2; ");
                builder.Append(instruction?.Description ?? "???");
                if (first) builder.Append(@"\b0");
                builder.Append(@"\line");

                index += data.Length;
                first = false;
            }

            builder.Append(@"}");
            return builder.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private (InstructionData instruction, byte[] data) GetNextInstruction(byte[] bytes, int index)
        {
            if (index > bytes.Length - 1)
            {
                return (null, null);
            }

            var instruction = _instructions.Get(bytes[index]);
            if (instruction == null)
            {
                return (null, new [] { bytes[index] });
            }

            if (index + instruction.Bytes - 1 > bytes.Length - 1)
            {
                return (null, new[] { bytes[index] });
            }

            return (instruction, bytes.Skip(index).Take(instruction.Bytes).ToArray());
        }
    }
}
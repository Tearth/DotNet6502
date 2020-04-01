using System.Text.RegularExpressions;

namespace CPU.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string Smash(this string str)
        {
            return Regex.Replace(str, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
        }
    }
}

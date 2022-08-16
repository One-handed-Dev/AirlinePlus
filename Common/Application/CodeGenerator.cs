using System;
using System.Linq;
using System.Text;

namespace Common.Application
{
    public sealed class CodeGenerator
    {
        private static string RandomString()
        {
            const int length = 2;
            var stringBuild = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                var Float = random.NextDouble();
                var shift = Convert.ToInt32(Math.Floor(25 * Float));
                var letter = Convert.ToChar(shift + 65);
                stringBuild.Append(letter);
            }

            return stringBuild.ToString();
        }

        public static string GenerateRandom(int length)
        {
            var rnd = new Random();
            var randomNumbers = Enumerable.Range(0, 10).OrderBy(x => rnd.Next()).Take(length).ToList();
            return string.Join("", randomNumbers);
        }

        public static string GenerateRandomString(int length)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateRandom(string symbol) => $"{symbol}{RandomString()}{RandomNumberWithLength(5)}";

        private static string RandomNumberWithLength(int powerOfTen) => new Random().Next((int)Math.Pow(10, powerOfTen), (int)Math.Pow(10, powerOfTen + 1) - 1).ToString();
    }
}

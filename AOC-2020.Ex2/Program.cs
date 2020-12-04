using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020.Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt").Split('\n');

            Console.WriteLine(input.Where(s => ValidateOld(s)).Count());
            Console.WriteLine(input.Where(s => ValidateNew(s)).Count());
        }

        private static bool ValidateOld(string s)
        {
            var match = Regex.Match(s, @"(\d+)-(\d+) (\w): (\w+)");
            
            if (match.Success)
            {
                var groups = match.Groups;

                try
                {
                    var min = int.Parse(groups[1].Value);
                    var max = int.Parse(groups[2].Value);
                    var digit = char.Parse(groups[3].Value);
                    var password = groups[4].Value;

                    var count = password.Count(x => x == digit);

                    if (count >= min && count <= max)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        private static bool ValidateNew(string s)
        {
            var match = Regex.Match(s, @"(\d+)-(\d+) (\w): (\w+)");

            if (match.Success)
            {
                var groups = match.Groups;

                try
                {
                    var posA = int.Parse(groups[1].Value) - 1;
                    var posB = int.Parse(groups[2].Value) - 1;
                    var digit = char.Parse(groups[3].Value);
                    var password = groups[4].Value;

                    if (password[posA] == digit ^ password[posB] == digit)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    }
}

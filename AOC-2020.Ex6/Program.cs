using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020.Ex6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt").Split("\r\n\r\n");

            Console.WriteLine(input.Sum(s => Regex.Matches(s, @"\w").Select(m => m.Value).Distinct().Count()));

            Console.WriteLine(input.Sum(t => GetConsensusCount(t.Split("\r\n"))));
        }

        private static int GetConsensusCount(IEnumerable<string> input)
        {
            IEnumerable<char> result = null;

            foreach (var item in input)
            {
                result = result == null ? item : result.Intersect(item);
            }

            return result.Count();
        }
    }
}

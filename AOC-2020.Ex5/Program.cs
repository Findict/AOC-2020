using System;
using System.Linq;

namespace AOC_2020.Ex5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt").Split("\r\n");

            var numbers = input.Select(s => LocationToNumber(s)).ToList();

            Console.WriteLine(numbers.Max());

            var result = numbers.Select(n => n + 1).Except(numbers).Min();

            Console.WriteLine(result);
        }

        private static int LocationToNumber(string input)
        {
            var binaryString = input.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');

            return Convert.ToInt32(binaryString, 2);
        }
    }
}

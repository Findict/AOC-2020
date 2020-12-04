using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020.Ex3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt").Split('\n').Select(s => s.Trim());

            var slopeDownA = 1;
            var slopeRightA = 3;
            var j = -slopeRightA;

            var result = input.Select((s, index) =>
            {
                if (index % slopeDownA == 0)
                {
                    j = (j + slopeRightA) % s.Length;
                    return s[j];
                }

                return '-';
            }).Count(d => d == '#');

            Console.WriteLine(result);

            var inputs = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 3),
                new Tuple<int, int>(1, 5),
                new Tuple<int, int>(1, 7),
                new Tuple<int, int>(2, 1),
            };

            var finalResult = 1l;

            foreach (var item in inputs)
            {
                var slopeDown = item.Item1;
                var slopeRight = item.Item2;
                var i = -slopeRight;

                finalResult *= input.Select((s, index) =>
                {
                    if (index % slopeDown == 0)
                    {
                        i = (i + slopeRight) % s.Length;
                        return s[i];
                    }

                    return '-';
                }).Count(d => d == '#');
            }

            Console.WriteLine(finalResult);
        }
    }
}

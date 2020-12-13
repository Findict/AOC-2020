using System;
using System.Linq;

namespace AOC_2020.Ex13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt").Split("\r\n");

            var num = int.Parse(input.First());
            var busses = input[1].Split(',').Where(s => s != "x").Select(s => int.Parse(s));

            var firstBus = busses.Select(bus => (time: bus - (num % bus), bus)).OrderBy(x => x.Item1).First();

            var result = firstBus.bus * firstBus.time;

            Console.WriteLine(firstBus.bus * firstBus.time);

            var primes = input[1].Split(',').Select((s, i) =>
            {
                if (int.TryParse(s, out int n))
                {
                    return new Tuple<int, int>(n, i == 0 ? 0 : n - (i % n));
                }

                return null;
            }).Where(t => t != null);

            var val = 0l;
            var previous = 1l;

            foreach (var item in primes)
            {
                if (val == 0)
                {
                    val = item.Item1 - item.Item2;
                }
                else
                {
                    while (val % item.Item1 != item.Item2)
                    {
                        val += previous;
                    }
                }

                previous *= item.Item1;
            }

            Console.WriteLine(val);

            // Exercise for the reader: Why does this work?
        }
    }
}

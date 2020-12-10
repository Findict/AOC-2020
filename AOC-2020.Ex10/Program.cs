using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020.Ex10
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = System.IO.File.ReadAllText("input.txt").Split("\r\n").Select(i => int.Parse(i)).ToList();

            numbers.AddRange(new List<int> { 0, numbers.Max() + 3 });

            numbers = numbers.OrderBy(i => i).ToList();

            var differences = numbers.Skip(1).Select((x, i) => x - numbers[i]).ToList();

            var dif1 = differences.Count(i => i == 1);
            var dif3 = differences.Count(i => i == 3) + 1;

            Console.WriteLine(dif1 * dif3);

            var numChains = new Dictionary<int, long>();

            foreach (var item in numbers)
            {
                if (item == 0)
                {
                    numChains.Add(item, 1);
                }
                else
                {
                    var sumNums = numChains.Where(i => i.Key < item && i.Key >= item - 3);

                    numChains.Add(item, sumNums.Sum(i => i.Value));
                }
            }

            Console.WriteLine(numChains[numbers.Max()]);
        }
    }
}

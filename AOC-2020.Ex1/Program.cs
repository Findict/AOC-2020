using System;
using System.Linq;

namespace AOC_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = System.IO.File.ReadAllText("input.txt").Split('\n').Select(i => int.Parse(i)).ToList();

            var altList = list.Select(i => 2020 - i);

            var resultsOne = altList.Intersect(list).ToList();

            Console.WriteLine(resultsOne[0]* resultsOne[1]);

            foreach (var value in list)
            {
                var antiVal = 2020 - value;

                var test = list.Where(i => i < antiVal).Select(i => antiVal - i).Intersect(list).ToList();

                if (test.Count() == 2)
                {
                    Console.WriteLine(test[0] * test[1] * value);
                    break;
                }
            }
        }
    }
}

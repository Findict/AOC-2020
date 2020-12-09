using System;
using System.Linq;

namespace AOC_2020.Ex9
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = System.IO.File.ReadAllText("input.txt").Split("\r\n").Select(i => long.Parse(i)).ToList();

            var lookback = 25;

            var firstResult = 0l;

            for (int i = lookback; i < numbers.Count(); i++)
            {
                var selectedNumber = numbers[i];

                var firstPart = numbers.Skip(i - lookback).Take(lookback);

                var sumNumbers = firstPart.Select(j => selectedNumber - j).Intersect(firstPart);

                if (sumNumbers.Count() == 0)
                {
                    firstResult = selectedNumber;

                    break;
                }
            }

            Console.WriteLine(firstResult);

            var firstIndex = 0;
            var lastIndex = 0;

            var sum = numbers.First();

            while (true)
            {
                if (sum < firstResult)
                {
                    lastIndex++;
                    sum += numbers[lastIndex];
                }

                else if (sum > firstResult)
                {
                    sum -= numbers[firstIndex];
                    firstIndex++;
                }

                else
                {
                    break;
                }
            }

            var sequence = numbers.Skip(firstIndex).Take(lastIndex - firstIndex + 1);

            Console.WriteLine(sequence.Max() + sequence.Min());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020.Ex8
{
    class Program
    {
        private static readonly string instructionPattern = @"(\w+) ([+-]\d+)";

        private static List<Tuple<string, int>> instructions;

        static void Main(string[] args)
        {
            instructions = System.IO.File.ReadAllText("input.txt").Split("\r\n").Select(i => GetInstruction(i)).ToList();

            var steps = new List<int>();

            var index = 0;
            var acc = 0;

            while (true)
            {
                if (steps.Contains(index))
                {
                    break;
                }

                steps.Add(index);

                (index, acc) = GetNext(index, acc);
            }
            
            Console.WriteLine(acc);

            IDictionary<int, int> stepsPlusAcc = new Dictionary<int, int>();
            var switched = new List<int>();

            index = 0;
            acc = 0;
            var flipEnumerator = steps.GetEnumerator();
            var indexToFlip = flipEnumerator.Current;

            while (true)
            {
                if (index == instructions.Count() - 1)
                {
                    break;
                }

                if (stepsPlusAcc.Keys.Contains(index))
                {
                    index = indexToFlip;
                    acc = stepsPlusAcc[index];
                    stepsPlusAcc = stepsPlusAcc.TakeWhile(i => i.Key != index).ToDictionary(k => k.Key, k => k.Value);
                    flipEnumerator.MoveNext();
                    indexToFlip = flipEnumerator.Current;
                }

                stepsPlusAcc.Add(index, acc);

                (index, acc) = GetNext(index, acc, indexToFlip == index);
            }

            Console.WriteLine(acc);
        }

        private static (int index, int acc) GetNext(int index, int acc, bool flip = false)
        {
            var instruction = instructions[index];

            switch (instruction.Item1)
            {
                case "jmp":
                    index += flip ? 1 : instruction.Item2;
                    break;
                case "acc":
                    index++;
                    acc += instruction.Item2;
                    break;
                case "nop":
                    index += flip ? instruction.Item2 : 1;
                    break;
                default:
                    break;
            }

            return (index, acc);
        }

        private static Tuple<string, int> GetInstruction(string i)
        {
            var Match = Regex.Match(i, instructionPattern);

            if (Match.Success)
            {
                return new Tuple<string, int>(Match.Groups[1].Value, int.Parse(Match.Groups[2].Value));
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020.Ex7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt").Split("\r\n");

            var bags = input.Select(i => new Bag(i)).ToList();

            var sgBag = bags.FirstOrDefault(b => b.Name == "shiny gold");

            var result = GetOuterBags(bags.FirstOrDefault(b => b.Name == "shiny gold"), bags);

            Console.WriteLine(result.Count());

            Console.WriteLine(GetTotalInnerBags(sgBag, bags) - 1);
        }

        static List<Bag> GetOuterBags(Bag bag, List<Bag> potentialOuter)
        {
            var thisBagOuter = potentialOuter.Where(b => b.LinkedBags.Keys.Contains(bag.Name)).ToList();
            potentialOuter.Remove(bag);

            var outerBags = new List<Bag>();

            outerBags.AddRange(thisBagOuter);

            foreach (var item in thisBagOuter)
            {
                outerBags.AddRange(GetOuterBags(item, potentialOuter));
            }

            return outerBags.Distinct().ToList();
        }

        static int GetTotalInnerBags(Bag bag, List<Bag> potentialInner)
        {
            // We assume there is no infinite loop
            var result = 1;

            foreach (var item in bag.LinkedBags)
            {
                result += item.Value * GetTotalInnerBags(potentialInner.FirstOrDefault(b => b.Name == item.Key), potentialInner);
            }

            return result;
        }
    }

    class Bag
    {
        private static readonly string bagPattern = @"^([\w ]+) bags? contain";
        private static readonly string linkedBagsPattern = @"(\d+) ([\w ]+) bags?";

        public string Name { get; set; }

        public Dictionary<string, int> LinkedBags { get; set; } = new Dictionary<string, int>();

        public Bag(string s)
        {
            var bagMatch = Regex.Match(s, bagPattern);

            if (bagMatch.Success)
            {
                this.Name = bagMatch.Groups[1].Value;
            }

            var linkedMatches = Regex.Matches(s, linkedBagsPattern);

            foreach (Match item in linkedMatches)
            {
                this.LinkedBags.Add(item.Groups[2].Value, int.Parse(item.Groups[1].Value));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020.Ex4
{
    class Program
    {
        private static readonly List<string> mandatory = new List<string>()
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
        };

        private static readonly List<string> eyeColors = new List<string>()
        {
            "amb",
            "blu",
            "brn",
            "gry",
            "grn",
            "hzl",
            "oth"
        };

        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt").Split("\r\n\r\n");

            var dict = input.Select(l => MakeDict(l)).ToList();

            var filtered = dict.Where(i => mandatory.All(m => i.Keys.Contains(m)));

            Console.WriteLine(filtered.Count());

            Console.WriteLine(filtered.Count(i => Validate(i)));
        }

        private static bool Validate(Dictionary<string, string> i)
        {
            var result = true;

            foreach (var item in i)
            {
                var inputText = item.Value.Trim();

                switch (item.Key)
                {
                    case "byr":
                        result &= ValidateNumber(inputText, 1920, 2002);
                        break;
                    case "iyr":
                        result &= ValidateNumber(inputText, 2010, 2020);
                        break;
                    case "eyr":
                        result &= ValidateNumber(inputText, 2020, 2030);
                        break;
                    case "hgt":
                        result &= ValidateHeight(inputText);
                        break;
                    case "hcl":
                        result &= ValidateHex(inputText);
                        break;
                    case "ecl":
                        result &= ValidateEye(inputText);
                        break;
                    case "pid":
                        result &= ValidatePassId(inputText);
                        break;
                    default:
                        break;
                }

                if (!result)
                {
                    break;
                }
            }

            return result;
        }

        private static bool ValidatePassId(string value)
            => Regex.IsMatch(value, @"^[0-9]{9}$");

        private static bool ValidateEye(string value)
            => eyeColors.Contains(value);

        private static bool ValidateHex(string value)
            => Regex.IsMatch(value, @"^#[0-9a-f]{6}$");

        private static bool ValidateHeight(string value)
        {
            var match = Regex.Match(value, @"^(\d+)(cm|in)$");

            if (match.Success)
            {
                switch (match.Groups[2].Value)
                {
                    case "cm":
                        return ValidateNumber(match.Groups[1].Value, 150, 193);
                    case "in":
                        return ValidateNumber(match.Groups[1].Value, 59, 76);
                    default:
                        break;
                }
            }

            return false;
        }

        private static Dictionary<string, string> MakeDict(string l)
        {
            var matches = Regex.Matches(l, @"(\w{3}):([^ \r\n]+)");

            var result = new Dictionary<string, string>();

            foreach (Match item in matches)
            {
                result.Add(item.Groups[1].Value, item.Groups[2].Value);
            }

            return result;
        }

        private static bool ValidateNumber(string num, int min, int max)
        {
            if (int.TryParse(num, out int number))
            {
                return number >= min && number <= max;
            }

            return false;
        }
    }
}

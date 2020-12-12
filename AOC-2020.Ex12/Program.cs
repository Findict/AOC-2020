using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020.Ex12
{
    class Program
    {
        static readonly string directionPattern = @"(\w)(\d+)";

        static void Main(string[] args)
        {
            var directions = System.IO.File.ReadAllText("input.txt").Split("\r\n").Select(s =>
            {
                var groups = Regex.Match(s, directionPattern).Groups;
                return (move: groups[1].Value, value: int.Parse(groups[2].Value));
            }).ToList();

            var x = directions.Where(d => d.move == "E" || d.move == "W").Sum(d => (d.move == "E" ? 1 : -1) * d.value);
            var y = directions.Where(d => d.move == "N" || d.move == "S").Sum(d => (d.move == "N" ? 1 : -1) * d.value);

            var instructions = directions.Where(d => d.move == "F" || d.move == "L" || d.move == "R").ToList();

            var ship = new Ship(x, y, Dir.E);
            ship.ProcessInstructions(instructions);

            Console.WriteLine(ship.ManhattanDist());

            var waypoint = new Waypoint(10, 1);

            waypoint.ProcessInstructions(directions);

            Console.WriteLine(waypoint.ManhattanDist());
        }
    }
}

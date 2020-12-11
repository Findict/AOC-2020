using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020.Ex11
{
    class Program
    {
        static void Main(string[] args)
        {
            var seats = System.IO.File.ReadAllText("input.txt").Split("\r\n").ToList()
                .SelectMany((s, i) =>
                    s.Select((c, j) =>
                    {
                        if (c == 'L')
                        {
                            return new Seat(i, j);
                        }

                        return null;
                    }).Where(s => s != null)).ToList();

            var neighbourDict = new Dictionary<Seat, List<Seat>>();

            foreach (var seat in seats)
            {
                var neigh = seats.Where(s => s != seat && s.Row <= seat.Row + 1 && s.Row >= seat.Row - 1 && s.Column <= seat.Column + 1 && s.Column >= seat.Column - 1)
                    .ToList();

                neighbourDict.Add(seat, neigh);
            }

            var result = 0;

            do
            {
                result = IterateSeats(neighbourDict, 4);
            } while (result != 0);

            Console.WriteLine(seats.Count(s => s.Occupied));

            seats.ForEach(s => s.Occupied = false);

            var directions = new List<(int, int)>
            {
                (1, 0),
                (0, 1),
                (-1, 0),
                (0, -1),
                (1, 1),
                (-1, 1),
                (1, -1),
                (-1, -1),
            };

            var width = seats.Max(s => s.Column);
            var height = seats.Max(s => s.Row);

            neighbourDict = new Dictionary<Seat, List<Seat>>();

            foreach (var seat in seats)
            {
                var neighbours = new List<Seat>();

                foreach (var item in directions)
                {
                    var location = (seat.Row, seat.Column);

                    while (true)
                    {
                        location.Row += item.Item1;
                        location.Column += item.Item2;

                        if (location.Row < 0 || location.Column < 0 || location.Row > height || location.Column > width)
                        {
                            break;
                        }

                        var neighSeat = seats.FirstOrDefault(s => s.Row == location.Row && s.Column == location.Column);

                        if (neighSeat != null)
                        {
                            neighbours.Add(neighSeat);
                            break;
                        }
                    }
                }

                neighbourDict.Add(seat, neighbours);
            }

            do
            {
                result = IterateSeats(neighbourDict, 5);
            } while (result != 0);

            Console.WriteLine(seats.Count(s => s.Occupied));
        }

        static int IterateSeats(Dictionary<Seat, List<Seat>> neighbourDict, int high)
        {
            var result = 0;

            var openSeats = neighbourDict.Where(s => !s.Key.Occupied && s.Value.Count(s => s.Occupied) == 0).Select(s => s.Key).ToList();
            var closedSeats = neighbourDict.Where(s => s.Key.Occupied && s.Value.Count(s => s.Occupied) >= high).Select(s => s.Key).ToList();

            openSeats.ForEach(s => { result++; s.Occupied = true; });
            closedSeats.ForEach(s => { result++; s.Occupied = false; });

            return result;
        }
    }
}

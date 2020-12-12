using System;
using System.Collections.Generic;

namespace AOC_2020.Ex12
{
    public class Waypoint
    {
        private int x;
        private int y;
        private Ship ship;

        public Waypoint(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.ship = new Ship(0, 0, Dir.E);
        }

        public void ProcessInstructions(List<(string move, int value)> instructions)
        {
            instructions.ForEach(i => this.ProcessInstruction(i));
        }

        public void ProcessInstruction((string move, int value) instruction)
        {
            switch (instruction.move)
            {
                case "N":
                    this.Move(0, instruction.value);
                    break;
                case "E":
                    this.Move(instruction.value, 0);
                    break;
                case "S":
                    this.Move(0, -instruction.value);
                    break;
                case "W":
                    this.Move(-instruction.value, 0);
                    break;
                case "L":
                    this.RotateClockwise(4 - (instruction.value / 90));
                    break;
                case "R":
                    this.RotateClockwise(instruction.value / 90);
                    break;
                case "F":
                    this.MoveForward(instruction.value);
                    break;
            }
        }
        public void Move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }


        private void MoveForward(int value)
        {
            var relx = this.x - ship.x;
            var rely = this.y - ship.y;

            ship.Move(relx * value, rely * value);
            this.Move(relx * value, rely * value);
        }

        private void RotateClockwise(int times)
        {
            for (int i = 0; i < times; i++)
            {
                (this.x, this.y) = (ship.x + (this.y - ship.y), ship.y - (this.x - ship.x));
            }
        }

        public int ManhattanDist()
            => ship.ManhattanDist();
    }
}
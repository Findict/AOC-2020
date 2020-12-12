using System;
using System.Collections.Generic;

namespace AOC_2020.Ex12
{
    public class Ship
    {
        public int x;
        public int y;
        private Dir direction;

        public Ship(int x, int y, Dir direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }

        public void ProcessInstructions(List<(string move, int value)> instructions)
        {
            instructions.ForEach(i => this.ProcessInstruction(i));
        }

        public void ProcessInstruction((string move, int value) instruction)
        {
            switch (instruction.move)
            {
                case "L":
                    direction = (Dir)(((int)direction + (360 - instruction.value) / 90) % 4);
                    break;
                case "R":
                    direction = (Dir)(((int)direction + instruction.value / 90) % 4);
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
            switch (this.direction)
            {
                case Dir.N:
                    this.Move(0, value);
                    break;
                case Dir.E:
                    this.Move(value, 0);
                    break;
                case Dir.S:
                    this.Move(0, -value);
                    break;
                case Dir.W:
                    this.Move(-value, 0);
                    break;
            };
        }

        public int ManhattanDist()
            => Math.Abs(this.x) + Math.Abs(this.y);
    }
}
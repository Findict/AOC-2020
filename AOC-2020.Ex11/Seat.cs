namespace AOC_2020.Ex11
{
    internal class Seat
    {
        public int Row;
        public int Column;
        public bool Occupied;

        public Seat(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
}
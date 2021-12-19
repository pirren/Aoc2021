namespace Aoc2021.Library
{
    public class AocPoint : IEquatable<AocPoint>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(AocPoint? other)
        {
            return X == other?.X && Y == other?.Y;
        }
    }
}

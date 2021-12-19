using Aoc2021.Library;
using System.Drawing;

namespace Aoc2021.Solutions
{
    public class Day9 : DayBase
    {
        public override string Name => "Smoke Basin";
        public override int Day => 9;

        List<int[]> Heightmap = new();

        public override object PartOne(string indata)
        {
            Heightmap = indata.Trim().Split("\r\n").Select(s => s.Select(ch => ch.ToInt()).ToArray()).ToList();
            return GetLowPoints().Select(point => Heightmap[point.Y][point.X] + 1).Sum();
        }

        public override object PartTwo(string indata)
        {
            Heightmap = indata.Trim().Split("\r\n").Select(s => s.Select(ch => ch.ToInt()).ToArray()).ToList();

            return GetLowPoints().Select(lowpoint => CalculateBasin(lowpoint)).OrderByDescending(s => s).Take(3).Aggregate((a, b) => a * b);
        }

        private long CalculateBasin(AocPoint point)
            => CalculateBasin(point, 0, new());

        IEnumerable<AocPoint> Neighbors(AocPoint point)
            => new List<AocPoint> {
                    new AocPoint { Y = point.Y - 1, X = point.X },
                    new AocPoint { Y = point.Y + 1, X = point.X },
                    new AocPoint { Y = point.Y, X = point.X - 1 },
                    new AocPoint { Y = point.Y, X = point.X + 1 },
            }.Where(InBounds);

        bool InBounds(AocPoint p) => p.X >= 0 && p.X < Heightmap.First().Length && p.Y >= 0 && p.Y < Heightmap.Count;

        private long CalculateBasin(AocPoint point, int previous, List<AocPoint> checkedPoints)
        {
            var currentvalue = Heightmap[point.Y][point.X];
            if (currentvalue == 9 || checkedPoints.Any(p => p.Equals(point)) || currentvalue < previous) return 0;

            checkedPoints.Add(point);

            var debug = Neighbors(point).ToList();

            foreach (var neighbor in Neighbors(point))
                CalculateBasin(neighbor, currentvalue, checkedPoints);

            return previous == 0 ? checkedPoints.Count : 0;
        }

        private List<AocPoint> GetLowPoints()
        {
            List<AocPoint> lowpoints = new();

            for (int row = 0; row < Heightmap.Count; row++)
            {
                for (int col = 0; col < Heightmap.First().Length; col++)
                {
                    var debug = Neighbors(new AocPoint { X = col, Y = row }).ToList();

                    List<int> controlvalues = new();
                    controlvalues.AddRange(Neighbors(new AocPoint { X = col, Y = row }).Select(s => Heightmap[s.Y][s.X]));

                    if (controlvalues.All(val => val > Heightmap[row][col]))
                        lowpoints.Add(new AocPoint { X = col, Y = row });
                }
            }

            return lowpoints;
        }
    }
}

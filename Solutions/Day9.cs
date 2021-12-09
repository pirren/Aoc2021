using Aoc2021.Library;
using System.Drawing;

namespace Aoc2021.Solutions
{
    public class Day9 : DayBase
    {
        public override string Name => "Smoke Basin";
        public override int Day => 9;

        public override object PartOne(string indata)
        {
            var heightmap = indata.Trim().Split("\r\n").Select(s => s.Select(ch => ch.ToInt()).ToArray()).ToList();
            var xmax = heightmap.First().Length;

            return GetLowPoints(heightmap, xmax).Select(point => heightmap[point.Y][point.X] + 1).Sum();
        }

        public override object PartTwo(string indata)
        {
            var heightmap = indata.Trim().Split("\r\n").Select(s => s.Select(ch => ch.ToInt()).ToArray()).ToList();
            var xmax = heightmap.First().Length;

            return GetLowPoints(heightmap, xmax).Select(lowpoint => CalculateBasin(lowpoint, heightmap)).OrderByDescending(s => s).Take(3).Aggregate((a, b) => a * b);
        }

        private long CalculateBasin(Point point, List<int[]> heightmap)
            => CalculateBasin(point, heightmap, 0, new());

        private long CalculateBasin(Point point, List<int[]> heightmap, int previous, List<Point> checkedPoints)
        {
            var currentvalue = heightmap[point.Y][point.X];
            if (currentvalue == 9 || checkedPoints.Contains(point) || currentvalue < previous) return 0;

            checkedPoints.Add(point);

            if (point.Y > 0) // check neighbor to north
                CalculateBasin(new Point { Y = point.Y - 1, X = point.X }, heightmap, currentvalue, checkedPoints);

            if (point.X < heightmap[0].Length - 1) // check neighbor to east
                CalculateBasin(new Point { Y = point.Y, X = point.X + 1 }, heightmap, currentvalue, checkedPoints);

            if (point.X > 0) // check neighbor to west
                CalculateBasin(new Point { Y = point.Y, X = point.X - 1 }, heightmap, currentvalue, checkedPoints);

            if (point.Y < heightmap.Count - 1) // check neighbor to south
                CalculateBasin(new Point { Y = point.Y + 1, X = point.X }, heightmap, currentvalue, checkedPoints);

            return previous == 0 ? checkedPoints.Count : 0;
        }

        private List<Point> GetLowPoints(List<int[]> heightmap, int xmax)
        {
            List<Point> lowpoints = new();

            for (int row = 0; row < heightmap.Count; row++)
            {
                for (int col = 0; col < xmax; col++)
                {
                    List<int> controlvalues = new();
                    if (row > 0) // check neighbor to north
                        controlvalues.Add(heightmap[row - 1][col]);

                    if (col < xmax - 1) // check neighbor to east
                        controlvalues.Add(heightmap[row][col + 1]);

                    if (col > 0) // check neighbor to west
                        controlvalues.Add(heightmap[row][col - 1]);

                    if (row < heightmap.Count - 1) // check neighbor to south
                        controlvalues.Add(heightmap[row + 1][col]);

                    if (controlvalues.All(val => val > heightmap[row][col]))
                        lowpoints.Add(new Point { X = col, Y = row });
                }
            }

            return lowpoints;
        }
    }
}

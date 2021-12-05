using Aoc2021.Library;
using System.Drawing;

namespace Aoc2021.Solutions
{
    public class Day5 : DayBase
    {
        public override string ProblemName => "Hydrothermal Venture";

        public override int Day => 5;

        public override object PartOne(string indata)
            => VentMap(indata).Flatten().Where(val => val != ".").Count(val => val.ToInt() > 1);

        public override object PartTwo(string indata)
            => VentMap(indata, horizontal: true).Flatten().Where(val => val != ".").Count(val => val.ToInt() > 1);

        private void Mark(int x, int y, ref string[,] map)
        {
            var val = map[x, y];
            if (int.TryParse(val, out var v))
                map[x, y] = (v + 1).ToString();
            else map[x, y] = "1";
        }

        private string[,] VentMap(string indata, bool horizontal = false)
        {
            var vents = indata.Split("\r\n")
                .Select(s => s.Split(" -> ").Select(s => s.Split(',')).Select(s => new Point
                {
                    X = int.Parse(s[0].ToString()),
                    Y = int.Parse(s[1].ToString()),
                }).ToArray());

            var map = new string[vents.Select(s => s.Max(s => s.X)).Max() + 1, vents.Select(s => s.Max(s => s.Y)).Max() + 1].Populate(".");

            foreach (var points in vents)
            {
                if (points[0].X == points[1].X)
                {
                    var yvals = points.Select(s => s.Y);
                    for (int y = yvals.Min(); y <= yvals.Max(); y++) Mark(points[0].X, y, ref map);
                }
                if (points[0].Y == points[1].Y)
                {
                    var xvals = points.Select(s => s.X);
                    for (int x = xvals.Min(); x <= xvals.Max(); x++) Mark(x, points[0].Y, ref map);
                }
                if (horizontal && (points[0], points[1]).IsAngle45())
                    InHorizontalLine(points[0], points[1]).ForEach(s => Mark(s.X, s.Y, ref map));
            }
            return map;
        }

        private List<Point> InHorizontalLine(Point p1, Point p2)
        {
            List<Point> points = new();
            var xvals = new[] { p1.X, p2.X };
            var yvals = new[] { p1.Y, p2.Y };
            var xrange = Enumerable.Range(xvals.Min(), xvals.Max() - xvals.Min() + 1).ToList();
            var yrange = Enumerable.Range(yvals.Min(), yvals.Max() - yvals.Min() + 1).ToList();
            if (p1.X > p2.X) xrange.Reverse();
            if (p1.Y > p2.Y) yrange.Reverse();

            while (xrange.Any() && yrange.Any())
                points.Add(new Point { X = xrange.PopAt(0), Y = yrange.PopAt(0) });

            return points;
        }
    }
}

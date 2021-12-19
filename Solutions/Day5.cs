using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day5 : DayBase
    {
        public override string Name => "Hydrothermal Venture";
        public override int Day => 5;

        private class VentLine
        {
            public AocPoint PointA = new();
            public AocPoint PointB = new();

            public bool IsLateral => PointA.X == PointB.X || PointA.Y == PointB.Y;
            public bool IsVertical => PointA.X == PointB.X && PointA.Y != PointB.Y;
            public bool IsHorizontal => PointA.Y == PointB.Y && PointA.X != PointB.X;
            public bool IsDiagonal => PointA.X != PointB.X && PointA.Y != PointB.Y;

            public int HighX => PointA.X > PointB.X ? PointA.X : PointB.X; 
            public int LowX => PointA.X < PointB.X ? PointA.X : PointB.X;

            public int HighY => PointA.Y > PointB.Y ? PointA.Y : PointB.Y;
            public int LowY => PointA.Y < PointB.Y ? PointA.Y : PointB.Y;
        }

        public override object PartOne(string indata)
        {
            var map = GetMap(indata, SolutionPart.PartA);
            return map.Count(s => s.Value > 1);
        }

        public override object PartTwo(string indata)
        {
            var map = GetMap(indata, SolutionPart.PartB);
            return map.Count(s => s.Value > 1);
        }

        public Dictionary<(int, int), int> GetMap(string indata, SolutionPart part)
        {
            var map = new Dictionary<(int, int), int>();

            HashSet<VentLine> ventLines = new();
            indata.Trim().Split("\r\n").Select(row => row.Split(" -> ")).Select(s => (s[0].Split(','), s[1].Split(',')))
            .ForEach(s => ventLines.Add(
                new VentLine
                {
                    PointA = new AocPoint
                    {
                        X = s.Item1.First().ToInt(),
                        Y = s.Item1.Last().ToInt()
                    }, 
                    PointB = new AocPoint
                    { 
                        X = s.Item2.First().ToInt(), 
                        Y = s.Item2.Last().ToInt()
                    }
                }));

            var linesA = ventLines.Where(x => x.IsLateral);

            foreach (var line in linesA.Where(x => x.IsVertical))
            {
                var x = line.HighX;
                Enumerable.Range(line.LowY, line.HighY - line.LowY + 1).ForEach(y => {
                    if (map.ContainsKey((x, y))) map[(x, y)]++;
                    else map.Add((x, y), 1);
                });
            }
            foreach (var line in linesA.Where(x => x.IsHorizontal))
            {
                var y = line.HighY;
                Enumerable.Range(line.LowX, line.HighX - line.LowX + 1).ForEach(x => {
                    if (map.ContainsKey((x, y))) map[(x, y)]++;
                    else map.Add((x, y), 1);
                });
            }

            if (part == SolutionPart.PartB)
            {
                ventLines.Where(x => x.IsDiagonal).ForEach(line =>
                {
                    GetHorizontalVents(line).ForEach(p =>
                    {
                        if (map.ContainsKey((p.X, p.Y))) map[(p.X, p.Y)]++;
                        else map.Add((p.X, p.Y), 1);
                    });
                });
            }

            return map;
        }

        private List<AocPoint> GetHorizontalVents(VentLine line)
        {
            List<AocPoint> vents = new();

            var xrange = Enumerable.Range(line.LowX, line.HighX - line.LowX + 1).ToList();
            var yrange = Enumerable.Range(line.LowY, line.HighY - line.LowY + 1).ToList();

            if (line.PointA.X > line.PointB.X) xrange.Reverse();
            if (line.PointA.Y > line.PointB.Y) yrange.Reverse();

            while (xrange.Any() && yrange.Any()) vents.Add(new AocPoint { X = xrange.PopAt(0), Y = yrange.PopAt(0) });

            return vents;
        }
    }
}

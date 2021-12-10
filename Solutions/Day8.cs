using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day8 : DayBase
    {
        public override string Name => "Seven Segment Search";
        public override int Day => 8;

        public override object PartOne(string indata)
            => indata
                .Trim()
                .Split('\n')
                .Select(s => s.Split(" | ")[1])
                .SelectMany(s => s.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Count(digit => new[] { 2, 4, 3, 7 }.Contains(digit.Length));

        public override object PartTwo(string indata)
        {
            var lines = indata.Trim().Split("\r\n");

            List<int> displayvalues = new();
            foreach (var line in lines)
            {
                var split = line.Split(" | ");
                var numbers = split[1].Split(' ').ToArray();
                var data = split[0].Split(' ').Distinct().ToList();
                Dictionary<int, string> displaymap = new();

                displaymap[1] = Segment(2, ref data);
                displaymap[4] = Segment(4, ref data);
                displaymap[7] = Segment(3, ref data);
                displaymap[8] = Segment(7, ref data);
                displaymap[9] = Segment(6, s => s.Trim(displaymap[1].Concat(displaymap[4]).Concat(displaymap[7]).Distinct().ToArray()).Length == 1, ref data);
                displaymap[2] = Segment(5, s => s.Trim(displaymap[9].ToArray()).Length == 1, ref data);
                displaymap[3] = Segment(5, s => s.Trim(displaymap[2].ToArray()).Length == 1, ref data);
                displaymap[5] = Segment(5, s => s.Trim(displaymap[3].ToArray()).Length == 1, ref data);
                displaymap[6] = Segment(6, s => s.Trim(displaymap[5].ToArray()).Length == 1, ref data);
                displaymap[0] = Segment(6, ref data);

                var valuetoadd = string.Join("",
                    numbers.Select(num =>
                        displaymap.Where(s => s.Value.Length == num.Length && s.Value.Trim(num.ToArray()).Length == 0)
                            .Select(s => s.Key)
                            .Sum()
                            .ToString()
                        ))
                    .ToInt();

                displayvalues.Add(valuetoadd);
            }
            return displayvalues.Aggregate((a, b) => a + b);
        }

        public string Segment(int length, Func<string, bool> exp, ref List<string> data)
        {
            var segment = data.Where(s => s.Length == length).Where(exp).Distinct().SingleOrDefault() ?? "";
            if (!string.IsNullOrEmpty(segment)) data.RemoveAll(s => s == segment);
            return segment;
        }

        public string Segment(int length, ref List<string> data)
            => Segment(length, s => true, ref data);
    }
}

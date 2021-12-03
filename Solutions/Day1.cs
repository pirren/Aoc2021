using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day1 : DayBase
    {
        public override string ProblemName => "Sonar Sweep";

        public override int Day => 1;

        public override object PartOne(string[] data)
            => Enumerable.Range(1, data.Length - 1)
            .Select(i => new
            {
                Increased = data[i].ToInt() > data[i - 1].ToInt()
            }).Count(s => s.Increased);

        public override object PartTwo(string[] data)
            => Enumerable.Range(1, data.Length - 1)
                .Select(i => new
                {
                    Increased = data.Skip(i).Take(3).Select(s => s.ToInt()).Sum() > data.Skip(i - 1).Take(3).Select(s => s.ToInt()).Sum()
                }).Count(s => s.Increased);
    }
}

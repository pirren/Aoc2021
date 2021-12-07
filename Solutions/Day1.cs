using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day1 : DayBase
    {
        public override string ProblemName => "Sonar Sweep";
        public override int Day => 1;

        public override object PartOne(string indata)
        {
            var data = indata.Split('\n').Select(int.Parse).ToArray();
            return Enumerable.Range(1, data.Length - 1)
            .Count(i =>
                data[i] > data[i - 1]
            );
        }

        public override object PartTwo(string indata)
        {
            var data = indata.Split('\n').Select(int.Parse).ToArray();
            return Enumerable.Range(1, data.Length - 1)
            .Count(i =>
                data.Skip(i).Take(3).Sum() > data.Skip(i - 1).Take(3).Sum()
            );
        }
    }
}

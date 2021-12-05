using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day1 : DayBase
    {
        public override string ProblemName => "Sonar Sweep";
        public override bool UseSample => true;
        public override int Day => 1;

        public override object PartOne(string indata)
        {
            var parsedData = indata.Split('\n');
            
            return Enumerable.Range(1, parsedData.Length - 1)
            .Select(i => new
            {
                Increased = parsedData[i].ToInt() > parsedData[i - 1].ToInt()
            }).Count(s => s.Increased);
        }

        public override object PartTwo(string indata)
        {
            var parsedData = indata.Split('\n');
            return Enumerable.Range(1, parsedData.Length - 1)
                .Select(i => new
                {
                    Increased = parsedData.Skip(i).Take(3).Select(int.Parse).Sum() > parsedData.Skip(i - 1).Take(3).Select(int.Parse).Sum()
                }).Count(s => s.Increased);
        }
    }
}

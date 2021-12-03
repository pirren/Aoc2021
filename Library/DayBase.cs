namespace Aoc2021.Library
{
    public abstract class DayBase : ISolver
    {
        public string[] Data => File.ReadAllLines(Path.Combine("indata", Day + ".in"));
        public abstract string ProblemName { get; }
        public abstract int Day { get; }
        public abstract object PartOne(string[] Data);
        public abstract object PartTwo(string[] Data);
    }
}

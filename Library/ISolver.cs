namespace Aoc2021.Library
{
    public interface ISolver
    {
        public string ProblemName { get; }
        public int Day { get; }
        public string[] Data { get; }
        public object PartOne(string[] Data);
        public object PartTwo(string[] Data);
    }

}

namespace Aoc2021.Library
{
    public interface ISolver
    {
        public string ProblemName { get; }
        public int Day { get; }
        public string Indata { get; }
        public object PartOne(string Indata);
        public object PartTwo(string Indata);
    }

}

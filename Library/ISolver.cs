namespace Aoc2021.Library
{
    public interface ISolver
    {
        public string ProblemName { get; }
        public int Day { get; }
        public string[] Data { get; }
        public IEnumerable<object> Solve();
    }

}

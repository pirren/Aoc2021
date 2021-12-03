namespace Aoc2021.Library
{
    public abstract class DayBase : ISolver
    {
        public string[] Data => File.ReadAllLines(Path.Combine("indata", Day + ".in"));
        
        public abstract string ProblemName { get; }

        public abstract int Day { get; }

        public abstract IEnumerable<object> Solve();
    }
}

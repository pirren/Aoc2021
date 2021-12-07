using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day7 : DayBase
    {
        public override string ProblemName => "The Treachery of Whales";
        public override int Day => 7;

        public override object PartOne(string indata) => FuelSpent(fuel => fuel, indata);
        public override object PartTwo(string indata) => FuelSpent(fuel => fuel * (1 + fuel) / 2, indata);

        public object FuelSpent(Func<int, int> cost, string indata)
        {
            int[] crabpos = indata.Trim().Split(',').Select(int.Parse).ToArray();
            var low = crabpos.Min();
            var max = crabpos.Max();

            return Enumerable.Range(low, max - low + 1).Min(i => crabpos.Select(pos => Math.Abs(pos - i)).Select(cost).Sum());
        }
    }
}

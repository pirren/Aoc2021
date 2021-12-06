using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day6 : DayBase
    {
        public override string ProblemName => "Lanternfish";
        public override int Day => 6;

        public override object PartOne(string indata) => Simulate(indata, 80);
        public override object PartTwo(string indata) => Simulate(indata, 256);

        long Simulate(string indata, int days)
        {
            var data = indata.Trim().Split(',').Select(long.Parse).ToArray();

            Dictionary<int, long> fish = Enumerable.Range(0, 9).ToDictionary(key => key, val => (long)data.Count(num => num == val));
            int day = 0;
            
            do
            {
                Dictionary<int, long> copy = new();
                for (int idx = 0; idx < fish.Count; idx++)
                {
                    if (idx > 0) {
                        try
                        {
                            copy.Add(idx - 1, copy.GetValueOrDefault(idx - 1, 0) + fish[idx]);
                        }
                        catch
                        {
                            copy[idx - 1] = copy.GetValueOrDefault(idx - 1, 0) + fish[idx];
                        }
                    }
                    else
                    {
                        copy[6] = fish.GetValueOrDefault(idx, 0);
                        copy[8] = fish.GetValueOrDefault(idx, 0);
                    }
                }

                fish = copy;
                day++;
            } while (day < days);

            return fish.Sum(val => val.Value);
        }
    }
}

using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day3 : DayBase
    {
        public override string ProblemName => "Binary Diagnostic";

        public override int Day => 3;

        private static List<int> data = new();
        private static int reportItemLength;

        public override object PartOne(string indata)
        {
            reportItemLength = indata.Split("\r\n").First().Length;
            data = ParseData(indata.Split("\r\n"));

            int gammarate = Enumerable.Range(0, reportItemLength)
                .Select(pos => data.Count(b => (b & (1 << pos)) != 0) > data.Count() / 2 ? 1 << pos : 0)
                .Sum();
            var epsilonrate = ~gammarate & (1 << reportItemLength) - 1;

            return gammarate * epsilonrate;
        }

        public override object PartTwo(string indata)
        {
            reportItemLength = indata.Split("\r\n").First().Length;
            data = ParseData(indata.Split("\r\n"));

            return LifeSupportRating();
        }

        private List<int> ParseData(string[] data)
            => data.Select(d => Convert.ToInt32(d, 2)).ToList();

        private int LifeSupportRating() => OxygenValue(new(data)) * CarbonDioxideValue(new(data));

        private int OxygenValue(List<int> remaining)
        {
            int oxygenValue = 0;

            for (int pos = reportItemLength - 1; pos >= 0; pos--)
            {
                if (remaining.Count == 1) return remaining[0];

                var ones = remaining.Where(c => (c & (1 << pos)) != 0);
                var oneCount = ones.Count();
                var zeroCount = remaining.Count - oneCount;

                if (oneCount > zeroCount || oneCount == zeroCount)
                {
                    oxygenValue += 1 << pos;
                    remaining.RemoveAll(item => !ones.Contains(item));
                }
                else remaining.RemoveAll(item => ones.Contains(item));
            }

            return oxygenValue;
        }

        private int CarbonDioxideValue(List<int> remaining)
        {
            int carbondioxideValue = 0;

            for (int pos = reportItemLength - 1; pos >= 0; pos--)
            {
                if (remaining.Count == 1) return remaining[0];

                var ones = remaining.Where(c => (c & (1 << pos)) != 0);
                var oneCount = ones.Count();
                var zeroCount = remaining.Count - oneCount;

                if (oneCount > zeroCount)
                    remaining.RemoveAll(item => ones.Contains(item));
                else if (oneCount == zeroCount)
                    remaining.RemoveAll(item => ones.Contains(item));
                else
                {
                    carbondioxideValue += 1 << pos;
                    remaining.RemoveAll(item => !ones.Contains(item));
                }
            }

            return carbondioxideValue;
        }
    }
}

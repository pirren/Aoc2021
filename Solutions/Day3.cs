using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day3 : DayBase
    {
        public override string ProblemName => "Binary Diagnostic";

        public override int Day => 3;

        private static List<int> diagnostics = new();
        private static int reportItemLength;

        public override object PartOne(string indata)
        {
            reportItemLength = indata.Split(Environment.NewLine).First().Length;
            diagnostics = ParseDiagnostics(indata.Split(Environment.NewLine));

            int gammaRate = Enumerable.Range(0, reportItemLength)
                .Select(pos => diagnostics.Count(b => (b & (1 << pos)) != 0) > diagnostics.Count() / 2 ? 1 << pos : 0)
                .Sum();

            return gammaRate * (4095 ^ gammaRate);
        }

        public override object PartTwo(string indata)
        {
            reportItemLength = indata.Split(Environment.NewLine).First().Length;
            diagnostics = ParseDiagnostics(indata.Split(Environment.NewLine));

            return LifeSupportRating();
        }

        private List<int> ParseDiagnostics(string[] data)
            => data.Select(d => Convert.ToInt32(d, 2)).ToList();

        private int LifeSupportRating() => OxygenValue(new(diagnostics)) * CarbonDioxideValue(new(diagnostics));

        private int OxygenValue(List<int> remainingDiagnostics)
        {
            int sum = 0;

            for (int pos = reportItemLength - 1; pos >= 0; pos--)
            {
                if (remainingDiagnostics.Count == 1) return remainingDiagnostics[0];

                var ones = remainingDiagnostics.Where(c => (c & (1 << pos)) != 0);
                var oneCount = ones.Count();
                var zeroCount = remainingDiagnostics.Count - oneCount;

                if (oneCount > zeroCount || oneCount == zeroCount)
                {
                    sum += 1 << pos;
                    remainingDiagnostics.RemoveAll(item => !ones.Contains(item));
                }
                else remainingDiagnostics.RemoveAll(item => ones.Contains(item));
            }

            return sum;
        }

        private int CarbonDioxideValue(List<int> remainingDiagnostics)
        {
            int sum = 0;

            for (int pos = reportItemLength - 1; pos >= 0; pos--)
            {
                if (remainingDiagnostics.Count == 1) return remainingDiagnostics[0];

                var ones = remainingDiagnostics.Where(c => (c & (1 << pos)) != 0);
                var oneCount = ones.Count();
                var zeroCount = remainingDiagnostics.Count - oneCount;

                if (oneCount > zeroCount)
                    remainingDiagnostics.RemoveAll(item => ones.Contains(item));
                else if (oneCount == zeroCount)
                    remainingDiagnostics.RemoveAll(item => ones.Contains(item));
                else
                {
                    sum += 1 << pos;
                    remainingDiagnostics.RemoveAll(item => !ones.Contains(item));
                }
            }

            return sum;
        }
    }
}

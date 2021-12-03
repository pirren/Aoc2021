using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day3 : DayBase
    {
        public override string ProblemName => "Binary Diagnostic";

        public override int Day => 3;

        public override IEnumerable<object> Solve()
        {
            yield return PartOne(Data);
            yield return PartTwo(Data);
        }

        private static List<int> diagnostics = new();
        private static int reportItemLength;

        long PartOne(string[] data)
        {
            reportItemLength = data.First().Length;
            diagnostics = ParseDiagnostics(data);

            int gammaRate = Enumerable.Range(0, reportItemLength)
                .Select(pos => diagnostics.Count(b => (b & (1 << pos)) != 0) > diagnostics.Count() / 2 ? 1 << pos : 0)
                .Sum();

            return gammaRate * (4095 ^ gammaRate);
        }

        long PartTwo(string[] data)
        {
            reportItemLength = data.First().Length;
            diagnostics = ParseDiagnostics(data);

            return CalculateLifeSupportRating();
        }

        private List<int> ParseDiagnostics(string[] data)
            => data.Select(d => Convert.ToInt32(d, 2)).ToList();

        private int CalculateLifeSupportRating() => GetOxygenRating(new(diagnostics)) * GetCo2ScrubberRating(new(diagnostics));

        private int GetOxygenRating(List<int> remainingDiagnostics)
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

        private int GetCo2ScrubberRating(List<int> remainingDiagnostics)
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

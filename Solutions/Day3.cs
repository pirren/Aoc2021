using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day3 : DayBase
    {
        public override string ProblemName => "Binary Diagnostic";
        public override int Day => 3;

        private int itemlength = 0;

        public override object PartOne(string indata)
        {
            itemlength = indata.Split("\r\n").First().Length;
            var data = indata.Split("\r\n").Select(d => Convert.ToInt32(d, 2)).ToList();

            int gammarate = Enumerable.Range(0, itemlength)
                .Select(pos => data.Count(b => (b & (1 << pos)) != 0) > data.Count / 2 ? 1 << pos : 0)
                .Sum();
            var epsilonrate = ~gammarate & (1 << itemlength) - 1;

            return gammarate * epsilonrate;
        }

        public override object PartTwo(string indata)
        {
            itemlength = indata.Split("\r\n").First().Length;
            var ratingdata = indata.Split("\r\n").ToList();

            return GetRating(new List<string>(ratingdata), RatingType.Oxygen) * GetRating(new List<string>(ratingdata), RatingType.CarbonDioxide);
        }

        int GetRating(List<string> rating, RatingType type)
        {
            for (int pos = 0; pos < itemlength; pos++)
            {
                if (rating.Count == 1) break;

                var onescount = rating.Select(str => new string(str.Reverse().ToArray())).Count(c => (Convert.ToInt32(c, 2) & (1 << pos)) != 0);
                var isone = false;

                if(type == RatingType.Oxygen)
                    isone = onescount > rating.Count / 2 || onescount == rating.Count - onescount;
                else 
                    isone = onescount < (double)rating.Count / 2 && onescount != rating.Count - onescount;

                if (isone) rating.RemoveAll(item => item[pos] != '1');
                else rating.RemoveAll(item => item[pos] != '0');
            }
            return Convert.ToInt32(rating[0], 2);
        }

        enum RatingType
        {
            Oxygen,
            CarbonDioxide
        }
    }
}

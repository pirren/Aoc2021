using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day14 : DayBase
    {
        public override string Name => "Extended Polymerization";
        public override int Day => 14;
        public override bool UseSample => true;

        Dictionary<string, List<string>> Rules = new();
        Dictionary<string, long> Template = new();

        List<char> Edges = new();

        public override object PartOne(string indata)
        {
            ParseInput(indata);

            return Count(1);
        }

        private long Count(int steps)
        {
            for(int i = 0; i < steps; i++)
            {
                var counter = Template.Select(s => s.Key).ToDictionary(s => s, v => 0L);
                Template.Where(s => s.Value != 0).ForEach(type =>
                {
                    Rules[type.Key].ForEach(x =>
                    {
                        counter[x] += type.Value;
                    });
                });
                Template = counter;
            }

            //var test = 
            Dictionary<string, long> result = Template.Select(s => s.Key)
                .SelectMany(s => s)
                .Distinct()
                .ToDictionary(
                    key => key.ToString(), 
                    val => 0L
                );

            result.ForEach(x => result[x.Key] = Template.Where(s => s.Key[0] != s.Key[1] && s.Key.Contains(x.Key) && s.Value > 1).Sum(s => s.Value));
            //Template.Where(s => s.Key[0] == s.Key[1]).ForEach(s => result[s.Key[..1]] += s.Value);
            //result.ForEach(x => result[x.Key] = Template.Where(s => s.Key[0] == s.Key[1] && s.Key.Contains(x.Key) && s.Value > 1).Sum(s => s.Value));


            var t = Template.Where(t => t.Value > 0).Select(s => s.Key).GroupBy(s => s.Select(x => x));


            return Template.Select(s => s.Value).Max() - Template.Select(s => s.Value).Min();
            //for (int step = 0; step < steps; step++)
            //{
            //    List<Pair> newpairs = new();
            //    for (int x = 0; x < Pairs.Count; x++)
            //    {
            //        var left = Pairs[x].First();
            //        var right = Pairs[x].Last();

            //        var newchar = Rules[new string(Pairs[x].Couple)];

            //        newpairs.Add(new Pair { Couple = new[] { left, newchar } });
            //        newpairs.Add(new Pair { Couple = new[] { newchar, right } });
            //    }
            //    Pairs = newpairs;
            //}

            ////Dictionary<char, int> letterFrequency = Pairs.SelectMany(x => x.Couple).Distinct().ToDictionary(s => s, v => 0);

            //var totalcount = Pairs.Where((s, i) => i % 2 != 0).SelectMany(x => x.Couple).GroupBy(s => s).Select(v => v.Count());
            //return totalcount.Max() - totalcount.Min();
        }

        public override object PartTwo(string indata)
        {
            ParseInput(indata);

            return Count(40);
        }

        //public List<Pair> Next(Pair p) => Rules[p];

        void ParseInput(string indata)
        {

            Rules = indata.Trim().Split("\r\n\r\n")[1].Split("\r\n").Select(s => s.Split(" -> "))
                .ToDictionary(s => s[0], v => new List<string>());

            indata.Trim().Split("\r\n\r\n")[1].Split("\r\n").Select(s => s.Split(" -> ")).ToArray().ForEach(row =>
            {
                Rules[row[0]].Add(new string(row[0][0].ToString().Concat(row[1]).ToArray()));
                Rules[row[0]].Add(new string(row[1].Concat(new string(row[0][1].ToString())).ToArray()));
            });

            Template = Rules.Select(rule => rule.Key).ToDictionary(k => k, v => 0L);

            var templateRow = indata.Trim().Split("\r\n\r\n")[0];

            Edges.AddRange(new [] { templateRow.First(), templateRow.Last() });

            Enumerable.Range(0, templateRow.Length - 1).ForEach(idx =>
            {
                Template[new string(new[] { templateRow[idx], templateRow[idx + 1] })] = 1;
            });
        }
    }

    internal static partial class Ext
    {

    }
}

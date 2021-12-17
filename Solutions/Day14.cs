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

        public override object PartOne(string indata)
        {
            ParseInput(indata);

            return QuantifyElements(0);
        }

        private long QuantifyElements(int steps)
        {
            Polymerize(steps);


            return Template.Select(s => s.Value).Max() - Template.Select(s => s.Value).Min();
        }

        private void Polymerize(int steps)
        {
            if (steps == 0) return;
            do
            {
                var counter = Template.Select(s => s.Key).ToDictionary(s => s, v => 0L);
                Template.Where(s => s.Value != 0).ForEach(type =>
                {
                    Next(type.Key).ForEach(x =>
                    {
                        counter[x] += type.Value;
                    });
                });
                Template = counter;
                steps++;
            } while (steps > 0);
        }

        public override object PartTwo(string indata)
        {
            ParseInput(indata);

            return QuantifyElements(40);
        }

        public List<string> Next(string idx) => Rules[idx];

        void ParseInput(string indata)
        {
            var data = indata.Trim().Split("\r\n\r\n");
            var template = data[0];

            Rules = data[1].Split("\r\n").Select(s => s.Split(" -> "))
                .ToDictionary(s => s[0], v => new List<string>());

            indata.Trim().Split("\r\n\r\n")[1].Split("\r\n").Select(s => s.Split(" -> ")).ToArray().ForEach(row =>
            {
                Rules[row[0]].Add(new string(row[0][0].ToString().Concat(row[1]).ToArray()));
                Rules[row[0]].Add(new string(row[1].Concat(new string(row[0][1].ToString())).ToArray()));
            });

            Template = Rules.Select(rule => rule.Key).ToDictionary(k => k, v => 0L);

            Enumerable.Range(0, template.Length - 1).ForEach(idx =>
            {
                Template[new string(new[] { template[idx], template[idx + 1] })] = 1;
            });
        }
    }

    internal static partial class Ext
    {

    }
}

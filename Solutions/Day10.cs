using Aoc2021.Library;
using System.Drawing;

namespace Aoc2021.Solutions
{
    public class Day10 : DayBase
    {
        public override string Name => "Syntax Scoring";
        public override int Day => 10;
        public override bool UseSample => true;

        Dictionary<char, char> matchmap = new()
        {
            { '}', '{' },
            { ']', '[' },
            { ')', '(' },
            { '>', '<' },
        };

        public override object PartOne(string indata)
        {
            var syntaxlines = indata.Trim().Split("\r\n");
            Dictionary<char, int> scoretable = new()
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
            };

            return GetCorruptLines(syntaxlines).Select(ch => scoretable[ch.Item2]).Aggregate((a, b) => a + b);
        }

        public override object PartTwo(string indata)
        {
            var syntaxlines = indata.Trim().Split("\r\n");
            Dictionary<char, int> scoretable = new()
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 },
            };

            List<int> scores = new ();
            foreach (var line in syntaxlines.Where(line => !GetCorruptLines(syntaxlines).Select(items => items.Item1).Contains(line)))
            {
                // only loop incomplete lines
                var linelength = line.Length;

                var syntax = string.Empty;

                for (int i = 0; i < linelength; i++)
                {
                    if (IsStartTag(line[i]))
                        syntax += line[i];

                    if (IsEndTag(line[i]))
                    {
                        if (matchmap[line[i]] == syntax.Last())
                            syntax = syntax.Substring(0, syntax.Length - 1);
                    }
                }

                var totalscore = 0;
                for(int i = 0; i < syntax.Length; i++)
                {
                    totalscore += syntax.Select(s => matchmap.FirstOrDefault(x => x.Value == s).Key).Select(s => scoretable[s] * totalscore).Aggregate((a, b) => 5 * a);
                    scores.Add(totalscore);
                }

                //var baselinescore = syntax.Select(s => matchmap.FirstOrDefault(x => x.Value == s).Key).Select(s => scoretable[s]);
                //foreach(var score in baselinescore)


                //scores.Add(syntax.Select(s => matchmap.FirstOrDefault(x => x.Value == s).Key).Select(s => scoretable[s]).Aggregate((a,b) => 5 * a));
            }

            //var debug = scores.Select(s => )

            return 0;
        }

        IEnumerable<(string, char)> GetCorruptLines(string[] syntaxlines)
        {

            List<(string, char)> corruptlines = new();
            foreach (var line in syntaxlines)
            {
                var linelength = line.Length;

                var syntax = string.Empty;
                char firstcorrupt = '\0';

                for (int i = 0; i < linelength; i++)
                {
                    if (IsStartTag(line[i]))
                        syntax += line[i];

                    if (IsEndTag(line[i]))
                    {
                        if (matchmap[line[i]] == syntax.Last())
                            syntax = syntax.Substring(0, syntax.Length - 1);
                        else
                        {
                            firstcorrupt = line[i];
                            break;
                        }
                    }
                }
                if (firstcorrupt != '\0')
                    corruptlines.Add((line, firstcorrupt));
            }
            return corruptlines;
        }

        public bool IsStartTag(char c)
            => new[] { '{', '[', '(', '<' }.Contains(c);

        public bool IsEndTag(char c)
            => new[] { '}', ']', ')', '>' }.Contains(c);
    }

}

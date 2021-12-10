using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day10 : DayBase
    {
        public override string Name => "Syntax Scoring";
        public override int Day => 10;

        readonly Dictionary<char, char> tagpairs = new()
        {
            { '}', '{' },
            { ']', '[' },
            { ')', '(' },
            { '>', '<' },
        };

        public override object PartOne(string indata)
        {
            var syntaxlines = indata.Trim().Split("\r\n");
            Dictionary<char, int> scoretable = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };

            return GetCorruptLines(syntaxlines).Select(ch => scoretable[ch.Item2]).Aggregate((a, b) => a + b);
        }

        public override object PartTwo(string indata)
        {
            var syntaxlines = indata.Trim().Split("\r\n");
            var corruptlines = GetCorruptLines(syntaxlines).ToArray();
            var scores = AutocompleteScores(syntaxlines, corruptlines.Select(s => s.Item1));
            return scores.OrderBy(s => s).Skip((syntaxlines.Length - corruptlines.Length) / 2).First();
        }

        IEnumerable<long> AutocompleteScores(string[] syntaxlines, IEnumerable<string> corruptlines)
        {
            Dictionary<char, int> scoretable = new() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 }, };

            foreach (var line in syntaxlines.Where(line => !corruptlines.Contains(line)))
            {
                var syntax = string.Empty;

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].IsStartTag())
                        syntax += line[i];

                    if (line[i].IsEndTag())
                    {
                        if (tagpairs[line[i]] == syntax.Last())
                            syntax = syntax[0..^1];
                    }
                }

                long totalscore = 0;
                foreach (var match in syntax.Select(s => tagpairs.FirstOrDefault(x => x.Value == s).Key).Reverse()) // reverse the order
                    totalscore = (totalscore * 5) + scoretable[match];

                yield return totalscore; // first iter 335857468
            }
        }

        IEnumerable<(string, char)> GetCorruptLines(string[] syntaxlines)
        {
            foreach (var line in syntaxlines)
            {
                var syntax = string.Empty;
                char firstcorruptsign = '\0';

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].IsStartTag())
                        syntax += line[i];

                    if (line[i].IsEndTag())
                    {
                        if (tagpairs[line[i]] == syntax.Last())
                            syntax = syntax[0..^1];
                        else
                        {
                            firstcorruptsign = line[i];
                            break;
                        }
                    }
                }
                if (firstcorruptsign != '\0')
                    yield return (line, firstcorruptsign);
            }
        }
    }

    public static class Ext
    {
        public static bool IsStartTag(this char c) => new[] { '{', '[', '(', '<' }.Contains(c);
        public static bool IsEndTag(this char c) => new[] { '}', ']', ')', '>' }.Contains(c);
    }
}

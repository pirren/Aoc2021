using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day13 : DayBase
    {
        public override string Name => "Transparent Origami";
        public override int Day => 13;
        public override bool UseSample => true;

        List<string> paper = new List<string>();
        List<(char, int)> instructions = new();

        public override object PartOne(string indata)
        {
            ParseInput(indata);

            return 0;
        }

        public override object PartTwo(string indata)
        {
            ParseInput(indata);

            return 0;
        }

        void ParseInput(string indata)
        {
            var lines = indata.Trim().Split("\r\n\r\n").Select(s => s.Split("\r\n")).ToList()[0];
            var cols = lines.Select(s => s[0].ToInt()).Max();
            var rows = lines.Select(s => s[1].ToInt()).Max();

            for(int i = 0; i < cols; i++)
            {
                string row = "";
                for(int j = 0; j < rows; j++)
                {

                }
            }


            instructions = indata.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split("\r\n")).ToList()[1]
                .Select(s => s.Split('=')).Select(instruction => (instruction.First().Last(), instruction.Last().ToInt())).ToList();
        }
    }
}

using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day2 : DayBase
    {
        public override string ProblemName => "Dive!";

        public override int Day => 2;

        public override object PartOne(string indata)
        {
            var instructions = indata.Split('\n').Select(s => s.Split(' '))
                .GroupBy(s => s[0])
                .ToDictionary(k => k.Key, v => v.Select(s => int.Parse(s[1])));

            return (instructions["down"].Sum() - instructions["up"].Sum()) * instructions["forward"].Sum();
        }

        public override object PartTwo(string indata)
        {
            var instructions = indata.Split('\n').Select(s => s.Split(' ')).ToList();
            int aim = 0, depth = 0, pos = 0;

            foreach (var instruction in instructions)
            {
                var value = instruction[1].ToInt();
                if (instruction[0] == "down") aim += value;
                if (instruction[0] == "up") aim -= value;
                if (instruction[0] == "forward")
                {
                    pos += value;
                    depth += aim * value;
                }

            }
            return pos * depth;
        }
    }
}

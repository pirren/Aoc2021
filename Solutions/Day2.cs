using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day2 : DayBase
    {
        public override string ProblemName => "Dive!";

        public override int Day => 2;

        public override object PartOne(string indata)
        {
            var instructions = indata.Split('\n');
            var values = instructions.Select(s => s.Split(' '))
                .GroupBy(s => s[0])
                .ToDictionary(k => k.Key, v => v.ToList().Select(s => int.Parse(s[1])));

            return (values["down"].Sum() - values["up"].Sum()) * values["forward"].Sum();
        }

        public override object PartTwo(string indata)
        {
            var instructions = indata.Split('\n');
            var commands = instructions.Select(s => s.Split(' ')).ToList();
            int aim = 0, depth = 0, pos = 0;

            commands.ForEach(command => { ExecuteCommand(command, ref aim, ref depth, ref pos); });
            return pos * depth;
        }

        private void ExecuteCommand(string[] command, ref int aim, ref int depth, ref int pos)
        {
            var value = command[1].ToInt();
            if (command[0] == "down") aim += value;
            if (command[0] == "up") aim -= value;
            if (command[0] == "forward")
            {
                pos += value;
                depth += aim * value;
            }
        }
    }
}

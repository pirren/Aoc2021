using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day16 : DayBase
    {
        public override string Name => "Packet Decoder";
        public override int Day => 16;
        public override bool UseSample => true;

        private static readonly Dictionary<char, string> hexToBinary = new() {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };

        public class Packet
        {
            public int Version => throw new NotImplementedException();
            public int TypeId => throw new NotImplementedException();
        }

        public override object PartOne(string indata)
        {
            return 0;
        }

        public override object PartTwo(string indata)
        {
            return 0;
        }
    }

    internal static partial class Ext
    {
    }
}

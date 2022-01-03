using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day16 : DayBase
    {
        public override string Name => "Packet Decoder";
        public override int Day => 16;
        public override bool UseSample => true;

        private static readonly Dictionary<char, string> hexNumbers = new()
        {
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
            public long Version { get; set; }
            public long TypeId { get; set; }
            public int Number { get; set; }
            public List<Packet> Subpackets { get; set; } = new();
        }

        public override object PartOne(string indata)
        {
            var packet = DecodeTransmission(indata, new Queue<byte>());
            return 0;
        }

        private (Packet, string) DecodeTransmission(string data, Queue<byte> queue)
        {
            Packet packet = new();
            if (!queue.Any()) queue = EnqueueTransmission(data);

            var version = queue.DequeueLong(3);
            var typeid = queue.DequeueLong(3); // Any other than 4 is an operator

            byte leadingpoint = 1;

            while(queue.Any())
            {
                if(version == 4 && leadingpoint != 0) // single packet
                {
                    leadingpoint = queue.Dequeue();
                    packet.Number = Convert.ToInt32(queue.DequeueLength(4), 2);

                    if (queue.Count < 4) queue = new Queue<byte>(); // dump noise
                }
                else // operation
                {
                    var lengthtypeid = queue.Dequeue(); // parse length typeId
                    if(lengthtypeid == 0) // 0 = 15 bits representing length of all sub packets
                    {
                        var subpacketslength = queue.DequeueLong(15);
                        var submessage = queue.DequeueLength((int)subpacketslength);
                        var subpackets = new List<Packet>();
                        while (!string.IsNullOrEmpty(submessage))
                        {
                            (var subpacket, submessage) = DecodeTransmission(submessage, new());
                            subpackets.Add(subpacket);
                        }
                    }
                    else if (lengthtypeid == 1) // 1 = 11 bits representing the number of subpackets immediately following
                    {
                        var subpacketamount = queue.DequeueLong(11);
                        var subpackets = new List<Packet>();
                        while(subpackets.Count < subpacketamount)
                        {
                            var(subpacket,_) = DecodeTransmission(string.Empty, queue);
                            subpackets.Add(subpacket);
                        }

                    }
                }
            }

            packet.Version = version;
            packet.TypeId = typeid;

            return (packet, data);
        }

        private Queue<byte> EnqueueTransmission(string message, bool convert = true)
        {
            Queue<byte> queue = new();
            if (convert) message.ForEach(x => { hexNumbers[x].ForEach(ch => queue.Enqueue((byte)ch.ToInt())); });
            else message.ForEach(x => { queue.Enqueue((byte)x.ToInt()); });
            return queue;
        }

        public override object PartTwo(string indata)
        {
            return 0;
        }
    }

    internal static partial class Ext
    {
        public static string DequeueLength(this Queue<byte> q, int length)
        {
            string s = "";
            for (int i = 0; i < length; i++) s += q.Dequeue();
            return s;
        }

        public static long DequeueLong(this Queue<byte> q, int amount)
        {
            long result = 0L;
            for(int i = 0; i < amount; i++) result = (result << 1) | q.Dequeue();
            return result;
        }
    }
}

using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day11 : DayBase
    {
        public override string Name => "Dumbo Octopus";
        public override int Day => 11;

        public override object PartOne(string indata)
        {
            var data = indata.Trim().Split("\r\n");
            var rows = data.Length;
            var cols = data.First().Length;

            var octopus = ModelOctopuses(data, rows, cols).ToList().Link(rows, cols);

            Enumerable.Range(0, 100).ForEach(day => {
                octopus.ForEach(oct => oct.Increase(day));
            });

            return octopus.Sum(oct => oct.Flashes);
        }

        public override object PartTwo(string indata)
        {
            var data = indata.Trim().Split("\r\n");
            var rows = data.Length;
            var cols = data.First().Length;

            var octopus = ModelOctopuses(data, rows, cols).ToList().Link(rows, cols);

            int steps = 0;
            while (true)
            {
                octopus.ForEach(oct => oct.Increase(steps));

                if (octopus.All(s => s.Value == 0))
                    break;

                steps++;
            }
            return steps + 1;
        }

        IEnumerable<Octopus> ModelOctopuses(string[] data, int rows, int cols)
        {
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                    yield return new Octopus(data[x][y].ToInt(), x, y);
        }
    }

    public class Octopus
    {
        public int X { get; set; }
        public int Y { get; set; }

        public List<Octopus> Linked { get; set; } = new();

        public int Value { get; private set; }
        public int Flashes { get; private set; }

        private int lastDayIncreased = 0;

        public void Increase(int day)
        {
            if (lastDayIncreased < day || Value != 0)
            {
                Value = Value == 9 ? 0 : Value + 1;
                if (Value == 0)
                    Flashes++;

                lastDayIncreased = day;

                if (Value == 0) // chain on flash
                {
                    foreach (var oct in Linked)
                        oct.Increase(day);
                }
            }
        }

        public Octopus(int initialvalue, int x, int y)
        {
            X = x;
            Y = y;
            Value = initialvalue;
            Flashes = 0;
        }
    }

    public static partial class Ext
    {
        public static List<Octopus> Link(this IEnumerable<Octopus> octopuses, int rows, int cols)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    var iter = octopuses.First(oct => oct.X == x && oct.Y == y);

                    // link latitudes and diagonals
                    if (y > 0) // add north 
                    {
                        iter.Linked.Add(octopuses.First(oct => oct.X == x && oct.Y == y - 1));
                        if (x < cols - 1) // add north east
                            iter.Linked.Add(octopuses.First(oct => oct.X == x + 1 && oct.Y == y - 1));
                    }

                    if (x < cols - 1) // add east
                    {
                        iter.Linked.Add(octopuses.First(oct => oct.X == x + 1 && oct.Y == y));
                        if (y < rows - 1) // add south east
                            iter.Linked.Add(octopuses.First(oct => oct.X == x + 1 && oct.Y == y + 1));
                    }

                    if (y < rows - 1) // add south
                    {
                        iter.Linked.Add(octopuses.First(oct => oct.X == x && oct.Y == y + 1));
                        if (x > 0) // add south west
                            iter.Linked.Add(octopuses.First(oct => oct.X == x - 1 && oct.Y == y + 1));
                    }

                    if (x > 0) // add west
                    {
                        iter.Linked.Add(octopuses.First(oct => oct.X == x - 1 && oct.Y == y));
                        if (y > 0) // add north west 
                            iter.Linked.Add(octopuses.First(oct => oct.X == x - 1 && oct.Y == y - 1));
                    }
                }
            }
            return octopuses.ToList();
        }
    }
}

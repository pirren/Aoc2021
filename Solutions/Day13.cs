using Aoc2021.Library;
using System.Drawing;

namespace Aoc2021.Solutions
{
    public class Day13 : DayBase
    {
        public override string Name => "Transparent Origami";
        public override int Day => 13;

        public Sheet Paper = new();

        public List<Instruction> Instructions = new();

        public override object PartOne(string indata)
        {
            ParseInput(indata);

            return ExecuteFolds(1);
        }

        public override object PartTwo(string indata)
        {
            ParseInput(indata);

            return ExecuteFolds();
        }

        private int ExecuteFolds() => ExecuteFolds(Instructions.Count);

        private int ExecuteFolds(int count)
        {
            Instructions.Take(count).ForEach(ins =>
            {
                FoldPaper(ins);
            });
            return Paper.Dots.Count;
        }

        private Sheet GetHalf(Instruction ins)
            => ins.Direction switch
            {
                Direction.Vertical => new Sheet()
                {
                    Dots = new List<Point>(Paper.Dots).Where(s => s.Y > ins.Lines).ToList(),
                    SizeX = Paper.SizeX,
                    SizeY = ins.Lines
                },
                Direction.Horizontal => new Sheet()
                {
                    Dots = new List<Point>(Paper.Dots).Where(s => s.X > ins.Lines).ToList(),
                    SizeX = ins.Lines,
                    SizeY = Paper.SizeY
                },
                _ => throw new Exception("Bad instruction")
            };

        private void FoldPaper(Instruction ins)
        {
            Sheet half = GetHalf(ins);
            Paper.Erase(half);

            if (ins.Direction == Direction.Vertical)
                half.ShiftVertical();
            else if (ins.Direction == Direction.Horizontal)
                half.ShiftHorizontal();

            Paper.Fold(half);
        }

        void ParseInput(string indata)
        {
            var split = indata.Trim().Split("\r\n\r\n").Select(s => s.Split("\r\n")).ToList();
            Paper.Dots = split[0].Select(x => x.Split(','))
                .Select(s => new Point
                {
                    X = s[0].ToInt(),
                    Y = s[1].ToInt(),
                }).ToList();

            Paper.SizeX = Paper.Dots.Max(s => s.X) + 1;
            Paper.SizeY = Paper.Dots.Max(s => s.Y) + 1;

            Instructions = split[1].Select(s => s.Split('='))
                .Select(s => new Instruction
                {
                    Direction = s[0].Last() == 'x' ? Direction.Horizontal : Direction.Vertical,
                    Lines = s[1].ToInt()
                }).ToList();
        }

        public enum Direction
        {
            Vertical,
            Horizontal
        }

        public class Instruction
        {
            public Direction Direction { get; set; }
            public int Lines { get; set; }
        }

        public class Sheet
        {
            public void Fold(Sheet half)
            {
                Dots.AddRange(half.Dots.Where(s => !Dots.Contains(s)));
            }

            public void Erase(Sheet negative)
            {
                Dots.RemoveAll(s => negative.Dots.Contains(s));
                SizeX = negative.SizeX;
                SizeY = negative.SizeY;
            }

            public void ShiftVertical()
            {
                Dots = Dots.Select(s => new Point 
                { 
                    Y = ~Convert.ToInt32(s.Y) + SizeY * 2 + 1,
                    X = s.X 
                }).ToList(); 
            }

            public void ShiftHorizontal()
            {
                Dots = Dots.Select(s => new Point
                {
                    Y = s.Y,
                    X = ~Convert.ToInt32(s.X) + SizeX * 2 + 1,
                }).ToList();
            }

            public int SizeX { get; set; }
            public int SizeY { get; set; }
            public List<Point> Dots { get; set; } = new List<Point>();
        }
    }
}

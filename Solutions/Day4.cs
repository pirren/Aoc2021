using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day4 : DayBase
    {
        public override string ProblemName => "Giant Squid";

        public override int Day => 4;

        public override object PartOne(string indata) => PlayBingo(indata);

        public override object PartTwo(string indata) => PlayBingo(indata, continuePlaying: true);

        public long PlayBingo(string indata, bool continuePlaying = false)
        {
            List<BingoBoard> boards = StartBingoGame(indata);

            var bingolines = indata.Split(Environment.NewLine + Environment.NewLine).First().Split(',').Select(s => s.ToInt()).ToList();
            int numbersdrawn = 0;
            while (bingolines.Any())
            {
                var bingonumber = bingolines.PopAt(0);
                numbersdrawn++;

                foreach (var board in boards)
                    board.Mark(bingonumber);

                if (numbersdrawn >= 5)
                {
                    var winningboards = new List<BingoBoard>();
                    foreach (var board in boards)
                    {
                        var bingoResult = board.Bingo();
                        if (bingoResult != 0)
                        {
                            if(!continuePlaying) return bingoResult * bingonumber;

                            winningboards.Add(board);
                            if (boards.Count == 1) return bingoResult * bingonumber;
                        }
                    }
                    foreach (var board in winningboards)
                        boards.Remove(board);
                }
            }

            return 0;
        }

        public class BingoBoard
        {
            public List<Row> Rows { get; set; } = new();

            public int GetUnmarkedSum() => Rows.Select(row => row.GetUnmarkedSum()).Sum();

            public int Bingo()
            {
                if (Rows.Any(s => s.Numbers.All(num => num == -1))) return GetUnmarkedSum();

                for(int i = 0; i < Rows.Count; i++)
                {
                    if (Rows.Select(s => s.GetAt(i)).All(s => s == -1))
                        return GetUnmarkedSum();
                }
                return 0;
            }

            public void Mark(int target)
            {
                var rows = Rows.Where(s => s.Numbers.Any(num => num == target));
                foreach (var row in rows) row.Mark(target);
            }

            public class Row
            {
                public int GetUnmarkedSum() => Numbers.Where(num => num != -1).Sum();
                public int GetAt(int index) => Numbers[index];
                public void Mark(int target)
                {
                    int index = Numbers.IndexOf(target);
                    Numbers[index] = -1;
                    if (!Numbers.All(num => num != target)) Mark(target);
                }
                public List<int> Numbers { get; set; }
            }
        }

        public List<BingoBoard> StartBingoGame(string indata)
        {
            List <BingoBoard> boards = new();
            var boardData = indata.Split(Environment.NewLine + Environment.NewLine).Skip(1).Select(s => s.Split(Environment.NewLine));

            foreach (var data in boardData)
            {
                var board = new BingoBoard();
                board.Rows.AddRange(data.Select(s => new BingoBoard.Row
                {
                    Numbers = s.Replace("  ", " ").Split(' ')
                        .Where(w => !string.IsNullOrEmpty(w))
                        .Select(s => s.ToInt())
                        .ToList()
                }));
                boards.Add(board);
            }
            return boards;
        }
    }
}

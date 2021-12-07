using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day4 : DayBase
    {
        public override string ProblemName => "Giant Squid";

        public override int Day => 4;

        public override object PartOne(string indata) => BoardScore(indata);

        public override object PartTwo(string indata) => BoardScore(indata, keeplooking: true);

        public long BoardScore(string indata, bool keeplooking = false)
        {
            List<Board> boards = GetBoards(indata);

            var bingolines = indata.Split("\r\n\r\n").First().Split(',').Select(int.Parse).ToList();
            int numbersdrawn = 0;
            while (bingolines.Any())
            {
                var bingonumber = bingolines.PopAt(0);
                numbersdrawn++;

                foreach (var board in boards)
                    board.Mark(bingonumber);

                if (numbersdrawn >= 5)
                {
                    var winningboards = new List<Board>();
                    foreach (var board in boards)
                    {
                        if (board.IsBingo())
                        {
                            var unmarkedSum = board.UnmarkedSum;
                            if(!keeplooking) return unmarkedSum * bingonumber;

                            winningboards.Add(board);
                            if (boards.Count == 1) return unmarkedSum * bingonumber;
                        }
                    }
                    foreach (var board in winningboards)
                        boards.Remove(board);
                }
            }

            return 0;
        }

        public class Board
        {
            public List<Row> Rows { get; set; } = new();

            public int UnmarkedSum => Rows.Select(row => row.GetUnmarkedSum()).Sum();

            public bool IsBingo()
            {
                if (Rows.Any(s => s.Numbers.All(num => num == -1))) return true;

                for(int i = 0; i < Rows.Count; i++)
                    if (Rows.Select(s => s.GetAt(i)).All(s => s == -1))
                        return true;

                return false;
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
                    if (!Numbers.All(num => num != target)) 
                        Mark(target);
                }
                public List<int> Numbers { get; set; } = new();
            }
        }

        public List<Board> GetBoards(string indata)
        {
            List <Board> boards = new();
            var boardData = indata.Split("\r\n\r\n").Skip(1).Select(s => s.Split("\r\n"));
            var debug = indata.Split("\r\n\r\n").Skip(1).Select(s => s.Split('\n'));

            foreach (var data in boardData)
            {
                var board = new Board();
                board.Rows.AddRange(
                    data.Select(s => new Board.Row
                    {
                        Numbers = s.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToList()
                    })
                );
                boards.Add(board);
            }
            return boards;
        }
    }
}

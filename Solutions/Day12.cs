using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day12 : DayBase
    {
        public override string Name => "Passage Pathing";
        public override int Day => 12;

        public override object PartOne(string indata)
        {
            var data = indata.Trim().Split("\r\n");
            var tree = LinkTree(data);

            var startnode = tree.FirstOrDefault(s => s.IsStart) ?? new("");

            var debug = CalculatePaths(startnode, new());
            Console.Write('\n');
            foreach (var line in debug)
                Console.WriteLine(line);


            return CalculatePaths(startnode, new()).Count();
        }

        public override object PartTwo(string indata)
        {
            var data = indata.Trim().Split("\r\n");
            var tree = LinkTree(data);

            var startVertex = tree.FirstOrDefault(s => s.IsStart) ?? new("");


            // buggrättar skiten
            var debug = RevisitCalculatePaths(startVertex);
            Console.Write('\n');
            foreach (var line in debug)
                Console.WriteLine(line);


            return RevisitCalculatePaths(startVertex).Count();
        }

        private List<Node> ValidNextNodes(List<Node> queue, Node edge)
        {
            return edge.Edges
                    .Where(node => !node.IsStart)
                    .Where(node => node.IsBig || !queue.Contains(node) ||
                                queue.Where(n => n.IsSmall).Count() == queue.Where(n => n.IsSmall).Distinct().Count())
                    .ToList();
        }

        List<string> RevisitCalculatePaths(Node startVertex)
            => RevisitCalculatePaths(startVertex, new());

        // part two
        private List<string> RevisitCalculatePaths(Node vertex, List<Node> queue)
        {
            if (vertex.IsEnd)
            {
                queue.Add(vertex);
                return queue.Deque();
            }

            List<string> paths = new();
            queue.Add(vertex);

            foreach (var edge in ValidNextNodes(queue, vertex))
            {
                if (edge.IsStart) // start automatisk skip
                    continue;

                if (queue.Select(s => s.Id).Contains(edge.Id) && edge.IsSmall)
                {
                    if (queue.GroupBy(s => s.Id).Select(s => s.Count()).Any(s => s >= 2))
                    {
                        continue;
                    }
                }

                paths.AddRange(RevisitCalculatePaths(edge, new(queue)));
            }
            return paths.Distinct().ToList();
        }

        // part one
        private List<string> CalculatePaths(Node node, List<Node> queue)
        {
            if (node.IsEnd)
            {
                queue.Add(node);
                return queue.Deque();
            }

            List<string> paths = new();
            queue.Add(node);

            foreach (var edge in node.GetEdges())
            {
                if (queue.Select(s => s.Id).Contains(edge.Id) && edge.IsSmall)
                {
                    continue;
                }
                paths.AddRange(CalculatePaths(edge, new(queue)));
            }
            return paths.Distinct().ToList();
        }

        private IEnumerable<Node> LinkTree(string[] scanvals)
        {
            List<Node> results = new();
            foreach (var scan in scanvals)
            {
                var fromval = scan.Split('-')[0];
                var toval = scan.Split('-')[1];

                Node from = new("");
                Node to = new("");

                if (results.Any(bst => bst.Id == fromval))
                    from = results.First(s => s.Id == fromval);
                else
                    from = new(fromval);

                if (results.Any(bst => bst.Id == toval))
                    to = results.First(s => s.Id == toval);
                else
                    to = new(toval);

                from.AddLink(to);
                to.AddLink(from);

                results.AddRange(new[] { from, to }.Where(s => !results.Contains(s)));
            }

            return results;
        }
    }

    public class Node
    {
        public string Id { get; set; }
        public List<Node> Edges { get; private set; }
        public bool IsSmall => Id.ToLower() == Id && !Id.Equals("start") && !Id.Equals("end");
        public bool IsBig => Id.ToUpper() == Id && !Id.Equals("start") && !Id.Equals("end");
        public bool IsStart => Id.Equals("start");
        public bool IsEnd => Id.Equals("end");
        public void AddLink(Node node) => Edges.Add(node);
        public IEnumerable<Node> GetEdges()
        {
            if (Edges.Any())
                foreach (var value in Edges)
                    yield return value;
        }

        public Node(string n)
        {
            Id = n;
            Edges = new();
        }
    }

    internal static partial class Ext
    {
        public static List<string> Deque(this List<Node> vertices)
            => new(new[] { string.Join(",", vertices.Select(s => s.Id)) });
    }
}

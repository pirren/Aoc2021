using Aoc2021.Library;

namespace Aoc2021.Solutions
{
    public class Day12 : DayBase
    {
        public override string Name => "Passage Pathing";
        public override int Day => 12;

        Dictionary<string, Node> nodeTree = new();

        public override object PartOne(string indata)
        {
            ParseInput(indata);
            Node startnode = nodeTree["start"];

            return CalculatePath(new(new[] { startnode })).Count;
        }

        public override object PartTwo(string indata)
        {
            ParseInput(indata);
            Node startnode = nodeTree["start"];

            return CalculatePath(new(new[] { startnode }), true).Count;
        }

        public void ParseInput(string indata)
        {
            var nodes = indata.Trim().Split("\r\n").SelectMany(row => row.Split('-')).Distinct().Select(id => new Node(id)).ToArray();

            nodeTree = nodes.ToDictionary(key => key.Id);

            indata.Trim().Split("\r\n").ToArray().ForEach(row =>
            {
                var valOne = row.Split('-')[0];
                var valTwo = row.Split('-')[1];

                nodeTree[valOne].Links.Add(valTwo);
                nodeTree[valTwo].Links.Add(valOne);
            });
        }

        private List<List<Node>> CalculatePath(List<Node> currentPath, bool revisit = false)
        {
            var last = currentPath.Last();

            if (last.IsEnd) return new List<List<Node>> { currentPath };

            var validNewNodes = revisit ? ValidNextNodesRevisit(currentPath, last) : ValidNextNodes(currentPath, last);
            if(!validNewNodes.Any()) return new List<List<Node>>();

            return validNewNodes
                .Select(nextEdge => new List<List<Node>> { currentPath, new List<Node> { nextEdge } }.SelectMany(s => s).ToList())
                .SelectMany(path => CalculatePath(path, revisit)).Where(s => s.Any()).ToList();
        }

        private List<Node> ValidNextNodes(List<Node> currentPath, Node node)
            => node.Links
                    .Select(x => nodeTree[x])
                    .Where(s => !s.IsStart && (!currentPath.Contains(s) || s.IsBig))
                    .ToList();

        private List<Node> ValidNextNodesRevisit(List<Node> currentPath, Node node)
            => node.Links
                    .Select(s => nodeTree[s])
                    .Where(node => !node.IsStart)
                    .Where(node => node.IsBig || !currentPath.Contains(node) ||
                                   currentPath.Where(node => node.IsSmall).Count() == currentPath.Where(node => node.IsSmall).Distinct().Count())
                    .ToList();

        public class Node
        {
            public string Id { get; set; }
            public List<string> Links { get; private set; }
            public bool IsSmall => Id.ToLower() == Id && !Id.Equals("start") && !Id.Equals("end");
            public bool IsBig => Id.ToUpper() == Id && !Id.Equals("start") && !Id.Equals("end");
            public bool IsStart => Id.Equals("start");
            public bool IsEnd => Id.Equals("end");
            public Node(string Id)
            {
                this.Id = Id;
                Links = new();
            }
        }
    }
}

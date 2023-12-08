namespace Day8;

public class Wasteland
{
    private readonly char[] _directions;
    private readonly List<WasteNode> _nodes;

    public Wasteland(string filePath)
    {
        var parts = File.ReadAllText(filePath).Replace("\r\n", "\n").Split("\n\n");
        _directions = parts[0].ToCharArray();
        var nodeLookup = new Dictionary<string, WasteNode>();
        _nodes = new List<WasteNode>();
        foreach (var nodeData in parts[1].Split("\n"))
        {
            var nodeId = nodeData[..3];
            var node = new WasteNode { Identifier = nodeId };
            nodeLookup.Add(node.Identifier, node);
            _nodes.Add(node);
        }

        foreach (var nodeData in parts[1].Split("\n"))
        {
            var nodeId = nodeData[..3];
            var idLeft = nodeData[7..10];
            var idRight = nodeData[12..15];
            nodeLookup[nodeId].Left = nodeLookup[idLeft];
            nodeLookup[nodeId].Right = nodeLookup[idRight];
        }
    }

    public void SolvePart1()
    {
        Console.WriteLine("-- Part 1: --");
        var steps = CountSteps("AAA", "ZZZ");
        Console.WriteLine($"It takes {steps} steps to get to ZZZ.");
    }

    public void SolvePart2()
    {
        Console.WriteLine("\n-- Part 2: --");
        var runners = _nodes
            .Where(it => it.Identifier.EndsWith("A")).ToList();
        var targets = runners.Select(FindTarget).ToList();

        var positions = new List<int>();
        for (var i = 0; i < targets.Count; i++)
        {
            var s = CountSteps(runners[i].Identifier, targets[i]);
            positions.Add(s);
        }
        var loops = targets.Select(id => CountSteps(id, id)).ToList();

        long steps = positions[0];
        long stepSize = loops[0];
        while (positions.Where((pos, index) => (steps - pos) % loops[index] != 0).Any())
        {
            steps += stepSize;
        }
        Console.WriteLine($"It takes {steps} steps before everyone is on something that ends with Z.");
    }

    private int CountSteps(string from, string to)
    {
        var current = _nodes.First(it => it.Identifier == from);
        var steps = 0;
        while (steps <= 0 || current.Identifier != to)
        {
            current = _directions[steps % _directions.Length] == 'L' ? current.Left : current.Right;
            steps++;
        }

        return steps;
    }

    private string FindTarget(WasteNode start)
    {
        var current = start;
        var steps = 0;
        while (!current.Identifier.EndsWith("Z"))
        {
            current = _directions[steps % _directions.Length] == 'L' ? current.Left : current.Right;
            steps++;
        }

        return current.Identifier;
    }
}

internal record WasteNode
{
    public required string Identifier { get; init; }
    public WasteNode? Left { get; set; }
    public WasteNode? Right { get; set; }
};

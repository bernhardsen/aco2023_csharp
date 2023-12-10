namespace Day9;

public class MirageMaintainer
{
    private readonly List<List<int>> _values;

    public MirageMaintainer(string filePath)
    {
        _values = File.ReadAllLines(filePath)
            .Select(line =>
                line.Split(" ")
                    .Select(num => Convert.ToInt32(num))
                    .ToList())
            .ToList();
    }

    public int SolvePart1()
    {
        return _values.Select(CalculateNext).Sum();
    }

    public int SolvePart2()
    {
        return _values.Select(CalculatePrev).Sum();
    }

    private int CalculateNext(List<int> values)
    {
        List<List<int>> layers = MakeLayers(values);
        for (var i = layers.Count - 2; i >= 0; i--)
        {
            layers[i].Add(layers[i + 1].Last() + layers[i].Last());
        }

        return layers[0].Last();
    }

    private int CalculatePrev(List<int> values)
    {
        List<List<int>> layers = MakeLayers(values);
        layers.Reverse();
        return layers.Aggregate(0, (acc, layer) => layer[0] - acc);
    }

    private List<List<int>> MakeLayers(List<int> values)
    {
        var layers = new List<List<int>> { values };
        var lastLayer = layers[0];
        while (!IsAllZeros(layers.Last()))
        {
            layers.Add(CreateLayer(layers.Last()));
        }
        
        return layers;
    }

    private static bool IsAllZeros(List<int> values)
    {
        return values.All(it => it == 0);
    }

    private List<int> CreateLayer(List<int> values)
    {
        List<int> layer = new();
        for (var i = 0; i < values.Count - 1; i++)
        {
            layer.Add(values[i + 1] - values[i]);
        }

        return layer;
    }
}
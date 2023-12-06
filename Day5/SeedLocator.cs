namespace Day5;

public class SeedLocator
{
    private readonly List<long> _seedLocations;
    private readonly List<List<LocationMapper>> _mappers;
    
    public SeedLocator(string path)
    {
        var data = File.ReadAllText(path);
        var parts = data.Replace("\r\n", "\n").Split("\n\n");
        _seedLocations = parts[0]
            .Split(": ")[1]
            .Split(" ")
            .Select(it => Convert.ToInt64(it))
            .ToList();

        _mappers = new List<List<LocationMapper>>
        {
            PartToLocationMapper(parts[1].Split("\n").ToList()),
            PartToLocationMapper(parts[2].Split("\n").ToList()),
            PartToLocationMapper(parts[3].Split("\n").ToList()),
            PartToLocationMapper(parts[4].Split("\n").ToList()),
            PartToLocationMapper(parts[5].Split("\n").ToList()),
            PartToLocationMapper(parts[6].Split("\n").ToList()),
            PartToLocationMapper(parts[7].Split("\n").ToList()),
        };

    }
    
    public void SolvePart1()
    {
        var lowest = _seedLocations.Select(location =>
            _mappers.Aggregate(location, (loc, mapper) => LookUpLocation(mapper, loc))).Min();
        Console.WriteLine($"The lowest possible location is {lowest}.");
    }

    public void SolvePart2()
    {
        var lowest = _seedLocations.Chunk(2).Select(locs =>
        {
            var l = new List<LongRange> { new LongRange(locs[0], locs[0] + locs[1]) };
            return _mappers.Aggregate(l, (ranges, mapper) =>
                ranges.SelectMany(r => MakeRangesFromRange(mapper, r)).ToList())
                .Select(r => r.Start)
                .Min();
        }).Min();
        Console.WriteLine($"The lowest possible location is {lowest}.");
    }

    private long LookUpLocation(List<LocationMapper> mappers, long location)
    {
        try
        {
            var mapper = mappers
                .First(m => location >= m.SourceStart && location < m.SourceStart + m.Length);
            return location + mapper.Translation;
        }
        catch (Exception)
        {
            return location;
        }
    }

    private List<LongRange> MakeRangesFromRange(List<LocationMapper> mappers, LongRange range)
    {
        List<LongRange> outRanges = new();
        while (true)
        {
            try
            {
                var mapper = mappers.First(m => range.Start >= m.SourceStart && range.Start < m.SourceStart + m.Length);
                if (range.Start - mapper.SourceStart + (range.End - range.Start) <= mapper.Length)
                {
                    outRanges.Add(new LongRange(range.Start + mapper.Translation, range.End + mapper.Translation));
                    return outRanges;
                }
                outRanges.Add(new LongRange(range.Start + mapper.Translation, mapper.SourceStart + mapper.Length + mapper.Translation));
                range = range with { Start = mapper.SourceStart + mapper.Length };
            }
            catch (Exception)
            {
                var end = mappers.Select(m => m.SourceStart).Where(it => it > range.Start).Min();
                if (end > range.End)
                {
                    outRanges.Add(range);
                    return outRanges;
                }
                outRanges.Add(range with { End = end - 1 });
                range = range with { Start = end };
            }
        }
    }

    private List<LocationMapper> PartToLocationMapper(List<string> lines)
    {
        lines.RemoveAt(0);
        return lines.Select(line =>
        {
            var values = line.Split(" ").Select(it => Convert.ToInt64(it)).ToList();
            return new LocationMapper(values[1], values[0] - values[1], values[2]);
        }).ToList();
    }
}

internal record LocationMapper(long SourceStart, long Translation, long Length);
internal record LongRange(long Start, long End);

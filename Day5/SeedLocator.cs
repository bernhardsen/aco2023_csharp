namespace Day5;

public class SeedLocator
{
    private readonly List<long> _seedLocations;

    private readonly List<LocationMapper> _seedToSoil;
    private readonly List<LocationMapper> _soilToFertilizer;
    private readonly List<LocationMapper> _fertilizerToWater;
    private readonly List<LocationMapper> _waterToLight;
    private readonly List<LocationMapper> _lightToTemperature;
    private readonly List<LocationMapper> _temperatureToHumidity;
    private readonly List<LocationMapper> _humidityToLocation;
    
    public SeedLocator(string path)
    {
        var data = File.ReadAllText(path);
        var parts = data.Replace("\r\n", "\n").Split("\n\n");
        _seedLocations = parts[0]
            .Split(": ")[1]
            .Split(" ")
            .Select(it => Convert.ToInt64(it))
            .ToList();

        _seedToSoil = PartToLocationMapper(parts[1].Split("\n").ToList());
        _soilToFertilizer = PartToLocationMapper(parts[2].Split("\n").ToList());
        _fertilizerToWater = PartToLocationMapper(parts[3].Split("\n").ToList());
        _waterToLight = PartToLocationMapper(parts[4].Split("\n").ToList());
        _lightToTemperature = PartToLocationMapper(parts[5].Split("\n").ToList());
        _temperatureToHumidity = PartToLocationMapper(parts[6].Split("\n").ToList());
        _humidityToLocation = PartToLocationMapper(parts[7].Split("\n").ToList());
    }
    
    public void SolvePart1()
    {
        var lowest = _seedLocations
            .Select(location => LookUpLocation(_seedToSoil, location))
            .Select(location => LookUpLocation(_soilToFertilizer, location))
            .Select(location => LookUpLocation(_fertilizerToWater, location))
            .Select(location => LookUpLocation(_waterToLight, location))
            .Select(location => LookUpLocation(_lightToTemperature, location))
            .Select(location => LookUpLocation(_temperatureToHumidity, location))
            .Select(location => LookUpLocation(_humidityToLocation, location))
            .Min();
        Console.WriteLine($"The lowest possible location is {lowest}.");
    }

    public void SolvePart2()
    {
        var lowest = _seedLocations.Chunk(2).Select(locs =>
        {
            return MakeRangesFromRange(_seedToSoil, new LongRange(locs[0], locs[0] + locs[1]))
                .SelectMany(r => MakeRangesFromRange(_soilToFertilizer, r))
                .SelectMany(r => MakeRangesFromRange(_fertilizerToWater, r))
                .SelectMany(r => MakeRangesFromRange(_waterToLight, r))
                .SelectMany(r => MakeRangesFromRange(_lightToTemperature, r))
                .SelectMany(r => MakeRangesFromRange(_temperatureToHumidity, r))
                .SelectMany(r => MakeRangesFromRange(_humidityToLocation, r))
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
                if (range.Start - mapper.SourceStart + (range.End - range.Start)  <= mapper.Length)
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

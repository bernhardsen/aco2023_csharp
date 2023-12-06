namespace Day6;

public class RaceCalculator
{
    private readonly string[] _rawData;

    public RaceCalculator(string filePath)
    {
        _rawData = File.ReadAllLines(filePath);
    }

    public void SolvePart1()
    {
        var result = CalculateLeniency(GetMultiRace());
        Console.WriteLine("-- Part 1: --");
        Console.WriteLine($"When we multiply all possible ways together we get {result}");
    }
    
    public void SolvePart2()
    {
        var result = CalculateLeniency(GetSingleRace());
        Console.WriteLine("-- Part 2: --");
        Console.WriteLine($"If its just one race, there are {result} ways to win.");
    }

    private IEnumerable<Race> GetMultiRace()
    {
        var num = (_rawData[0].Length - 8) / 7;
        return Enumerable.Range(0, num).Select(i =>
        {
            var start = 9 + i * 7;
            return new Race(
                Convert.ToInt64(_rawData[0].Substring(start, 6)),
                Convert.ToInt64(_rawData[1].Substring(start, 6))
                );
        }).ToList();
    }

    private List<Race> GetSingleRace()
    {
        return new List<Race>() { new Race(
            Convert.ToInt64(_rawData[0].Substring(9).Replace(" ", "")),
                Convert.ToInt64(_rawData[1].Substring(9).Replace(" ", ""))) };
    }

    private long CalculateLeniency(IEnumerable<Race> races)
    {
        return races.Aggregate(1L, (ways, race) => ways * (race.Time - (long) double.Ceiling((race.Time - double.Sqrt(race.Time * race.Time - (race.Distance + 1) * 4.0)) / 2.0) * 2 + 1));
    }
}

internal record Race(long Time, long Distance);

using System.Text.RegularExpressions;

namespace Day2;

public class CubeConundrum
{
    private readonly List<Game> _games;
    
    public CubeConundrum(string inputFile)
    {
        var input = File.ReadAllText(inputFile);
        _games = ParseGames(input);
    }

    public void SolvePart1()
    {
        // Find all the games that are possible, and sum up the ID of those
        var sumOfIds =_games
                .Where(game => game.Rounds.All(IsPossible))
                .Select(game => game.Id)
                .Sum();
        Console.WriteLine("-- Part 1: --");
        Console.WriteLine($"The sum of the ID of the possible games is {sumOfIds}.");
    }

    public void SolvePart2()
    {
        var sumOfPowers = _games
            .Select(game =>
                game.Rounds.Select(r => r.Red).Max() *
                    game.Rounds.Select(r => r.Green).Max() *
                    game.Rounds.Select(r => r.Blue).Max())
            .Sum();
        Console.WriteLine("-- Part 2: --");
        Console.WriteLine($"The sum of the powers of all the games is {sumOfPowers}.");
    }

    private static bool IsPossible(Round round)
    {
        return round is { Red: <= 12, Green: <= 13, Blue: <= 14 };
    }

    private static List<Game> ParseGames(string input)
    {
        var gameRe = new Regex(@"Game ([0-9]+): ([^\n]+)\n");
        var matches = gameRe.Matches(input);
        return matches.Select(match =>
        {
            var id = Convert.ToInt32(match.Groups[1].Value);
            var rounds = match.Groups[2].Value.Split(";")
                .Select(round =>
                {
                    var red = 0;
                    var green = 0;
                    var blue = 0;
                    foreach (string cubeCount in round.Split(","))
                    {
                        var parts = cubeCount.Trim().Split(" ");
                        switch (parts[1])
                        {
                            case "red":
                                red = Convert.ToInt32(parts[0]);
                                break;
                            case "green":
                                green = Convert.ToInt32(parts[0]);
                                break;
                            case "blue":
                                blue = Convert.ToInt32(parts[0]);
                                break;
                        }
                    }

                    return new Round(red, green, blue);
                }).ToList();
            return new Game(id, rounds);
        }).ToList();
    }
}

internal record Game(int Id, List<Round> Rounds);
internal record Round(int Red, int Green, int Blue);

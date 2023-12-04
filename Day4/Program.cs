
Console.WriteLine("Advent of Code 2023!");
Console.WriteLine("=== Day 4: Scratchcards ===");

var lines = File.ReadAllLines("input.txt");

var winners = lines.Select(line =>
    line.Split(": ")[1]
        .Split(" | ")[0]
        .Split(" ")
        .Where(it => it != "")
        .Select(it => Convert.ToInt32(it))
        .ToList()).ToList();

var picks = lines.Select(line =>
    line.Split(": ")[1]
        .Split(" | ")[1]
        .Split(" ")
        .Where(it => it != "")
        .Select(it => Convert.ToInt32(it))
        .ToList()).ToList();

Console.WriteLine("-- Part 1: --");
var score = 0; 
for (var i = 0; i < picks.Count; i++)
{
    var correctPicks = picks[i].Count(pick => winners[i].Contains(pick));
    score += CalcScore(correctPicks);
}
Console.WriteLine($"The tickets give us {score} points in total.");

Console.WriteLine("\n-- Part 2: --");
var ticketsInHand = Enumerable.Repeat(1, picks.Count).ToList();
for (var i = 0; i < picks.Count; i++)
{
    var correctPicks = picks[i].Count(pick => winners[i].Contains(pick));
    for (var ticketId = i + 1; ticketId < i + correctPicks + 1; ticketId++)
    {
        if (ticketId < ticketsInHand.Count)
        {
            ticketsInHand[ticketId] += ticketsInHand[i];
        }
    }
}
Console.WriteLine($"We end up with a total of {ticketsInHand.Sum()} tickets.");

return;

int CalcScore(int i)
{
    if (i == 0) return 0;
    return 1 << i - 1;
}

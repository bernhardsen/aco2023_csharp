namespace Day7;

public class CamelCards
{
    private readonly string _rawData;
    
    public CamelCards(string filePath)
    {
        _rawData = File.ReadAllText(filePath).Replace("\r\n", "\n");
    }

    public void SolvePart1()
    {
        Console.WriteLine("-- Part 1: --");
        var hands = ParseHands(_rawData);
        var score = ScoreHands(hands);
        Console.WriteLine($"The total score of all the hands is {score}.");
    }

    public void SolvePart2()
    {
        Console.WriteLine("-- Part 2: --");
        var hands = ParseHands(_rawData.Replace("J", "?"));
        var score = ScoreHands(hands);
        Console.WriteLine($"When we play with jokers, the total score is {score}.");
    }

    private int ScoreHands(List<CamelHand> hands)
    {
        return hands.OrderBy(hand => hand.Score)
            .ThenBy(hand => hand.Cards[0])
            .ThenBy(hand => hand.Cards[1])
            .ThenBy(hand => hand.Cards[2])
            .ThenBy(hand => hand.Cards[3])
            .ThenBy(hand => hand.Cards[4])
            .Select((hand, index) => hand.Value * (index + 1))
            .Sum();
    }

    private List<CamelHand> ParseHands(string data)
    {
        return data.Split("\n").Select(ParseHand).ToList();
    }

    private CamelHand ParseHand(string handData)
    {
        var parts = handData.Split(" ");
        var cards = parts[0].ToCharArray().Select(CalculateCardValue).ToArray();
        return new CamelHand(
            cards,
            Convert.ToInt32(parts[1]),
            CalculateHandScore(cards)
        );
    }

    private static int CalculateCardValue(char c) => c switch
    {
        '?' => 1,
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => 11,
        'T' => 10,
        _ => Convert.ToInt32(c.ToString())
    };

    private int CalculateHandScore(int[] cards)
    {
        var duplicates = new Dictionary<int, int>();
        var nonJokers = cards.Where(it => it != 1).ToList();
        var jokerCount = 5 - nonJokers.Count;
        if (jokerCount == 5)
        {
            return 7;
        }
        foreach (var card in nonJokers)
        {
            duplicates[card] = duplicates.GetValueOrDefault(card, 0) + 1;
        }

        var dupOrdered = duplicates.Values.Order().Reverse().ToArray();
        dupOrdered[0] += jokerCount;
        switch (dupOrdered[0])
        {
            case 5: return 7;
            case 4: return 6;
            case 3: return dupOrdered[1] == 2 ? 5 : 4;
            case 2: return dupOrdered[1] == 2 ? 3 : 2;
        }
        return 1;
    }
}

internal record CamelHand(int[] Cards, int Value, int Score);

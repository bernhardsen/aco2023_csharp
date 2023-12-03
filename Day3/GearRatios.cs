namespace Day3;

public class GearRatios
{
    private int _width;
    private int _height;
    private char[ , ] _mapData = {{}};
    
    public GearRatios(string fileName)
    {
        ParseInput(fileName);
    }

    private void ParseInput(string fileName)
    {
        var allLines = File.ReadAllLines(fileName);
        _width = allLines[0].Length;
        _height = allLines.Length;
        _mapData = new char[_width, _height];
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _mapData[x, y] = allLines[y][x];
            }
        }
    }

    public void SolvePart1()
    {
        var totalSum = 0;
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (IsNumeric(x, y))
                {
                    // Its a number, figure out how long it is
                    var hasSymbol = IsSymbol(x - 1, y - 1) || IsSymbol(x - 1, y) || IsSymbol(x - 1, y + 1)
                                    || IsSymbol(x, y - 1) || IsSymbol(x, y + 1);
                    var numWidth = 1;
                    var numAsStr = $"{_mapData[x, y]}";
                    while (IsNumeric(x + numWidth, y))
                    {
                        hasSymbol |= IsSymbol(x + numWidth, y - 1) || IsSymbol(x + numWidth, y + 1);
                        numAsStr += _mapData[x + numWidth, y];
                        numWidth += 1;
                    }
                    // Check if any of the surrounding characters are symbols
                    hasSymbol |= IsSymbol(x + numWidth, y - 1) || IsSymbol(x + numWidth, y)
                                                                   || IsSymbol(x + numWidth, y + 1);
                    if (hasSymbol)
                    {
                        totalSum += Convert.ToInt32(numAsStr);
                    }
                    
                    x += numWidth - 1;
                }
            }
        }
        Console.WriteLine("--- Part 1: ---");
        Console.WriteLine($"The total sum of numbers that are touching symbols is {totalSum}.");
    }

    public void SolvePart2()
    {
        var totalSum = 0;
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (IsSymbol(x, y))
                {
                    // Find touching numbers
                    List<int> foundNumbers = new List<int>();
                    CheckAndAddNumber(x - 1, y - 1, foundNumbers);
                    CheckAndAddNumber(x - 1, y, foundNumbers);
                    CheckAndAddNumber(x - 1, y + 1, foundNumbers);
                    
                    CheckAndAddNumber(x, y - 1, foundNumbers);
                    CheckAndAddNumber(x, y + 1, foundNumbers);
                    
                    CheckAndAddNumber(x + 1, y - 1, foundNumbers);
                    CheckAndAddNumber(x + 1, y, foundNumbers);
                    CheckAndAddNumber(x + 1, y + 1, foundNumbers);

                    foundNumbers.Remove(0);

                    if (foundNumbers.Count == 2)
                    {
                        totalSum += foundNumbers[0] * foundNumbers[1];
                    }
                }
            }
        }
        Console.WriteLine("--- Part 2: ---");
        Console.WriteLine($"The total sum of all the gear ratios is {totalSum}.");
    }

    private void CheckAndAddNumber(int x, int y, List<int> numbers)
    {
        var n = GetNumberAtCoord(x, y);
        if (!numbers.Contains(n))
        {
            numbers.Add(n);
        }
    } 

    private bool IsSymbol(int x, int y)
    {
        if (x >= _width || y >= _height || x < 0 || y < 0)
        {
            return false;
        }
        return !IsNumeric(x, y) && _mapData[x, y] != '.';
    }

    private bool IsNumeric(int x, int y)
    {
        if (x >= _width || y >= _height || x < 0 || y < 0)
        {
            return false;
        }
        return _mapData[x, y] >= '0' && _mapData[x, y] <= '9';
    }

    private int GetNumberAtCoord(int x, int y)
    {
        if (!IsNumeric(x, y))
        {
            return 0;
        }
        var start = x;
        while (IsNumeric(start - 1, y))
        {
            start -= 1;
        }

        var end = x;
        while (IsNumeric(end + 1, y))
        {
            end += 1;
        }

        var number = "";
        for (var i = start; i <= end; i++)
        {
            number += _mapData[i, y];
        }
        return Convert.ToInt32(number);
    }
}

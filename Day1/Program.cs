
Console.WriteLine("Advent of Code 2023!");
Console.WriteLine("-- Day 1: Trebuchet?! --");

var input = File.ReadAllLines("input.txt");

var sum1 = input
    .Select(line =>
        {
            var a = line
                .SkipWhile(c => !char.IsDigit(c))
                .First();
            var b = line
                .Reverse()
                .SkipWhile(c => !char.IsDigit(c))
                .First();
            return "" + a + b;
        }
    ).Select(str => Convert.ToInt32(str)).Sum();

var sum2 = input
    .Select(line =>
        line.Replace("one", "o1e")
            .Replace("two", "t2o")
            .Replace("three", "t3e")
            .Replace("four", "f4r")
            .Replace("five", "f5e")
            .Replace("six", "s6x")
            .Replace("seven", "s7n")
            .Replace("eight", "e8t")
            .Replace("nine", "n9e"))
    .Select(line =>
        {
            var a = line
                .SkipWhile(c => !char.IsDigit(c))
                .First();
            var b = line
                .Reverse()
                .SkipWhile(c => !char.IsDigit(c))
                .First();
            return "" + a + b;
        }
    ).Select(str => Convert.ToInt32(str)).Sum();

Console.WriteLine("Part 1:");
Console.WriteLine($"The sum of all calibration values is {sum1}!");
Console.WriteLine("Part 2:");
Console.WriteLine($"When we include numbers written as words, the sum is {sum2}!");

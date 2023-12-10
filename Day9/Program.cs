

using Day9;

Console.WriteLine("Advent of Code 2023!");
Console.WriteLine("=== Day 9: Mirage Maintenance ===");

var solver = new MirageMaintainer("input.txt");

Console.WriteLine("-- Part 1: --");
var sumNext = solver.SolvePart1();
Console.WriteLine($"The sum of all the next values is {sumNext}.");
Console.WriteLine();
Console.WriteLine("-- Part 2: --");
var sumPrev = solver.SolvePart2();
Console.WriteLine($"The sum of all the previous values is {sumPrev}.");

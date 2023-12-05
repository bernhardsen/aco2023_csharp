
using Day5;

Console.WriteLine("Advent of Code 2023!");
Console.WriteLine("Day 5: If You Give A Seed A Fertilizer");

var watch = new System.Diagnostics.Stopwatch();

var solver = new SeedLocator("input.txt");
solver.SolvePart1();
solver.SolvePart2();
Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


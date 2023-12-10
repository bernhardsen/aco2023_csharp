

using Day10;

Console.WriteLine("Advent of Code 2023!");
Console.WriteLine("=== Day 10: Pipe Maze ===");

var solver = new PipeLayer("input.txt");
Console.WriteLine("-- Part 1: --");
int distance = solver.MaxDistanceInLoop();
Console.WriteLine($"The furthest away from the animal is {distance} steps.");
Console.WriteLine();
Console.WriteLine("-- Part 2: --");
int inside = solver.CountTilesInsideLoop();
Console.WriteLine($"There are {inside} squares inside the loop.");

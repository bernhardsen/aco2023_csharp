# Advent of Code 2023: C#

I'm using [Advent of Code 2023](https://adventofcode.com/2023) to learn some new
programming languages, C# is one of them. Don't expect this to be very good code,
I'm still learning.

---

### Day 1: Trebuchet?!
This one was as expected very straight forward. Didn't make a class for this one
since it was a very quick implementation.  

### Day 2: Cube Conundrum
There is one regular expression in there. Didn't really need it, could have done all
the parsing with splitting strings. Didn't bother going back to refactor.
Once the input is parsed, the rest was very straight forward.

### Day 3: Gear Ratios
Not really happy about this one. I feel the solution became very verbose, and ugly.
It works, but yeah... When I reimplemented this in Rust, I did it by building
a list of stuff-on-the-map, instead of bytes representing all positions on the map.
That solution was much more elegant, and would be easier to continue working with.

### Day 4: Scratchcards
Pretty simple task. So simple that I went back to a procedural solution,
and didn't bother creating a class for it.

### Day 5: If You Give A Seed A Fertilizer
First kinda tricky problem of the year. Part 1 was trivial.
Did part 2 with brute force first, and it took about 20 minutes.
Later I refactored into looking up whole ranges at once,
which resulted in a run-time of 4 ms on my machine (for part 2 only).

### Day 6: Wait For It
Pretty simple problem. I had done it in Rust and Python already.
Tried to make this solution even shorter than those.
I think I managed to keep most of the readability, except maybe the `CalculateLeniency` method.
That one is just the algebraic formula I figured out just smashed together into one line.

### Day 7: Camel Cards
Fun problem. Same solution as the other languages. I do like the LINQ statements,
and how the code reads. I think C# is my favorite solution for this problem.

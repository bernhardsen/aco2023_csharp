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

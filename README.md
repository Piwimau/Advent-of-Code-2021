# 🎄 Advent of Code 2021 🎄

This repository contains my solutions for [Advent of Code 2021](https://adventofcode.com/2021),
the first year I ever participated.

## What is Advent of Code?

[Advent of Code](https://adventofcode.com/) is a series of small programming puzzles created by
[Eric Wastl](http://was.tl/). Every day from December 1st to 25th, a puzzle is released alongside an
engaging fictional Christmas story. Each puzzle consists of two parts, the second of which usually
contains some interesting twist or changing requirements and is only unlocked after completing the
first one. The objective is to solve all parts and collect fifty stars ⭐ until December 25th to
save Christmas.

Many users compete on the [global leaderboard](https://adventofcode.com/2021/leaderboard) by
solving the puzzles in an unbelievably fast way in order to get some extra points. Personally,
I see Advent of Code as a fun exercise to do during the Advent season while waiting for Christmas.
I often use it to learn a new programming language (like I did in 2021 with `C#`) or some advanced
programming concepts. I can only encourage you to participate as well - of course in a way that you
find fun. Just get started and learn more about Advent of Code
[here](https://adventofcode.com/2021/about).

## About this project

The solutions for Advent of Code 2021 were originally developed using `.NET 6` and `C# 10` at the
time. Since then I have taken some time to update them to more recent versions (`.NET 8` and
`C# 12`), which allowed me to take advantage of new language features and modern data structures
that either did not exist yet or I did not know about (as I was just starting to learn the whole
ecosystem). These include expression bodies (`=>`), collection expressions (`[...]`), target-typed
`new()`, `Span<T>` and `ReadOnlySpan<T>`, types of the `System.Collections.Immutable` or
`System.Collections.Frozen` namespaces and much more.

For this project and in general when developing software, I strive to produce readable and well
documented source code. However, I also enjoy benchmarking and optimizing my code, which is why I
sometimes implement a less idiomatic, yet more efficient solution at the expense of readability.
In those situations, I try to document my design choices with analogies, possible alternative
solutions and sometimes little sketches to better illustrate the way a piece of code works.

The general structure of this project is as follows:

```
Day 1 - Sonar Sweep/
    Resources/
    Source/
        Program.cs
    Day 1 - Sonar Sweep.csproj
Day 2 - Dive!/
...
Day 25 - Sea Cucumber/
.gitignore
Advent of Code 2021.sln
LICENSE
README.md
```

At the top level, the [solution file](Advent+of+Code+2021.sln) contains 25 standalone projects
for the days of the Advent calendar, organized into separate directories. Each one provides a
corresponding `.csproj` file that can be opened in Visual Studio. In addition, there are the usual
`Source` and `Resources` directories, the latter of which contains the puzzle description and my
personal input for that day. However, [as requested](https://adventofcode.com/2021/about) by the
creator of Advent of Code, these are only present in my own private copy of the repository and
therefore not publicly available.

> If you're posting a code repository somewhere, please don't include parts of Advent of Code like
  the puzzle text or your inputs.

As a consequence, you will have to provide your own inputs for the days, as described in more detail
in the following section.

## Dependencies and usage

If you want to try one of my solutions, follow these steps below:

0. Make sure you have `.NET 8` or a later version installed on your machine.

1. Clone the repository (or download the source code) to a directory of your choice.
   ```shell
   git clone https://github.com/Piwimau/Advent-of-Code-2021 "./Advent of Code 2021"
   cd "./Advent of Code 2021"
   ```

2. Put your input for the day in a file called `input.txt` and copy it to the appropriate resources
   directory. You can get all inputs from the [official website](https://adventofcode.com/2021) if
   you have not downloaded them already.
   ```shell
   cp input.txt "./Day 1 - Sonar Sweep/Resources"
   ```

3. Nagivate into the appropriate day's directory.
   ```shell
   cd "./Day 1 - Sonar Sweep"
   ```

4. Finally, build and run the code. Be sure to build in release mode (as shown below) to take
   advantage of all optimizations and achieve the best performance.
   ```shell
   dotnet build -c Release
   dotnet run
   ```

Alternatively, if you have Visual Studio installed on your machine, simply open the provided
[solution file](Advent+of+Code+2021.sln) and proceed from there.

## Benchmarks

Finally, here are some (non-scientific) benchmarks I created using the fantastic
[BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) package and my main machine (Intel Core
i9-13900HX, 32GB DDR5-5600 RAM) running Windows 11 23H2. All benchmarks include the time spent for
reading the input from disk and parsing it, but not the time for printing the final result.

| Day                              | Min       | Max       | Mean      | Median    | Standard Deviation |
|----------------------------------|-----------|-----------|-----------|-----------|--------------------|
| Day 1 - Sonar Sweep              |  0.060 ms |  0.063 ms |  0.061 ms |  0.061 ms | 0.001 ms           |
| Day 2 - Dive!                    |  0.109 ms |  0.112 ms |  0.110 ms |  0.110 ms | 0.001 ms           |
| Day 3 - Binary Diagnostic        |  0.084 ms |  0.086 ms |  0.085 ms |  0.085 ms | 0.001 ms           |
| Day 4 - Giant Squid              |  1.605 ms |  1.639 ms |  1.622 ms |  1.622 ms | 0.009 ms           |
| Day 5 - Hydrothermal Venture     | 13.532 ms | 13.881 ms | 13.734 ms | 13.740 ms | 0.094 ms           |
| Day 6 - Lanternfish              |  0.022 ms |  0.023 ms |  0.022 ms |  0.022 ms | 0.000 ms           |
| Day 7 - The Treachery of Whales  |  3.289 ms |  3.381 ms |  3.335 ms |  3.341 ms | 0.033 ms           |
| Day 8 - Seven Segment Search     |  0.176 ms |  0.183 ms |  0.180 ms |  0.180 ms | 0.002 ms           |
| Day 9 - Smoke Basin              |  1.004 ms |  1.054 ms |  1.034 ms |  1.036 ms | 0.016 ms           |
| Day 10 - Syntax Scoring          |  0.135 ms |  0.138 ms |  0.137 ms |  0.137 ms | 0.001 ms           |
| Day 11 - Dumbo Octopus           |  1.127 ms |  1.159 ms |  1.143 ms |  1.141 ms | 0.008 ms           |
| Day 12 - Passage Pathing         | 38.544 ms | 39.815 ms | 39.132 ms | 39.048 ms | 0.372 ms           |
| Day 13 - Transparent Origami     |  0.146 ms |  0.149 ms |  0.148 ms |  0.148 ms | 0.001 ms           |
| Day 14 - Extended Polymerization |  0.186 ms |  0.192 ms |  0.189 ms |  0.190 ms | 0.002 ms           |
| Day 15 - Chiton                  | 34.518 ms | 36.790 ms | 35.401 ms | 35.389 ms | 0.642 ms           |
| Day 16 - Packet Decoder          |  0.040 ms |  0.041 ms |  0.040 ms |  0.040 ms | 0.000 ms           |
| Day 17 - Trick Shot              |  0.617 ms |  0.628 ms |  0.623 ms |  0.623 ms | 0.003 ms           |

## License

This project is licensed under the [MIT License](LICENSE). Feel free to experiment with the code,
adapt it to your own preferences, and share it with others.
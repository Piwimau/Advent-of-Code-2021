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
        .gitkeep
    Source/
        Benchmark.cs
        SonarSweep.cs
    Day 1 - Sonar Sweep.csproj
Day 2 - Dive!/
    Resources/
        .gitkeep
    Source/
        Benchmark.cs
        Dive.cs
    Day 2 - Dive!.csproj
...
Day 25 - Sea Cucumber/
    ...
.gitignore
Advent of Code 2021.sln
LICENSE
README.md
```

On the top level, the [solution file](Advent+of+Code+2021.sln) contains 25 standalone projects
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

4. Finally, run the code in release mode to take advantage of all optimizations and achieve the best
   performance.

   ```shell
   dotnet run -c Release
   ```

   Optionally, specify an additional flag `--benchmark` to benchmark the relevant day on your
   machine. Note that in this mode no output for the results of the solved puzzle is produced.

   ```shell
   dotnet run -c Release --benchmark
   ```

If you have Visual Studio installed on your machine, you may also just open the provided
[solution file](Advent+of+Code+2021.sln) and proceed from there.

## Benchmarks

Finally, here are some (non-scientific) benchmarks I created using the fantastic
[BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) package and my main machine (Intel Core
i9-13900HX, 32GB DDR5-5600 RAM) running Windows 11 24H2. All benchmarks include the time spent for
reading the input from disk, as well as printing the puzzle results (although the output is written
to `TextWriter.Null` when benchmarking, which is effectively a no-op and rather fast).

| Day                              | Min        | Max        | Mean       | Median     | Standard Deviation |
|----------------------------------|------------|------------|------------|------------|--------------------|
| Day 1 - Sonar Sweep              |   0.060 ms |   0.063 ms |   0.061 ms |   0.061 ms |  0.001 ms          |
| Day 2 - Dive!                    |   0.109 ms |   0.113 ms |   0.111 ms |   0.111 ms |  0.001 ms          |
| Day 3 - Binary Diagnostic        |   0.083 ms |   0.086 ms |   0.085 ms |   0.084 ms |  0.001 ms          |
| Day 4 - Giant Squid              |   1.601 ms |   1.682 ms |   1.653 ms |   1.658 ms |  0.022 ms          |
| Day 5 - Hydrothermal Venture     |  12.340 ms |  12.907 ms |  12.680 ms |  12.707 ms |  0.164 ms          |
| Day 6 - Lanternfish              |   0.022 ms |   0.023 ms |   0.023 ms |   0.023 ms |  0.000 ms          |
| Day 7 - The Treachery of Whales  |   3.221 ms |   3.321 ms |   3.280 ms |   3.287 ms |  0.026 ms          |
| Day 8 - Seven Segment Search     |   0.182 ms |   0.194 ms |   0.189 ms |   0.189 ms |  0.003 ms          |
| Day 9 - Smoke Basin              |   0.990 ms |   1.064 ms |   1.019 ms |   1.014 ms |  0.019 ms          |
| Day 10 - Syntax Scoring          |   0.134 ms |   0.139 ms |   0.136 ms |   0.135 ms |  0.001 ms          |
| Day 11 - Dumbo Octopus           |   1.097 ms |   1.123 ms |   1.110 ms |   1.108 ms |  0.007 ms          |
| Day 12 - Passage Pathing         |  39.537 ms |  40.785 ms |  40.243 ms |  40.309 ms |  0.406 ms          |
| Day 13 - Transparent Origami     |   0.148 ms |   0.152 ms |   0.150 ms |   0.151 ms |  0.001 ms          |
| Day 14 - Extended Polymerization |   0.188 ms |   0.199 ms |   0.192 ms |   0.192 ms |  0.003 ms          |
| Day 15 - Chiton                  |  33.354 ms |  35.100 ms |  34.392 ms |  34.419 ms |  0.551 ms          |
| Day 16 - Packet Decoder          |   0.040 ms |   0.042 ms |   0.041 ms |   0.041 ms |  0.000 ms          |
| Day 17 - Trick Shot              |   0.584 ms |   0.606 ms |   0.595 ms |   0.595 ms |  0.006 ms          |
| Day 18 - Snailfish               |  18.189 ms |  18.735 ms |  18.540 ms |  18.539 ms |  0.167 ms          |
| Day 19 - Beacon Scanner          | 244.310 ms | 255.338 ms | 250.808 ms | 251.105 ms |  3.147 ms          |
| Day 20 - Trench Map              |  22.665 ms |  23.194 ms |  22.926 ms |  22.922 ms |  0.157 ms          |
| Day 21 - Dirac Dice              |   2.971 ms |   3.059 ms |   3.028 ms |   3.032 ms |  0.027 ms          |
| Day 22 - Reactor Reboot          |  28.976 ms |  33.655 ms |  30.885 ms |  30.593 ms |  1.203 ms          |
| Day 23 - Amphipod                | 284.823 ms | 302.756 ms | 295.350 ms | 296.108 ms |  5.225 ms          |
| Day 24 - Arithmetic Logic Unit   | 127.090 ms | 132.216 ms | 129.253 ms | 128.711 ms |  1.598 ms          |
| Day 25 - Sea Cucumber            |  56.955 ms |  60.222 ms |  58.291 ms |  58.280 ms |  0.848 ms          |
| Total                            | 879.669 ms | 926.774 ms | 905.041 ms | 905.374 ms | 13.584 ms          |

## License

This project is licensed under the [MIT License](LICENSE). Feel free to experiment with the code,
adapt it to your own preferences, and share it with others.
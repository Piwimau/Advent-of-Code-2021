﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Lanternfish.Source;

internal sealed class Program {

    /// <summary>Initial timer of existing lanternfish.</summary>
    private const int InitialLanternfishTimer = 6;

    /// <summary>Initial timer of newborn lanternfish.</summary>
    private const int NewbornLanternfishTimer = 8;

    private static readonly string InputFile = Path.Combine(
        AppContext.BaseDirectory,
        "Resources",
        "input.txt"
    );

    /// <summary>Counts the number of lanternfish existing after a given number of days.</summary>
    /// <param name="initialLanternfish">Sequence of the initial lanternfish timers.</param>
    /// <param name="days">Number of days to simulate.</param>
    /// <returns>The number of lanternfish existing after the given number of days.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="days"/> is negative.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long CountLanternfish(ReadOnlySpan<int> initialLanternfish, int days) {
        ArgumentOutOfRangeException.ThrowIfNegative(days, nameof(days));
        // A stack-allocated span for counting the timers is way faster than a list or dictionary.
        Span<long> countByTimer = stackalloc long[NewbornLanternfishTimer + 1];
        foreach (int timer in initialLanternfish) {
            countByTimer[timer]++;
        }
        for (int i = 0; i < days; i++) {
            long resetLanternfish = countByTimer[0];
            // Copying all values over by one to the left effectively decrements the timer.
            countByTimer[1..].CopyTo(countByTimer);
            countByTimer[InitialLanternfishTimer] += resetLanternfish;
            countByTimer[NewbornLanternfishTimer] = resetLanternfish;
        }
        long lanternfish = 0;
        foreach (long count in countByTimer) {
            lanternfish += count;
        }
        return lanternfish;
    }

    private static void Main() {
        ReadOnlySpan<int> initialLanternfish = [.. File.ReadAllText(InputFile).Split(',')
            .Select(int.Parse)
        ];
        long count80 = CountLanternfish(initialLanternfish, 80);
        long count256 = CountLanternfish(initialLanternfish, 256);
        Console.WriteLine($"After 80 days, there would be {count80} lanternfish.");
        Console.WriteLine($"After 256 days, there would be {count256} lanternfish.");
    }

}
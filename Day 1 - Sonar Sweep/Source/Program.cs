using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SonarSweep.Source;

internal sealed class Program {

    private static readonly string InputFile = Path.Combine(
        AppContext.BaseDirectory,
        "Resources",
        "input.txt"
    );

    /// <summary>
    /// Counts the number of depth increases in a given sequence of depths using a sliding window of
    /// a given size.
    /// </summary>
    /// <param name="depths">Sequence of depths for the calculation.</param>
    /// <param name="slidingWindowSize">Positive size of the sliding window used.</param>
    /// <returns>
    /// The number of depth increases in the given sequence of depths using a sliding window of the
    /// given size.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="slidingWindowSize"/> is negative.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDepthIncreases(ReadOnlySpan<int> depths, int slidingWindowSize) {
        ArgumentOutOfRangeException.ThrowIfNegative(slidingWindowSize, nameof(slidingWindowSize));
        int depthIncreases = 0;
        for (int i = slidingWindowSize; i < depths.Length; i++) {
            // Two sliding windows of size N share N - 1 items. We therefore only need to compare
            // the two remaining items at the edges to find out if the depth increased.
            //
            // -----------------------------------------
            // | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | ...
            // -----------------------------------------
            //     └------------ N ------------┘
            //         └------------ N ------------┘
            //         └- Shared N - 1 Items --┘ 
            //       ^                           ^
            //       └----- Remaining Items -----┘
            if (depths[i] > depths[i - slidingWindowSize]) {
                depthIncreases++;
            }
        }
        return depthIncreases;
    }

    private static void Main() {
        ReadOnlySpan<int> depths = [.. File.ReadLines(InputFile).Select(int.Parse)];
        int countOne = CountDepthIncreases(depths, 1);
        int countThree = CountDepthIncreases(depths, 3);
        Console.WriteLine($"{countOne} measurements are larger than the previous measurement.");
        Console.WriteLine(
            $"{countThree} measurements are larger than the previous three measurements."
        );
    }

}
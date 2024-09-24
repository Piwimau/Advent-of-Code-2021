using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HydrothermalVenture.Source;

internal sealed partial class Program {

    /// <summary>Represents a two-dimensional <see cref="Position"/>.</summary>
    /// <param name="X">X-coordinate of the <see cref="Position"/>.</param>
    /// <param name="Y">Y-coordinate of the <see cref="Position"/>.</param>
    private readonly record struct Position(int X, int Y);

    /// <summary>
    /// Represents a <see cref="Line"/> with a start and end <see cref="Position"/>.
    /// </summary>
    /// <param name="Start">Start <see cref="Position"/> of the <see cref="Line"/>.</param>
    /// <param name="End">End <see cref="Position"/> of the <see cref="Line"/>.</param>
    private readonly partial record struct Line(Position Start, Position End) {

        /// <summary>Determines whether this <see cref="Line"/> is diagonal (45° angle).</summary>
        public bool IsDiagonal => Math.Abs(Start.X - End.X) == Math.Abs(Start.Y - End.Y);

        [GeneratedRegex("^\\d+,\\d+ -> \\d+,\\d+$")]
        private static partial Regex LineRegex();

        /// <summary>Parses a <see cref="Line"/> from a given string.</summary>
        /// <remarks>
        /// The string <paramref name="s"/> must contain two positions separated by " -> ",
        /// which in turn each consist of two positive integers (separated by a comma).<br/>
        /// An example for a valid line might be "0,9 -> 2,9".
        /// </remarks>
        /// <param name="s">String to parse a <see cref="Line"/> from.</param>
        /// <returns>A <see cref="Line"/> parsed from the given string.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="s"/> has an invalid format.
        /// </exception>
        public static Line Parse(string s) {
            ArgumentNullException.ThrowIfNull(s, nameof(s));
            if (!LineRegex().IsMatch(s)) {
                throw new ArgumentOutOfRangeException(
                    nameof(s),
                    $"The string \"{s}\" does not represent a valid line."
                );
            }
            ReadOnlySpan<char> span = s;
            int separatorIndex = span.IndexOf(" -> ");
            ReadOnlySpan<char> startSpan = span[..separatorIndex];
            int commaIndex = startSpan.IndexOf(',');
            Position start = new() {
                X = int.Parse(startSpan[..commaIndex]),
                Y = int.Parse(startSpan[(commaIndex + 1)..])
            };
            ReadOnlySpan<char> endSpan = span[(separatorIndex + " -> ".Length)..];
            commaIndex = endSpan.IndexOf(',');
            Position end = new() {
                X = int.Parse(endSpan[..commaIndex]),
                Y = int.Parse(endSpan[(commaIndex + 1)..])
            };
            return new Line(start, end);
        }

        /// <summary>Returns all positions covered by this <see cref="Line"/>.</summary>
        /// <returns>All positions covered by this <see cref="Line"/>.</returns>
        public IEnumerable<Position> CoveredPositions() {
            int xOffset = Math.Sign(End.X - Start.X);
            int yOffset = Math.Sign(End.Y - Start.Y);
            Position current = Start;
            do {
                yield return current;
                current = new Position(current.X + xOffset, current.Y + yOffset);
            }
            while (current != End);
            // End of the line counts as covered, just like the start position.
            yield return current;
        }

    }

    private static readonly string InputFile = Path.Combine(
        AppContext.BaseDirectory,
        "Resources",
        "input.txt"
    );

    /// <summary>Counts the number of overlaps in a given sequence of lines.</summary>
    /// <remarks>
    /// An overlap occurs if a <see cref="Position"/> is covered by at least two lines.
    /// </remarks>
    /// <param name="lines">Sequence of lines for counting the number of overlaps.</param>
    /// <returns>The number of overlaps in the given sequence of lines.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="lines"/> is <see langword="null"/>.
    /// </exception>
    private static int CountOverlaps(IEnumerable<Line> lines) {
        ArgumentNullException.ThrowIfNull(lines, nameof(lines));
        HashSet<Position> coveredPositions = [];
        HashSet<Position> overlappingPositions = [];
        foreach (Line line in lines) {
            foreach (Position position in line.CoveredPositions()) {
                if (!coveredPositions.Add(position)) {
                    overlappingPositions.Add(position);
                }
            }
        }
        return overlappingPositions.Count;
    }

    private static void Main() {
        IReadOnlyList<Line> lines = [.. File.ReadLines(InputFile).Select(Line.Parse)];
        int nonDiagonalOverlaps = CountOverlaps(lines.Where(line => !line.IsDiagonal));
        int totalOverlaps = CountOverlaps(lines);
        Console.WriteLine($"There are {nonDiagonalOverlaps} overlaps in non-diagonal lines.");
        Console.WriteLine($"There are {totalOverlaps} overlaps in all lines.");
    }

}
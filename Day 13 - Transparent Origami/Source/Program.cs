﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TransparentOrigami.Source;

internal sealed partial class Program {

    /// <summary>Represents an enumeration of all possible directions.</summary>
    private enum Direction { Left, Up }

    /// <summary>Represents a two-dimensional <see cref="Position"/>.</summary>
    /// <param name="X">X-coordinate of the <see cref="Position"/>.</param>
    /// <param name="Y">Y-coordinate of the <see cref="Position"/>.</param>
    private readonly partial record struct Position(int X, int Y) {

        [GeneratedRegex("^\\d+,\\d+$")]
        private static partial Regex PositionRegex();

        /// <summary>Parses a <see cref="Position"/> from a given string.</summary>
        /// <remarks>
        /// The string <paramref name="s"/> must contain two positive, comma-separated integers
        /// as in "6,42".
        /// </remarks>
        /// <param name="s">String to parse a <see cref="Position"/> from.</param>
        /// <returns>A <see cref="Position"/> parsed from the given string.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="s"/> has an invalid format.
        /// </exception>
        public static Position Parse(string s) {
            ArgumentNullException.ThrowIfNull(s, nameof(s));
            ReadOnlySpan<char> span = s;
            if (!PositionRegex().IsMatch(span)) {
                throw new ArgumentOutOfRangeException(
                    nameof(s),
                    $"The string \"{s}\" does not represent a valid position."
                );
            }
            int commaIndex = span.IndexOf(',');
            int x = int.Parse(span[..commaIndex]);
            int y = int.Parse(span[(commaIndex + 1)..]);
            return new Position(x, y);
        }

    }

    /// <summary>Represents an <see cref="Instruction"/> for folding an origami.</summary>
    /// <param name="Direction">
    /// <see cref="Program.Direction"/> of the <see cref="Instruction"/> to fold the origami to.
    /// </param>
    /// <param name="Coordinate">
    /// Coordinate of the <see cref="Instruction"/> to fold the origami at.
    /// </param>
    private readonly partial record struct Instruction(Direction Direction, int Coordinate) {

        /// <summary>Index of the character indicating the folding direction.</summary>
        private const int DirectionIndex = 11;

        /// <summary>Index at which the folding coordinate starts.</summary>
        private const int CoordinateStartIndex = 13;

        [GeneratedRegex("^fold along [xy]=\\d+$")]
        private static partial Regex InstructionRegex();

        /// <summary>Parses an <see cref="Instruction"/> from a given string.</summary>
        /// <remarks>
        /// The string <paramref name="s"/> must have the format described by
        /// <see cref="InstructionRegex"/>. An example might be the following:
        /// <example>
        /// <code>
        /// "fold along y = 42"
        /// </code>
        /// </example>
        /// </remarks>
        /// <param name="s">String to parse an <see cref="Instruction"/> from.</param>
        /// <returns>An <see cref="Instruction"/> parsed from the given string.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="s"/> has an invalid format.
        /// </exception>
        public static Instruction Parse(string s) {
            ArgumentNullException.ThrowIfNull(s, nameof(s));
            ReadOnlySpan<char> span = s;
            if (!InstructionRegex().IsMatch(span)) {
                throw new ArgumentOutOfRangeException(
                    nameof(s),
                    $"The string \"{s}\" does not represent a valid instruction."
                );
            }
            Direction direction = (span[DirectionIndex] == 'x') ? Direction.Left : Direction.Up;
            int coordinate = int.Parse(span[CoordinateStartIndex..]);
            return new Instruction(direction, coordinate);
        }

    }

    private static readonly string InputFile = Path.Combine(
        AppContext.BaseDirectory,
        "Resources",
        "input.txt"
    );

    /// <summary>
    /// Finds all distinct visible dots by executing folding instructions on a given sequence of
    /// positions.
    /// </summary>
    /// <param name="positions">Sequence of the initial dot positions.</param>
    /// <param name="instructions">Sequence of folding instructions to execute.</param>
    /// <returns>All distinct visible dots after executing the given folding instructions.</returns>
    private static HashSet<Position> VisibleDots(
        ReadOnlySpan<Position> positions,
        ReadOnlySpan<Instruction> instructions
    ) {
        // Semantically, it would only be necessary to keep all distinct visible dots, for example
        // by using a HashSet<Position> or the Enumerable<T>.Distinct<T>() method to remove any
        // duplicates. However, since we are only working with a relatively small number of dots
        // anyway, a Span<Position> (probably backed by a List<Position> or Position[] in the end)
        // actually outperforms both alternatives (about twice as fast). It not only avoids the cost
        // of calculating hash codes and comparing values, but also accesses memory in a linear,
        // predictable fashion (which is great for cache locality). We therefore only pay the cost
        // of determining all distinct dots once by creating a HashSet<Position> at the very end.
        Span<Position> visibleDots = [.. positions];
        foreach (Instruction instruction in instructions) {
            foreach (ref Position position in visibleDots) {
                if (instruction.Direction == Direction.Left) {
                    if (position.X > instruction.Coordinate) {
                        position = position with {
                            X = position.X - (2 * (position.X - instruction.Coordinate))
                        };
                    }
                }
                else if (position.Y > instruction.Coordinate) {
                    position = position with {
                        Y = position.Y - (2 * (position.Y - instruction.Coordinate))
                    };
                }
            }
        }
        return [.. visibleDots];
    }

    /// <summary>
    /// Prints the final eight letter activation code for the thermal imaging camera system.
    /// </summary>
    /// <param name="visibleDots">
    /// All distinct visible dots after executing the folding instructions.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="visibleDots"/> is <see langword="null"/>.
    /// </exception>
    private static void PrintActivationCode(IReadOnlySet<Position> visibleDots) {
        ArgumentNullException.ThrowIfNull(visibleDots, nameof(visibleDots));
        int maxY = 0;
        foreach (Position visibleDot in visibleDots) {
            // Offset to prevent overwriting any previous output.
            Console.SetCursorPosition(visibleDot.X, visibleDot.Y + 3);
            Console.Write("#");
            maxY = Math.Max(maxY, visibleDot.Y);
        }
        // Offset to prevent the activation code from being overwritten by the termination message
        // when running the application from within Visual Studio.
        Console.SetCursorPosition(0, maxY + 4);
    }

    private static void Main() {
        ReadOnlySpan<string> parts = File.ReadAllText(InputFile)
            .Split(Environment.NewLine + Environment.NewLine);
        ReadOnlySpan<Position> positions = [.. parts[0]
            .Split(Environment.NewLine)
            .Select(Position.Parse)
        ];
        ReadOnlySpan<Instruction> instructions = [.. parts[1]
            .Split(Environment.NewLine)
            .Select(Instruction.Parse)
        ];
        int dots = VisibleDots(positions, instructions[..1]).Count;
        Console.WriteLine($"{dots} dots are visible after executing just the first instruction.");
        Console.WriteLine($"The final eight letter activation code is:{Environment.NewLine}");
        PrintActivationCode(VisibleDots(positions, instructions));
    }

}
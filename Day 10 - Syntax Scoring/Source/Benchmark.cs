﻿using System.Diagnostics.CodeAnalysis;
using System.IO;
using BenchmarkDotNet.Attributes;

namespace SyntaxScoring.Source;

/// <summary>
/// Represents a <see cref="Benchmark"/> for the <see cref="SyntaxScoring"/> puzzle.
/// </summary>
[MemoryDiagnoser]
public class Benchmark {

    /// <summary>Runs a benchmark for the <see cref="SyntaxScoring"/> puzzle.</summary>
    [Benchmark]
    [SuppressMessage(
        "Performance",
        "CA1822:Mark members as static",
        Justification = "Benchmarking static methods is not supported."
    )]
    public void Run() => SyntaxScoring.Solve(TextWriter.Null);

}
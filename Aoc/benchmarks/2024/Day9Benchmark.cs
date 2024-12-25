using AoC.benchmark;
using AoC._2024;
using BenchmarkDotNet.Attributes;

namespace Benchmarks._2024;

public class Day9Benchmark : BenchmarkAttributes
{
    [Benchmark(Baseline = true)]
    public int regular()
    {
        new Day09().Run();
        return -1;
    }
}
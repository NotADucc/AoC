using AoC;
using AoC.benchmark;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.days
{
    public class Day9Benchmark : BenchmarkAttributes
    {
        [Benchmark(Baseline = true)]
        public int regular()
        {
            new Day9().Run();
            return -1;
        }
    }
}
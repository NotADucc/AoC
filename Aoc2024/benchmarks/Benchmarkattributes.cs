using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AoC.benchmark;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[Config(typeof(Config))]
public class BenchmarkAttributes { }
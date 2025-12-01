using AoC;

namespace AoCTesting._2025;

public class Day01Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(1102L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(6175L, res.res_2);
    }
}
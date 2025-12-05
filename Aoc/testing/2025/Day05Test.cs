using AoC;

namespace AoCTesting._2025;

public class Day05Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(613L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(336495597913098L, res.res_2);
    }
}
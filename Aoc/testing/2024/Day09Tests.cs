using AoC;

namespace AoCTesting._2024;

public class Day09Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(6386640365805L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(6423258376982L, res.res_2);
    }
}
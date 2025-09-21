using AoC;

namespace AoCTesting._2016;

public class Day06Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal("tsreykjj", res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("hnfbujie", res.res_2);
    }
}
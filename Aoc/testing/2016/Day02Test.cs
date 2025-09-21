using AoC;

namespace AoCTesting._2016;

public class Day02Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(56855L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("B3C27", res.res_2);
    }
}
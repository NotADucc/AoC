using AoC;

namespace AoCTesting._2016;

public class Day05Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal("f77a0e6e", res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("999828ec", res.res_2);
    }
}
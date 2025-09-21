using AoC;

namespace AoCTesting._2016;

public class Day03Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(917L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1649L, res.res_2);
    }
}
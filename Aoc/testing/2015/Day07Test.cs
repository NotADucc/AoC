using AoC;

namespace AoCTesting._2015;

public class Day07Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(46065L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(14134L, res.res_2);
    }
}
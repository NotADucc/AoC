using AoC;

namespace AoCTesting._2015;

public class Day23Test
{
    private (object? res_1, object? res_2) res;
    public Day23Test() 
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(184L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(231L, res.res_2);
    }
}
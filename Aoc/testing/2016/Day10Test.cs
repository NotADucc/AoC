using AoC;

namespace AoCTesting._2016;

public class Day10Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(101L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(37789L, res.res_2);
    }
}
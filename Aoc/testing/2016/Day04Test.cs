using AoC;

namespace AoCTesting._2016;

public class Day04Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(137896L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(501L, res.res_2);
    }
}
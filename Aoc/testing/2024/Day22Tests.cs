using AoC;

namespace AoCTesting._2024;

public class Day22Test
{
    private (object? res_1, object? res_2) res;
    public Day22Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(20215960478L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(2221L, res.res_2);
    }
}
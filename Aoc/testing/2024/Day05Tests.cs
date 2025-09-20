using AoC;

namespace AoCTesting._2024;

public class Day05Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(5166L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(4679L, res.res_2);
    }
}
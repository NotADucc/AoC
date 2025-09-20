using AoC;

namespace AoCTesting._2024;

public class Day08Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(285L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(944L, res.res_2);
    }
}
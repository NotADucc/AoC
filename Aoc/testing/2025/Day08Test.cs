using AoC;

namespace AoCTesting._2025;

public class Day08Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(42315L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(8079278220L, res.res_2);
    }
}
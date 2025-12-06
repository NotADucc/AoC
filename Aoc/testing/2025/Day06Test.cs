using AoC;

namespace AoCTesting._2025;

public class Day06Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(6209956042374L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(12608160008022L, res.res_2);
    }
}
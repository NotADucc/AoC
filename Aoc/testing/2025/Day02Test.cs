using AoC;

namespace AoCTesting._2025;

public class Day02Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(64215794229L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(85513235135L, res.res_2);
    }
}
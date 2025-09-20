using AoC;

namespace AoCTesting._2015;

public class Day02Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(1588178L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(3783758L, res.res_2);
    }
}
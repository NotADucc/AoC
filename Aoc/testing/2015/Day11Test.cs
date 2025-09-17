using AoC;
using AoC._2015;

namespace AoCTesting._2015;

public class Day11Test
{
    private (object res_1, object res_2) res;
    public Day11Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal("hxbxxyzz", res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("hxcaabcc", res.res_2);
    }
}
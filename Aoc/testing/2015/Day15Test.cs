using AoC;
using AoC._2015;

namespace AoCTesting._2015;

public class Day15Test
{
    private (object res_1, object res_2) res;
    public Day15Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(21367368L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1766400L, res.res_2);
    }
}
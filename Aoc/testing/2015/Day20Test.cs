using AoC;
using AoC._2015;

namespace AoCTesting._2015;

public class Day20Test
{
    private (object res_1, object res_2) res;
    public Day20Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(776160L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(786240L, res.res_2);
    }
}
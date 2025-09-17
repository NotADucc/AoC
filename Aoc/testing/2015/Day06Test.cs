using AoC;
using AoC._2015;

namespace AoCTesting._2015;

public class Day06Test
{
    private (object res_1, object res_2) res;
    public Day06Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(400410L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(15343601L, res.res_2);
    }
}
using AoC;

namespace AoCTesting._2015;

public class Day14Test
{
    private (object res_1, object res_2) res;
    public Day14Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(2640L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1102L, res.res_2);
    }
}
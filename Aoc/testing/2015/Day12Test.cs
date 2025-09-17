using AoC;

namespace AoCTesting._2015;

public class Day12Test
{
    private (object res_1, object res_2) res;
    public Day12Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(191164L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(87842L, res.res_2);
    }
}
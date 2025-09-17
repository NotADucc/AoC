using AoC;
using AoC._2024;

namespace AoCTesting._2024;

public class Day19Test
{
    private (object res_1, object res_2) res;
    public Day19Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(342L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(891192814474630L, res.res_2);
    }
}
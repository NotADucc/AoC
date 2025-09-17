using AoC;
using AoC._2024;

namespace AoCTesting._2024;

public class Day07Test
{
    private (object res_1, object res_2) res;
    public Day07Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(1153997401072L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(97902809384118L, res.res_2);
    }
}
using AoC;
using AoC._2024;

namespace AoCTesting._2024;

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
        Assert.Equal(220971520L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(6355L, res.res_2);
    }
}
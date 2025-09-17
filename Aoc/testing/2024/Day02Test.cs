using AoC;
using AoC._2024;

namespace AoCTesting._2024;

public class Day02Test
{
    private (object res_1, object res_2) res;
    public Day02Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(564L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(604L, res.res_2);
    }
}
using AoC;

namespace AoCTesting._2015;

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
        Assert.Equal(535L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(212L, res.res_2);
    }
}
using AoC;
using AoC._2015;

namespace AoCTesting._2015;

public class Day21Test
{
    private (object? res_1, object? res_2) res;
    public Day21Test() 
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(0L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(0L, res.res_2);
    }
}
using AoC;

namespace AoCTesting._2024;

public class Day23Test
{
    private (object? res_1, object? res_2) res;
    public Day23Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(1400L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("am,bc,cz,dc,gy,hk,li,qf,th,tj,wf,xk,xo", res.res_2);
    }
}
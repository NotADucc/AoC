using AoC.days;

namespace AoCTesting;

public class Day23Test
{
    private Day23 proj = new();
    private (long res_1, string res_2) res;
    public Day23Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(1400, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("am,bc,cz,dc,gy,hk,li,qf,th,tj,wf,xk,xo", res.res_2);
    }
}
using AoC.days;

namespace AoCTesting;

public class Day24Test
{
    private Day24 proj = new();
    private (long res_1, string res_2) res;
    public Day24Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(48063513640678, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("hqh,mmk,pvb,qdq,vkq,z11,z24,z38", res.res_2);
    }
}
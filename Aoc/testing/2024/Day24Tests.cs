using AoC;

namespace AoCTesting._2024;

public class Day24Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(48063513640678L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("hqh,mmk,pvb,qdq,vkq,z11,z24,z38", res.res_2);
    }
}
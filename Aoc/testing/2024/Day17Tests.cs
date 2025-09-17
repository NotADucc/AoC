using AoC;
using AoC._2024;

namespace AoCTesting._2024;

public class Day17Test
{
    private (object res_1, object res_2) res;
    public Day17Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal("2,1,4,7,6,0,3,1,4", res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(266932601404433L, res.res_2);
    }
}
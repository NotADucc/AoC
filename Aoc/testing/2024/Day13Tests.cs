using AoC;
using AoC._2024;

namespace AoCTesting._2024;

public class Day13Test
{
    private (object res_1, object res_2) res;
    public Day13Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(29522L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(101214869433312L, res.res_2);
    }
}
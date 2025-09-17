using AoC;

namespace AoCTesting._2015;

public class Day05Test
{
    private (object res_1, object res_2) res;
    public Day05Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(236L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(51L, res.res_2);
    }
}
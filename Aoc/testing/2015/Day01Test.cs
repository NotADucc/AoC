using AoC;

namespace AoCTesting._2015;

public class Day01Test
{
    private (object res_1, object res_2) res;
    public Day01Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(138L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1771L, res.res_2);
    }
}
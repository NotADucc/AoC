using AoC;

namespace AoCTesting._2015;

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
        Assert.Equal(1638L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(17L, res.res_2);
    }
}
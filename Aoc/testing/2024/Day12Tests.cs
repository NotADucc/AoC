using AoC;

namespace AoCTesting._2024;

public class Day12Test
{
    private (object res_1, object res_2) res;
    public Day12Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(1533644L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(936718L, res.res_2);
    }
}
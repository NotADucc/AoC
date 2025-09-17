using AoC;

namespace AoCTesting._2024;

public class Day15Test
{
    private (object res_1, object res_2) res;
    public Day15Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(1577255L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1597035L, res.res_2);
    }
}
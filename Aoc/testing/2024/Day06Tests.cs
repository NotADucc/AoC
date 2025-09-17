using AoC;

namespace AoCTesting._2024;

public class Day06Test
{
    private (object res_1, object res_2) res;
    public Day06Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(5564L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1976L, res.res_2);
    }
}
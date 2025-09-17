using AoC;

namespace AoCTesting._2015;

public class Day03Test
{
    private (object res_1, object res_2) res;
    public Day03Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(2565L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(2639L, res.res_2);
    }
}
using AoC;

namespace AoCTesting._2015;

public class Day04Test
{
    private (object res_1, object res_2) res;
    public Day04Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(117946L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(3938038L, res.res_2);
    }
}
using AoC;

namespace AoCTesting._2024;

public class Day16Test
{
    private (object res_1, object res_2) res;
    public Day16Test()
    {
        res = Helper.RunAocDayBasedOnCallerPath();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(91464L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(494L, res.res_2);
    }
}
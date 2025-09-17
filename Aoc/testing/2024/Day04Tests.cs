using AoC;

namespace AoCTesting._2024;

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
        Assert.Equal(2414L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1871L, res.res_2);
    }
}
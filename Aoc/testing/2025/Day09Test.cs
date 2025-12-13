using AoC;

namespace AoCTesting._2025;

public class Day09Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(4763040296L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1396494456L, res.res_2);
    }
}
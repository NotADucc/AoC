using AoC;

namespace AoCTesting._2025;

public class Day03Test
{
    private (object? res_1, object? res_2) res = Helper.RunAocDayBasedOnCallerPath();

    [Fact]
    public void Part1()
    {
        Assert.Equal(17158L, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(170449335646486L, res.res_2);
    }
}
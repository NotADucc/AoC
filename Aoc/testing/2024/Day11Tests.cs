using AoC._2024;

namespace AoCTesting._2024;

public class Day11Test
{
    private Day11 proj = new();
    private (long res_1, long res_2) res;
    public Day11Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(187738, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(223767210249237, res.res_2);
    }
}
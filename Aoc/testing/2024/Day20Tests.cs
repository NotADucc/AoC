using AoC._2024;

namespace AoCTesting._2024;

public class Day20Test
{
    private Day20 proj = new();
    private (long res_1, long res_2) res;
    public Day20Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(1511, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1020507, res.res_2);
    }
}
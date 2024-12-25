using AoC._2024;

namespace AoCTesting._2024;

public class Day01Test
{
    private Day01 proj = new();
    private (long res_1, long res_2) res;
    public Day01Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(2113135, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(19097157, res.res_2);
    }
}
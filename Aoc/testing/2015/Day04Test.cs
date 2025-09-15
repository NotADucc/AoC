using AoC._2024;

namespace AoCTesting._2015;

public class Day04Test
{
    private Day01 proj = new();
    private (long res_1, long res_2) res;
    public Day04Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(117946, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(3938038, res.res_2);
    }
}
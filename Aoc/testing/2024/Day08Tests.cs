﻿using AoC._2024;

namespace AoCTesting._2024;

public class Day08Test
{
    private Day08 proj = new();
    private (long res_1, long res_2) res;
    public Day08Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(285, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(944, res.res_2);
    }
}
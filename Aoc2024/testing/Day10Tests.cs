﻿namespace AoCTesting
{
    public class Day10Test
    {
        private Day10 proj = new();
        private (long res_1, long res_2) res;
        public Day10Test()
        {
            res = proj.Run();
        }

        [Fact]
        public void Part1()
        {
            Assert.Equal(0, res.res_1);
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(0, res.res_2);
        }
    }
}
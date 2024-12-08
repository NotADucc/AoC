namespace AoCTesting
{
    public class Day8Test
    {
        private Day8 proj = new();
        private (long res_1, long res_2) res;
        public Day8Test()
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
}
namespace AoCTesting
{
    public class Day15Test
    {
        private Day15 proj = new();
        private (long res_1, long res_2) res;
        public Day15Test()
        {
            res = proj.Run();
        }

        [Fact]
        public void Part1()
        {
            Assert.Equal(1577255, res.res_1);
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(0, res.res_2);
        }
    }
}
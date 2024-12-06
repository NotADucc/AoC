namespace AoCTesting
{
    public class Day3Test
    {
        private Day3 proj = new();
        private (long res_1, long res_2) res;
        public Day3Test()
        {
            res = proj.Run();
        }

        [Fact]
        public void Part1()
        {
            Assert.Equal(171183089, res.res_1);
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(63866497, res.res_2);
        }
    }
}
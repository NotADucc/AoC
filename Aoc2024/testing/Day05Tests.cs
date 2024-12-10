namespace AoCTesting
{
    public class Day05Test
    {
        private Day05 proj = new();
        private (long res_1, long res_2) res;
        public Day05Test()
        {
            res = proj.Run();
        }

        [Fact]
        public void Part1()
        {
            Assert.Equal(5166, res.res_1);
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(4679, res.res_2);
        }
    }
}
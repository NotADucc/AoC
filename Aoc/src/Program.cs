using AoC;
using System.Diagnostics;

public class Program
{
    private static void Main(string[] args)
    {
        try
        {
            const int YEAR = 2025;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var (day, result1, result2) = Helper.RunYear(YEAR);
            sw.Stop();

            Console.WriteLine($"{YEAR}: {day}");
            Console.WriteLine($"Res 1 : {result1}");
            Console.WriteLine($"Res 2 : {result2}");
            Console.WriteLine($"Elapsed time : {sw.Elapsed}");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
        finally
        {
            Console.ReadLine();
        }
    }
}

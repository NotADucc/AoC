namespace AoC;

public interface IRun<T1, T2>
{
    (T1, T2) Run();
}

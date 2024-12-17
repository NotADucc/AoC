namespace AoC;

public interface IRun<T>
{
    (T, T) Run();
}

namespace AoC;

public interface IRun
{
    (object?, object?) RunUntyped();
}

public interface IRun<T1, T2> : IRun
{
    (T1, T2) Run();

    (object?, object?) IRun.RunUntyped() => Run();
}

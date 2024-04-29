using System.Numerics;

namespace ParallelSimd;

public interface IOperator
{
    static virtual bool IsVectorizable => true;
}

public interface IBinaryOperator<T1, T2, TResult>
    : IOperator
    where T1 : struct
    where T2 : struct
    where TResult : struct
{
    static abstract TResult Invoke(T1 x, T2 y);

    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y);
}

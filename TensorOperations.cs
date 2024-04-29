using System.Numerics;

namespace ParallelSimd;

public static class TensorOperations
{
    public static void Add<T>(T[] left, T[] right, T[] destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, TensorOperators.AddOperator<T>>(left, right.AsMemory(), destination.AsMemory());

    public static void Add<T>(ReadOnlyMemory<T> left, ReadOnlyMemory<T> right, Memory<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, TensorOperators.AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, TensorOperators.AddOperator<T>>(left, right, destination);
}

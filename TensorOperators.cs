using System.Numerics;
using System.Runtime.CompilerServices;

namespace ParallelSimd;

public static class TensorOperators
{
    public readonly struct AddOperator<T>
        : IBinaryOperator<T, T, T>
        where T : struct, IAdditionOperators<T, T, T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Invoke(T x, T y)
            => x + y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
            => x + y;
    }
}

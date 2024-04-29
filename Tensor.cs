using NetFabric;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParallelSimd;

public static class Tensor
{
    const int minChunkSize = 1_000;
    const int minChunkCount = 4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int AvailableCores()
        => Environment.ProcessorCount;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool OverlapAndAreNotSame<T>(ReadOnlyMemory<T> span, ReadOnlyMemory<T> other)
        => OverlapAndAreNotSame(span.Span, other.Span);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool OverlapAndAreNotSame<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other)
        => !Unsafe.AreSame(ref MemoryMarshal.GetReference(span), ref MemoryMarshal.GetReference(other)) && MemoryExtensions.Overlaps(span, other);

    public static void Apply<T, TOperator>(ReadOnlyMemory<T> x, ReadOnlyMemory<T> y, Memory<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T, T>
    {
        if (OverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, TOperator>(x, y, destination);
    }

    public static void Apply<T1, T2, TResult, TOperator>(ReadOnlyMemory<T1> x, ReadOnlyMemory<T2> y, Memory<TResult> destination)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<T1, T2, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        var coreCount = AvailableCores();

        if (coreCount >= minChunkCount && x.Length > minChunkCount * minChunkSize)
            ParallelApply(x, y, destination, coreCount);
        else
            Apply<T1, T2, TResult, TOperator>(x.Span, y.Span, destination.Span);

        static void ParallelApply(ReadOnlyMemory<T1> x, ReadOnlyMemory<T2> y, Memory<TResult> destination, int coreCount)
        {
            var totalSize = x.Length;
            var chunkSize = int.Max(totalSize / coreCount, minChunkSize);

            var actions = new Action[totalSize / chunkSize];
            var start = 0;
            for (var index = 0; index < actions.Length; index++)
            {
                var length = (index == actions.Length - 1)
                    ? totalSize - start
                    : chunkSize;

                var xSlice = x.Slice(start, length);
                var ySlice = y.Slice(start, length);
                var destinationSlice = destination.Slice(start, length);
                actions[index] = () => Apply<T1, T2, TResult, TOperator>(xSlice.Span, ySlice.Span, destinationSlice.Span);

                start += length;
            }
            Parallel.Invoke(actions);
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T, T>
    {
        if (OverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (OverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, T, TOperator>(x, y, destination);
    }

    public static void Apply<T1, T2, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<T1, T2, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the length of Vector<T>.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length > Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = 0;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref Unsafe.Add(ref yVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource < x.Length - 3; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1));
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2));
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3));
        }

        switch (x.Length - indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1));
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2));
                break;
            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1));
                break;
            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }
}

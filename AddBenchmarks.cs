using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.ComponentModel;
using System.Numerics;
using System.Numerics.Tensors;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParallelSimd;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class AddBenchmarks
{
    float[]? sourceSingle, otherSingle, resultSingle;
    int[]? sourceInt32, otherInt32, resultInt32;

    [Params(111_111)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        sourceSingle = new float[Count];
        otherSingle = new float[Count];
        resultSingle = new float[Count];

        sourceInt32 = new int[Count];
        otherInt32 = new int[Count];
        resultInt32 = new int[Count];

        var random = new Random(42);
        for (var index = 0; index < Count; index++)
        {
            var sourceValue = random.Next(100) - 50;
            var otherValue = random.Next(100) - 50;

            sourceSingle[index] = sourceValue;
            otherSingle[index] = otherValue;

            sourceInt32[index] = sourceValue;
            otherInt32[index] = otherValue;
        }
    }

    [BenchmarkCategory("Single")]
    [Benchmark(Baseline = true)]
    public void Single_For()
        => For<float>(sourceSingle!, otherSingle!, resultSingle!);

    [BenchmarkCategory("Single")]
    [Benchmark]
    public void Single_For_GetReference()
        => For_GetReference<float>(sourceSingle!, otherSingle!, resultSingle!);

    [BenchmarkCategory("Single")]
    [Benchmark]
    public void Single_For_GetReference4()
        => For_GetReference_4<float>(0, sourceSingle!, otherSingle!, resultSingle!);

    [BenchmarkCategory("Single")]
    [Benchmark]
    public void Single_For_SIMD()
        => For_SIMD<float>(sourceSingle!, otherSingle!, resultSingle!);

    [BenchmarkCategory("Single")]
    [Benchmark]
    public void Single_System_Numerics_Tensor()
        => TensorPrimitives.Add<float>(sourceSingle!, otherSingle!, resultSingle!);

    [BenchmarkCategory("Single")]
    [Benchmark]
    public void Single_Parallel_For()
        => Parallel_For(sourceSingle!, otherSingle!, resultSingle!);

    [BenchmarkCategory("Single")]
    [Benchmark]
    public void Single_Parallel_Invoke()
        => Parallel_Invoke(sourceSingle!.AsMemory(), otherSingle!.AsMemory(), resultSingle!.AsMemory());

    [BenchmarkCategory("Single")]
    [Benchmark]
    public void Single_Parallel_Invoke_SIMD()
        => Parallel_Invoke_SIMD(sourceSingle!.AsMemory(), otherSingle!.AsMemory(), resultSingle!.AsMemory());

    [BenchmarkCategory("Int32")]
    [Benchmark(Baseline = true)]
    public void Int32_For()
        => For<int>(sourceInt32!, otherInt32!, resultInt32!);

    [BenchmarkCategory("Int32")]
    [Benchmark]
    public void Int32_For_GetReference()
        => For_GetReference<int>(sourceInt32!, otherInt32!, resultInt32!);

    [BenchmarkCategory("Int32")]
    [Benchmark]
    public void Int32_For_GetReference4()
        => For_GetReference_4<int>(0, sourceInt32!, otherInt32!, resultInt32!);

    [BenchmarkCategory("Int32")]
    [Benchmark]
    public void Int32_For_SIMD()
        => For_SIMD<int>(sourceInt32!, otherInt32!, resultInt32!);

    [BenchmarkCategory("Int32")]
    [Benchmark]
    public void Int32_System_Numerics_Tensor()
        => TensorPrimitives.Add<int>(sourceInt32!, otherInt32!, resultInt32!);

    [BenchmarkCategory("Int32")]
    [Benchmark]
    public void Int32_Parallel_For()
        => Parallel_For(sourceInt32!, otherInt32!, resultInt32!);

    [BenchmarkCategory("Int32")]
    [Benchmark]
    public void Int32_Parallel_Invoke()
        => Parallel_Invoke(sourceInt32!.AsMemory(), otherInt32!.AsMemory(), resultInt32!.AsMemory());

    [BenchmarkCategory("Int32")]
    [Benchmark]
    public void Int32_Parallel_Invoke_SIMD()
        => Parallel_Invoke_SIMD(sourceInt32!.AsMemory(), otherInt32!.AsMemory(), resultInt32!.AsMemory());

    static void For<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        for (var index = 0; index < left.Length; index++)
            destination[index] = left[index] + right[index];
    }

    static void For_GetReference<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        ref var leftRef = ref MemoryMarshal.GetReference(left);
        ref var rightRef = ref MemoryMarshal.GetReference(right);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        var end = left.Length;
        for (var index = 0; index < end; index++)
            Unsafe.Add(ref destinationRef, index) = Unsafe.Add(ref leftRef, index) + Unsafe.Add(ref rightRef, index);
    }

    static void For_GetReference_4<T>(int index, ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        ref var leftRef = ref MemoryMarshal.GetReference(left);
        ref var rightRef = ref MemoryMarshal.GetReference(right);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        var end = left.Length - 3;
        for (; index < end; index += 4)
        {
            Unsafe.Add(ref destinationRef, index) = Unsafe.Add(ref leftRef, index) + Unsafe.Add(ref rightRef, index);
            Unsafe.Add(ref destinationRef, index + 1) = Unsafe.Add(ref leftRef, index + 1) + Unsafe.Add(ref rightRef, index + 1);
            Unsafe.Add(ref destinationRef, index + 2) = Unsafe.Add(ref leftRef, index + 2) + Unsafe.Add(ref rightRef, index + 2);
            Unsafe.Add(ref destinationRef, index + 3) = Unsafe.Add(ref leftRef, index + 3) + Unsafe.Add(ref rightRef, index + 3);
        }

        switch (left.Length - index)
        {
            case 3:
                Unsafe.Add(ref destinationRef, index) = Unsafe.Add(ref leftRef, index) + Unsafe.Add(ref rightRef, index);
                Unsafe.Add(ref destinationRef, index + 1) = Unsafe.Add(ref leftRef, index + 1) + Unsafe.Add(ref rightRef, index + 1);
                Unsafe.Add(ref destinationRef, index + 2) = Unsafe.Add(ref leftRef, index + 2) + Unsafe.Add(ref rightRef, index + 2);
                break;
            case 2:
                Unsafe.Add(ref destinationRef, index) = Unsafe.Add(ref leftRef, index) + Unsafe.Add(ref rightRef, index);
                Unsafe.Add(ref destinationRef, index + 1) = Unsafe.Add(ref leftRef, index + 1) + Unsafe.Add(ref rightRef, index + 1);
                break;
            case 1:
                Unsafe.Add(ref destinationRef, index) = Unsafe.Add(ref leftRef, index) + Unsafe.Add(ref rightRef, index);
                break;
        }
    }

    static void For_SIMD<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        var indexSource = 0;

        if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
        {
            var leftVectors = MemoryMarshal.Cast<T, Vector<T>>(left);
            var rightVectors = MemoryMarshal.Cast<T, Vector<T>>(right);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            ref var leftVectorsRef = ref MemoryMarshal.GetReference(leftVectors);
            ref var rightVectorsRef = ref MemoryMarshal.GetReference(rightVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var endVectors = leftVectors.Length;
            for (var indexVector = 0; indexVector < endVectors; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = Unsafe.Add(ref leftVectorsRef, indexVector) + Unsafe.Add(ref rightVectorsRef, indexVector);
            }

            indexSource = leftVectors.Length * Vector<T>.Count;
        }

        For_GetReference_4(indexSource, left, right, destination);
    }

    static void Parallel_For<T>(T[] left, T[] right, T[] destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        Parallel.For(0, left.Length, index => destination[index] = left[index] + right[index]);
    }

    static void Parallel_Invoke<T>(ReadOnlyMemory<T> left, ReadOnlyMemory<T> right, Memory<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        const int minChunkCount = 4;
        const int minChunkSize = 1_000;

        var coreCount = Environment.ProcessorCount;

        if (coreCount >= minChunkCount && left.Length > minChunkCount * minChunkSize)
            ParallelApply(left, right, destination, coreCount);
        else
            For_GetReference_4(0, left.Span, right.Span, destination.Span);

        static void ParallelApply(ReadOnlyMemory<T> left, ReadOnlyMemory<T> right, Memory<T> destination, int coreCount)
        {
            var totalSize = left.Length;
            var chunkSize = int.Max(totalSize / coreCount, minChunkSize);

            var actions = GC.AllocateArray<Action>(totalSize / chunkSize);
            var start = 0;
            for (var index = 0; index < actions.Length; index++)
            {
                var length = (index == actions.Length - 1)
                    ? totalSize - start
                    : chunkSize;

                var leftSlice = left.Slice(start, length);
                var rightSlice = right.Slice(start, length);
                var destinationSlice = destination.Slice(start, length);
                actions[index] = () => For_GetReference_4(0, leftSlice.Span, rightSlice.Span, destinationSlice.Span);

                start += length;
            }
            Parallel.Invoke(actions);
        }
    }

    static void Parallel_Invoke_SIMD<T>(ReadOnlyMemory<T> left, ReadOnlyMemory<T> right, Memory<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        const int minChunkCount = 4;
        const int minChunkSize = 1_000;

        var coreCount = Environment.ProcessorCount;

        if (coreCount >= minChunkCount && left.Length > minChunkCount * minChunkSize)
            ParallelApply(left, right, destination, coreCount);
        else
            For_SIMD(left.Span, right.Span, destination.Span);

        static void ParallelApply(ReadOnlyMemory<T> left, ReadOnlyMemory<T> right, Memory<T> destination, int coreCount)
        {
            var totalSize = left.Length;
            var chunkSize = int.Max(totalSize / coreCount, minChunkSize);

            var actions = GC.AllocateArray<Action>(totalSize / chunkSize);
            var start = 0;
            for (var index = 0; index < actions.Length; index++)
            {
                var length = (index == actions.Length - 1)
                    ? totalSize - start
                    : chunkSize;

                var leftSlice = left.Slice(start, length);
                var rightSlice = right.Slice(start, length);
                var destinationSlice = destination.Slice(start, length);
                actions[index] = () => For_SIMD(leftSlice.Span, rightSlice.Span, destinationSlice.Span);

                start += length;
            }
            Parallel.Invoke(actions);
        }
    }
}
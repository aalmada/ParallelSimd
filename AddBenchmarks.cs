﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace ParallelSimd;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AddBenchmarks
{
    short[]? sourceShort, otherShort, resultShort;
    int[]? sourceInt, otherInt, resultInt;
    long[]? sourceLong, otherLong, resultLong;
    Half[]? sourceHalf, otherHalf, resultHalf;
    float[]? sourceFloat, otherFloat, resultFloat;
    double[]? sourceDouble, otherDouble, resultDouble;

    [Params(1_000, 10_000, 100_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        sourceShort = new short[Count];
        otherShort = new short[Count];
        resultShort = new short[Count];
        sourceInt = new int[Count];
        otherInt = new int[Count];
        resultInt = new int[Count];
        sourceLong = new long[Count];
        otherLong = new long[Count];
        resultLong = new long[Count];
        sourceHalf = new Half[Count];
        otherHalf = new Half[Count];
        resultHalf = new Half[Count];
        sourceFloat = new float[Count];
        otherFloat = new float[Count];
        resultFloat = new float[Count];
        sourceDouble = new double[Count];
        otherDouble = new double[Count];
        resultDouble = new double[Count];

        var random = new Random(42);
        for (var index = 0; index < Count; index++)
        {
            var value = random.Next(10);
            sourceShort[index] = (short)value;
            otherShort[index] = (short)value;
            sourceInt[index] = value;
            otherInt[index] = value;
            sourceLong[index] = value;
            otherLong[index] = value;
            sourceHalf[index] = (Half)value;
            otherHalf[index] = (Half)value;
            sourceFloat[index] = value;
            otherFloat[index] = value;
            sourceDouble[index] = value;
            otherDouble[index] = value;
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public void System_Short()
        => TensorPrimitives.Add<short>(sourceShort!, otherShort!, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void NetFabric_Short()
        => TensorOperations.Add(sourceShort!.AsSpan(), otherShort!.AsSpan(), resultShort!.AsSpan());

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void NetFabric_Parallel_Short()
        => TensorOperations.Add(sourceShort!, otherShort!, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void System_Int()
        => TensorPrimitives.Add<int>(sourceInt!, otherInt!, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void NetFabric_Int()
        => TensorOperations.Add(sourceInt!.AsSpan(), otherInt!.AsSpan(), resultInt!.AsSpan());

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void NetFabric_Parallel_Int()
        => TensorOperations.Add(sourceInt!, otherInt!, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void System_Long()
        => TensorPrimitives.Add<long>(sourceLong!, otherLong!, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void NetFabric_Long()
        => TensorOperations.Add(sourceLong!.AsSpan(), otherLong!.AsSpan(), resultLong!.AsSpan());

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void NetFabric_Parallel_Long()
        => TensorOperations.Add(sourceLong!, otherLong!, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void System_Half()
        => TensorPrimitives.Add<Half>(sourceHalf!, otherHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void NetFabric_Half()
        => TensorOperations.Add(sourceHalf!.AsSpan(), otherHalf!.AsSpan(), resultHalf!.AsSpan());

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void NetFabric_Parallel_Half()
        => TensorOperations.Add(sourceHalf!, otherHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void System_Float()
        => TensorPrimitives.Add<float>(sourceFloat!, otherFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void NetFabric_Float()
        => TensorOperations.Add(sourceFloat!.AsSpan(), otherFloat!.AsSpan(), resultFloat!.AsSpan());

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void NetFabric_Parallel_Float()
        => TensorOperations.Add(sourceFloat!, otherFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void System_Double()
        => TensorPrimitives.Add<double>(sourceDouble!, otherDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void NetFabric_Double()
        => TensorOperations.Add(sourceDouble!.AsSpan(), otherDouble!.AsSpan(), resultDouble!.AsSpan());

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void NetFabric_Parallel_Double()
        => TensorOperations.Add(sourceDouble!, otherDouble!, resultDouble!);
}
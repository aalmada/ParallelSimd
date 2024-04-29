```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4355/22H2/2022Update)
Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.100-preview.1.24101.2
  [Host]    : .NET 9.0.0 (9.0.24.8009), X64 RyuJIT AVX2
  Scalar    : .NET 9.0.0 (9.0.24.8009), X64 RyuJIT
  Vector128 : .NET 9.0.0 (9.0.24.8009), X64 RyuJIT AVX
  Vector256 : .NET 9.0.0 (9.0.24.8009), X64 RyuJIT AVX2


```
| Method                    | Job       | Categories | Count  | Mean            | StdDev         | Median          | Ratio         | 
|-------------------------- |---------- |----------- |------- |----------------:|---------------:|----------------:|--------------:|-
| **System_Double**             | **Scalar**    | **Double**     | **1000**   |       **423.42 ns** |      **12.665 ns** |       **420.42 ns** |      **baseline** | 
| NetFabric_Double          | Scalar    | Double     | 1000   |       409.22 ns |       3.450 ns |       409.57 ns |  1.04x faster | 
| NetFabric_Parallel_Double | Scalar    | Double     | 1000   |       421.00 ns |      13.315 ns |       415.92 ns |  1.00x faster | 
| System_Double             | Vector128 | Double     | 1000   |       176.96 ns |       4.131 ns |       177.15 ns |  2.40x faster | 
| NetFabric_Double          | Vector128 | Double     | 1000   |       285.03 ns |      10.580 ns |       282.35 ns |  1.49x faster | 
| NetFabric_Parallel_Double | Vector128 | Double     | 1000   |       291.13 ns |       5.441 ns |       290.04 ns |  1.46x faster | 
| System_Double             | Vector256 | Double     | 1000   |       117.25 ns |       6.995 ns |       113.68 ns |  3.65x faster | 
| NetFabric_Double          | Vector256 | Double     | 1000   |       152.02 ns |       5.310 ns |       150.49 ns |  2.80x faster | 
| NetFabric_Parallel_Double | Vector256 | Double     | 1000   |       165.69 ns |       8.945 ns |       162.80 ns |  2.52x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Double**             | **Scalar**    | **Double**     | **10000**  |     **4,857.19 ns** |      **51.249 ns** |     **4,866.23 ns** |      **baseline** | 
| NetFabric_Double          | Scalar    | Double     | 10000  |     4,854.06 ns |      88.533 ns |     4,851.99 ns |  1.00x faster | 
| NetFabric_Parallel_Double | Scalar    | Double     | 10000  |     5,618.05 ns |     197.897 ns |     5,632.08 ns |  1.13x slower | 
| System_Double             | Vector128 | Double     | 10000  |     2,978.72 ns |      43.438 ns |     2,983.64 ns |  1.63x faster | 
| NetFabric_Double          | Vector128 | Double     | 10000  |     3,506.48 ns |     393.283 ns |     3,576.19 ns |  1.53x faster | 
| NetFabric_Parallel_Double | Vector128 | Double     | 10000  |     5,062.58 ns |     167.981 ns |     5,037.22 ns |  1.06x slower | 
| System_Double             | Vector256 | Double     | 10000  |     2,433.85 ns |      75.732 ns |     2,434.91 ns |  2.01x faster | 
| NetFabric_Double          | Vector256 | Double     | 10000  |     2,811.26 ns |      59.891 ns |     2,799.36 ns |  1.73x faster | 
| NetFabric_Parallel_Double | Vector256 | Double     | 10000  |     4,405.57 ns |     220.512 ns |     4,414.60 ns |  1.12x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Double**             | **Scalar**    | **Double**     | **100000** |    **56,629.65 ns** |   **1,559.702 ns** |    **56,261.06 ns** |      **baseline** | 
| NetFabric_Double          | Scalar    | Double     | 100000 |    56,443.03 ns |   1,725.355 ns |    55,971.56 ns |  1.00x slower | 
| NetFabric_Parallel_Double | Scalar    | Double     | 100000 |    34,954.66 ns |   3,038.804 ns |    34,726.59 ns |  1.68x faster | 
| System_Double             | Vector128 | Double     | 100000 |    37,587.31 ns |     866.415 ns |    37,320.03 ns |  1.51x faster | 
| NetFabric_Double          | Vector128 | Double     | 100000 |    43,443.47 ns |   1,321.636 ns |    43,434.32 ns |  1.30x faster | 
| NetFabric_Parallel_Double | Vector128 | Double     | 100000 |    29,884.73 ns |   2,384.675 ns |    29,992.40 ns |  1.95x faster | 
| System_Double             | Vector256 | Double     | 100000 |    34,861.78 ns |     615.970 ns |    34,724.94 ns |  1.64x faster | 
| NetFabric_Double          | Vector256 | Double     | 100000 |    41,519.43 ns |     867.807 ns |    41,538.17 ns |  1.37x faster | 
| NetFabric_Parallel_Double | Vector256 | Double     | 100000 |    26,878.53 ns |     963.197 ns |    26,711.13 ns |  2.13x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Float**              | **Scalar**    | **Float**      | **1000**   |       **415.28 ns** |      **11.871 ns** |       **411.74 ns** |      **baseline** | 
| NetFabric_Float           | Scalar    | Float      | 1000   |       422.25 ns |      24.359 ns |       413.44 ns |  1.02x slower | 
| NetFabric_Parallel_Float  | Scalar    | Float      | 1000   |       421.74 ns |       4.439 ns |       421.96 ns |  1.00x slower | 
| System_Float              | Vector128 | Float      | 1000   |        92.84 ns |       2.092 ns |        92.40 ns |  4.51x faster | 
| NetFabric_Float           | Vector128 | Float      | 1000   |       152.42 ns |       7.427 ns |       151.08 ns |  2.73x faster | 
| NetFabric_Parallel_Float  | Vector128 | Float      | 1000   |       156.71 ns |       5.158 ns |       156.03 ns |  2.65x faster | 
| System_Float              | Vector256 | Float      | 1000   |        62.89 ns |       1.172 ns |        62.77 ns |  6.70x faster | 
| NetFabric_Float           | Vector256 | Float      | 1000   |        84.68 ns |       3.174 ns |        84.29 ns |  4.89x faster | 
| NetFabric_Parallel_Float  | Vector256 | Float      | 1000   |        91.75 ns |       3.436 ns |        91.44 ns |  4.52x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Float**              | **Scalar**    | **Float**      | **10000**  |     **4,483.27 ns** |     **141.243 ns** |     **4,446.83 ns** |      **baseline** | 
| NetFabric_Float           | Scalar    | Float      | 10000  |     4,410.07 ns |     116.792 ns |     4,374.98 ns |  1.02x faster | 
| NetFabric_Parallel_Float  | Scalar    | Float      | 10000  |     5,200.04 ns |     206.719 ns |     5,195.03 ns |  1.16x slower | 
| System_Float              | Vector128 | Float      | 10000  |     1,630.62 ns |     122.987 ns |     1,661.62 ns |  2.92x faster | 
| NetFabric_Float           | Vector128 | Float      | 10000  |     1,738.21 ns |     169.888 ns |     1,764.58 ns |  2.87x faster | 
| NetFabric_Parallel_Float  | Vector128 | Float      | 10000  |     3,522.81 ns |     182.918 ns |     3,553.60 ns |  1.28x faster | 
| System_Float              | Vector256 | Float      | 10000  |     1,178.27 ns |      20.035 ns |     1,177.04 ns |  3.79x faster | 
| NetFabric_Float           | Vector256 | Float      | 10000  |     1,237.01 ns |      20.473 ns |     1,235.91 ns |  3.63x faster | 
| NetFabric_Parallel_Float  | Vector256 | Float      | 10000  |     3,181.74 ns |      36.943 ns |     3,187.26 ns |  1.40x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Float**              | **Scalar**    | **Float**      | **100000** |    **45,601.39 ns** |     **972.035 ns** |    **45,563.93 ns** |      **baseline** | 
| NetFabric_Float           | Scalar    | Float      | 100000 |    45,427.04 ns |   1,244.390 ns |    45,114.40 ns |  1.00x faster | 
| NetFabric_Parallel_Float  | Scalar    | Float      | 100000 |    30,036.78 ns |   1,864.661 ns |    30,211.59 ns |  1.56x faster | 
| System_Float              | Vector128 | Float      | 100000 |    18,620.74 ns |     400.252 ns |    18,596.08 ns |  2.45x faster | 
| NetFabric_Float           | Vector128 | Float      | 100000 |    21,322.86 ns |     776.117 ns |    21,135.02 ns |  2.14x faster | 
| NetFabric_Parallel_Float  | Vector128 | Float      | 100000 |    16,550.81 ns |   1,065.138 ns |    16,573.44 ns |  2.94x faster | 
| System_Float              | Vector256 | Float      | 100000 |    16,335.72 ns |     502.075 ns |    16,264.79 ns |  2.80x faster | 
| NetFabric_Float           | Vector256 | Float      | 100000 |    19,705.53 ns |     307.977 ns |    19,688.59 ns |  2.31x faster | 
| NetFabric_Parallel_Float  | Vector256 | Float      | 100000 |    14,894.54 ns |     623.532 ns |    14,861.30 ns |  3.07x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Half**               | **Scalar**    | **Half**       | **1000**   |    **12,412.83 ns** |     **133.814 ns** |    **12,418.72 ns** |      **baseline** | 
| NetFabric_Half            | Scalar    | Half       | 1000   |    12,494.82 ns |     115.692 ns |    12,541.34 ns |  1.01x slower | 
| NetFabric_Parallel_Half   | Scalar    | Half       | 1000   |    12,625.39 ns |     309.834 ns |    12,519.77 ns |  1.02x slower | 
| System_Half               | Vector128 | Half       | 1000   |     9,919.00 ns |     172.793 ns |     9,877.05 ns |  1.25x faster | 
| NetFabric_Half            | Vector128 | Half       | 1000   |     9,815.25 ns |     181.975 ns |     9,757.58 ns |  1.27x faster | 
| NetFabric_Parallel_Half   | Vector128 | Half       | 1000   |     9,936.12 ns |     303.575 ns |     9,845.55 ns |  1.25x faster | 
| System_Half               | Vector256 | Half       | 1000   |    10,030.37 ns |     346.337 ns |     9,932.71 ns |  1.25x faster | 
| NetFabric_Half            | Vector256 | Half       | 1000   |    10,030.29 ns |     339.312 ns |     9,888.22 ns |  1.23x faster | 
| NetFabric_Parallel_Half   | Vector256 | Half       | 1000   |     9,908.13 ns |     152.685 ns |     9,935.57 ns |  1.25x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Half**               | **Scalar**    | **Half**       | **10000**  |   **124,565.36 ns** |   **4,022.930 ns** |   **123,970.75 ns** |      **baseline** | 
| NetFabric_Half            | Scalar    | Half       | 10000  |   124,751.82 ns |   2,447.000 ns |   124,373.63 ns |  1.00x faster | 
| NetFabric_Parallel_Half   | Scalar    | Half       | 10000  |    64,107.28 ns |   7,726.589 ns |    62,861.29 ns |  2.02x faster | 
| System_Half               | Vector128 | Half       | 10000  |   101,938.71 ns |   6,498.672 ns |    99,578.33 ns |  1.23x faster | 
| NetFabric_Half            | Vector128 | Half       | 10000  |    98,567.86 ns |   1,320.438 ns |    98,511.68 ns |  1.28x faster | 
| NetFabric_Parallel_Half   | Vector128 | Half       | 10000  |    55,291.74 ns |   7,401.974 ns |    54,130.52 ns |  2.31x faster | 
| System_Half               | Vector256 | Half       | 10000  |   101,076.66 ns |   3,976.361 ns |   100,096.19 ns |  1.23x faster | 
| NetFabric_Half            | Vector256 | Half       | 10000  |    99,921.02 ns |   2,440.957 ns |    99,021.01 ns |  1.25x faster | 
| NetFabric_Parallel_Half   | Vector256 | Half       | 10000  |    54,662.81 ns |   9,844.101 ns |    53,704.93 ns |  2.41x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Half**               | **Scalar**    | **Half**       | **100000** | **1,326,928.19 ns** | **109,980.575 ns** | **1,274,343.95 ns** |      **baseline** | 
| NetFabric_Half            | Scalar    | Half       | 100000 | 1,257,322.84 ns |  15,950.870 ns | 1,257,608.30 ns |  1.05x faster | 
| NetFabric_Parallel_Half   | Scalar    | Half       | 100000 |   607,249.93 ns |  84,204.839 ns |   596,155.08 ns |  2.24x faster | 
| System_Half               | Vector128 | Half       | 100000 | 1,001,029.55 ns |  22,136.688 ns |   991,844.43 ns |  1.37x faster | 
| NetFabric_Half            | Vector128 | Half       | 100000 |   997,195.51 ns |  21,736.025 ns |   994,103.71 ns |  1.37x faster | 
| NetFabric_Parallel_Half   | Vector128 | Half       | 100000 |   531,768.23 ns |  77,099.900 ns |   534,569.53 ns |  2.56x faster | 
| System_Half               | Vector256 | Half       | 100000 | 1,009,560.94 ns |  38,547.747 ns | 1,005,995.70 ns |  1.33x faster | 
| NetFabric_Half            | Vector256 | Half       | 100000 |   999,063.44 ns |  29,593.159 ns |   989,604.88 ns |  1.37x faster | 
| NetFabric_Parallel_Half   | Vector256 | Half       | 100000 |   536,826.45 ns |  79,012.765 ns |   533,721.53 ns |  2.52x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Int**                | **Scalar**    | **Int**        | **1000**   |       **418.91 ns** |      **10.454 ns** |       **415.24 ns** |      **baseline** | 
| NetFabric_Int             | Scalar    | Int        | 1000   |       417.69 ns |      15.535 ns |       411.70 ns |  1.01x slower | 
| NetFabric_Parallel_Int    | Scalar    | Int        | 1000   |       422.03 ns |       6.184 ns |       421.98 ns |  1.01x slower | 
| System_Int                | Vector128 | Int        | 1000   |        95.59 ns |       4.789 ns |        94.17 ns |  4.32x faster | 
| NetFabric_Int             | Vector128 | Int        | 1000   |       151.06 ns |       5.951 ns |       150.31 ns |  2.76x faster | 
| NetFabric_Parallel_Int    | Vector128 | Int        | 1000   |       154.37 ns |       2.809 ns |       154.48 ns |  2.71x faster | 
| System_Int                | Vector256 | Int        | 1000   |        62.35 ns |       1.032 ns |        62.18 ns |  6.72x faster | 
| NetFabric_Int             | Vector256 | Int        | 1000   |        84.49 ns |       3.100 ns |        83.81 ns |  4.97x faster | 
| NetFabric_Parallel_Int    | Vector256 | Int        | 1000   |        93.85 ns |       3.238 ns |        94.10 ns |  4.46x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Int**                | **Scalar**    | **Int**        | **10000**  |     **4,196.36 ns** |     **176.515 ns** |     **4,194.37 ns** |      **baseline** | 
| NetFabric_Int             | Scalar    | Int        | 10000  |     4,237.18 ns |     225.346 ns |     4,168.34 ns |  1.01x slower | 
| NetFabric_Parallel_Int    | Scalar    | Int        | 10000  |     5,070.96 ns |     188.554 ns |     5,051.94 ns |  1.21x slower | 
| System_Int                | Vector128 | Int        | 10000  |     1,463.94 ns |      30.717 ns |     1,455.90 ns |  2.84x faster | 
| NetFabric_Int             | Vector128 | Int        | 10000  |     1,784.71 ns |     168.310 ns |     1,822.13 ns |  2.43x faster | 
| NetFabric_Parallel_Int    | Vector128 | Int        | 10000  |     3,542.32 ns |      97.072 ns |     3,532.15 ns |  1.18x faster | 
| System_Int                | Vector256 | Int        | 10000  |     1,238.08 ns |      45.470 ns |     1,230.24 ns |  3.41x faster | 
| NetFabric_Int             | Vector256 | Int        | 10000  |     1,301.45 ns |      69.109 ns |     1,278.77 ns |  3.23x faster | 
| NetFabric_Parallel_Int    | Vector256 | Int        | 10000  |     3,224.35 ns |      78.037 ns |     3,227.73 ns |  1.29x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Int**                | **Scalar**    | **Int**        | **100000** |    **43,202.95 ns** |     **978.993 ns** |    **43,092.96 ns** |      **baseline** | 
| NetFabric_Int             | Scalar    | Int        | 100000 |    44,754.18 ns |   1,751.795 ns |    44,795.92 ns |  1.03x slower | 
| NetFabric_Parallel_Int    | Scalar    | Int        | 100000 |    30,346.72 ns |   1,714.525 ns |    30,536.37 ns |  1.50x faster | 
| System_Int                | Vector128 | Int        | 100000 |    17,943.60 ns |     402.601 ns |    17,954.49 ns |  2.41x faster | 
| NetFabric_Int             | Vector128 | Int        | 100000 |    21,183.09 ns |     521.986 ns |    21,074.27 ns |  2.04x faster | 
| NetFabric_Parallel_Int    | Vector128 | Int        | 100000 |    16,090.57 ns |     719.616 ns |    16,141.36 ns |  2.75x faster | 
| System_Int                | Vector256 | Int        | 100000 |    16,590.64 ns |     383.678 ns |    16,547.38 ns |  2.60x faster | 
| NetFabric_Int             | Vector256 | Int        | 100000 |    19,914.99 ns |     566.470 ns |    19,782.57 ns |  2.17x faster | 
| NetFabric_Parallel_Int    | Vector256 | Int        | 100000 |    14,486.21 ns |     569.348 ns |    14,516.71 ns |  3.03x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Long**               | **Scalar**    | **Long**       | **1000**   |       **427.89 ns** |      **25.675 ns** |       **418.79 ns** |      **baseline** | 
| NetFabric_Long            | Scalar    | Long       | 1000   |       424.92 ns |      11.473 ns |       422.96 ns |  1.01x faster | 
| NetFabric_Parallel_Long   | Scalar    | Long       | 1000   |       434.76 ns |      12.413 ns |       432.67 ns |  1.02x slower | 
| System_Long               | Vector128 | Long       | 1000   |       180.63 ns |       7.494 ns |       180.11 ns |  2.38x faster | 
| NetFabric_Long            | Vector128 | Long       | 1000   |       307.96 ns |      35.590 ns |       296.15 ns |  1.40x faster | 
| NetFabric_Parallel_Long   | Vector128 | Long       | 1000   |       304.48 ns |      19.554 ns |       300.97 ns |  1.41x faster | 
| System_Long               | Vector256 | Long       | 1000   |       112.49 ns |       2.340 ns |       112.38 ns |  3.84x faster | 
| NetFabric_Long            | Vector256 | Long       | 1000   |       161.16 ns |       5.825 ns |       160.65 ns |  2.65x faster | 
| NetFabric_Parallel_Long   | Vector256 | Long       | 1000   |       179.59 ns |      15.127 ns |       174.98 ns |  2.40x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Long**               | **Scalar**    | **Long**       | **10000**  |     **5,176.10 ns** |     **397.183 ns** |     **5,093.66 ns** |      **baseline** | 
| NetFabric_Long            | Scalar    | Long       | 10000  |     4,801.33 ns |     177.136 ns |     4,800.04 ns |  1.08x faster | 
| NetFabric_Parallel_Long   | Scalar    | Long       | 10000  |     6,442.95 ns |     747.074 ns |     6,211.38 ns |  1.24x slower | 
| System_Long               | Vector128 | Long       | 10000  |     3,814.38 ns |     337.822 ns |     3,705.06 ns |  1.36x faster | 
| NetFabric_Long            | Vector128 | Long       | 10000  |     3,660.69 ns |      78.158 ns |     3,668.98 ns |  1.43x faster | 
| NetFabric_Parallel_Long   | Vector128 | Long       | 10000  |     4,884.53 ns |     172.184 ns |     4,895.06 ns |  1.06x faster | 
| System_Long               | Vector256 | Long       | 10000  |     2,409.64 ns |      31.217 ns |     2,420.32 ns |  2.21x faster | 
| NetFabric_Long            | Vector256 | Long       | 10000  |     2,782.16 ns |      79.217 ns |     2,765.18 ns |  1.88x faster | 
| NetFabric_Parallel_Long   | Vector256 | Long       | 10000  |     4,618.83 ns |     177.562 ns |     4,626.44 ns |  1.12x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Long**               | **Scalar**    | **Long**       | **100000** |    **54,995.01 ns** |   **2,705.739 ns** |    **54,184.34 ns** |      **baseline** | 
| NetFabric_Long            | Scalar    | Long       | 100000 |    54,866.67 ns |   2,100.238 ns |    54,675.60 ns |  1.01x faster | 
| NetFabric_Parallel_Long   | Scalar    | Long       | 100000 |    33,781.86 ns |   2,547.395 ns |    33,602.67 ns |  1.66x faster | 
| System_Long               | Vector128 | Long       | 100000 |    37,665.90 ns |   1,094.701 ns |    37,538.00 ns |  1.48x faster | 
| NetFabric_Long            | Vector128 | Long       | 100000 |    46,087.07 ns |   4,790.318 ns |    44,496.77 ns |  1.20x faster | 
| NetFabric_Parallel_Long   | Vector128 | Long       | 100000 |    30,052.95 ns |   2,441.086 ns |    29,695.56 ns |  1.85x faster | 
| System_Long               | Vector256 | Long       | 100000 |    35,778.76 ns |   1,573.031 ns |    35,038.98 ns |  1.54x faster | 
| NetFabric_Long            | Vector256 | Long       | 100000 |    40,785.61 ns |     582.515 ns |    40,755.97 ns |  1.36x faster | 
| NetFabric_Parallel_Long   | Vector256 | Long       | 100000 |    25,795.44 ns |     844.567 ns |    25,667.30 ns |  2.17x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Short**              | **Scalar**    | **Short**      | **1000**   |       **420.34 ns** |      **13.542 ns** |       **417.27 ns** |      **baseline** | 
| NetFabric_Short           | Scalar    | Short      | 1000   |       423.02 ns |       7.344 ns |       421.15 ns |  1.00x faster | 
| NetFabric_Parallel_Short  | Scalar    | Short      | 1000   |       445.83 ns |      20.687 ns |       440.55 ns |  1.06x slower | 
| System_Short              | Vector128 | Short      | 1000   |        50.63 ns |       2.249 ns |        50.44 ns |  8.27x faster | 
| NetFabric_Short           | Vector128 | Short      | 1000   |        85.34 ns |       3.922 ns |        84.54 ns |  4.94x faster | 
| NetFabric_Parallel_Short  | Vector128 | Short      | 1000   |        94.16 ns |       5.171 ns |        92.45 ns |  4.49x faster | 
| System_Short              | Vector256 | Short      | 1000   |        34.74 ns |       1.607 ns |        34.67 ns | 12.19x faster | 
| NetFabric_Short           | Vector256 | Short      | 1000   |        53.85 ns |       3.126 ns |        52.84 ns |  7.82x faster | 
| NetFabric_Parallel_Short  | Vector256 | Short      | 1000   |        68.04 ns |       4.680 ns |        67.50 ns |  6.19x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Short**              | **Scalar**    | **Short**      | **10000**  |     **4,449.01 ns** |     **381.551 ns** |     **4,338.64 ns** |      **baseline** | 
| NetFabric_Short           | Scalar    | Short      | 10000  |     4,540.20 ns |     489.361 ns |     4,432.80 ns |  1.02x slower | 
| NetFabric_Parallel_Short  | Scalar    | Short      | 10000  |     5,564.77 ns |     222.944 ns |     5,568.47 ns |  1.26x slower | 
| System_Short              | Vector128 | Short      | 10000  |       848.94 ns |      34.389 ns |       846.42 ns |  5.20x faster | 
| NetFabric_Short           | Vector128 | Short      | 10000  |     1,084.32 ns |      74.597 ns |     1,068.71 ns |  4.12x faster | 
| NetFabric_Parallel_Short  | Vector128 | Short      | 10000  |     3,068.21 ns |     121.028 ns |     3,057.97 ns |  1.45x faster | 
| System_Short              | Vector256 | Short      | 10000  |       640.21 ns |      20.283 ns |       639.23 ns |  7.04x faster | 
| NetFabric_Short           | Vector256 | Short      | 10000  |       682.89 ns |      26.094 ns |       675.96 ns |  6.47x faster | 
| NetFabric_Parallel_Short  | Vector256 | Short      | 10000  |     3,078.07 ns |     326.871 ns |     2,918.44 ns |  1.48x faster | 
|                           |           |            |        |                 |                |                 |               | 
| **System_Short**              | **Scalar**    | **Short**      | **100000** |    **46,974.80 ns** |   **5,309.881 ns** |    **45,742.96 ns** |      **baseline** | 
| NetFabric_Short           | Scalar    | Short      | 100000 |    45,508.71 ns |   4,329.183 ns |    43,742.24 ns |  1.04x faster | 
| NetFabric_Parallel_Short  | Scalar    | Short      | 100000 |    30,369.69 ns |   2,620.894 ns |    30,590.46 ns |  1.56x faster | 
| System_Short              | Vector128 | Short      | 100000 |     9,478.47 ns |     147.803 ns |     9,430.80 ns |  5.17x faster | 
| NetFabric_Short           | Vector128 | Short      | 100000 |    10,241.60 ns |     411.962 ns |    10,178.68 ns |  4.63x faster | 
| NetFabric_Parallel_Short  | Vector128 | Short      | 100000 |    10,994.98 ns |     857.803 ns |    11,065.52 ns |  4.31x faster | 
| System_Short              | Vector256 | Short      | 100000 |     9,119.31 ns |     390.941 ns |     9,057.00 ns |  5.23x faster | 
| NetFabric_Short           | Vector256 | Short      | 100000 |     9,802.39 ns |     966.465 ns |     9,412.58 ns |  4.82x faster | 
| NetFabric_Parallel_Short  | Vector256 | Short      | 100000 |     8,959.19 ns |     404.419 ns |     8,965.06 ns |  5.29x faster | 

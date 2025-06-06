# TestValueComparerDelegate

## Result

```
BenchmarkDotNet v0.15.0, Windows 11 (10.0.26100.4061/24H2/2024Update/HudsonValley)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 9.0.1 (9.0.124.61010), X64 AOT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```
Not stable, multi test result are different

| Method    | A   | B   | Mean      | Error     | StdDev    | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|---------- |---- |---- |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Delegate  | 123 | 456 | 0.0297 ns | 0.0074 ns | 0.0069 ns |  1.04 |    0.31 |      47 B |         - |          NA |
| Value     | 123 | 456 | 0.4436 ns | 0.0058 ns | 0.0054 ns | 15.59 |    2.97 |      27 B |         - |          NA |
| Interface | 123 | 456 | 0.0324 ns | 0.0056 ns | 0.0050 ns |  1.14 |    0.28 |      51 B |         - |          NA |

| Method    | A   | B   | Mean      | Error     | StdDev    | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|---------- |---- |---- |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Delegate  | 123 | 456 | 0.0287 ns | 0.0091 ns | 0.0081 ns |  1.08 |    0.44 |      47 B |         - |          NA |
| Value     | 123 | 456 | 0.4637 ns | 0.0114 ns | 0.0101 ns | 17.51 |    5.11 |      27 B |         - |          NA |
| Interface | 123 | 456 | 0.0344 ns | 0.0062 ns | 0.0055 ns |  1.30 |    0.43 |      51 B |         - |          NA |

| Method    | A   | B   | Mean      | Error     | StdDev    | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|---------- |---- |---- |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Delegate  | 123 | 456 | 0.2237 ns | 0.0057 ns | 0.0053 ns |  1.00 |    0.03 |      47 B |         - |          NA |
| Value     | 123 | 456 | 0.4633 ns | 0.0158 ns | 0.0148 ns |  2.07 |    0.08 |      27 B |         - |          NA |
| Interface | 123 | 456 | 0.4524 ns | 0.0479 ns | 0.1411 ns |  2.02 |    0.63 |      51 B |         - |          NA |

| Method    | A   | B   | Mean      | Error     | StdDev    | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|---------- |---- |---- |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Delegate  | 123 | 456 | 0.2328 ns | 0.0052 ns | 0.0046 ns |  1.00 |    0.03 |      47 B |         - |          NA |
| Value     | 123 | 456 | 0.4439 ns | 0.0094 ns | 0.0088 ns |  1.91 |    0.05 |      27 B |         - |          NA |
| Interface | 123 | 456 | 0.2483 ns | 0.0170 ns | 0.0159 ns |  1.07 |    0.07 |      51 B |         - |          NA |


## Asm

## .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```assembly
; Benchmark.Test1.Delegate()
       mov       r10,[rcx+10]
       mov       edx,[rcx+18]
       mov       r8d,[rcx+1C]
       mov       rax,offset Benchmark.Test1+Comparer.Compare(Int32, Int32)
       cmp       [r10+18],rax
       jne       short M00_L00
       mov       eax,edx
       sub       eax,r8d
       ret
M00_L00:
       mov       rcx,[r10+8]
       jmp       qword ptr [r10+18]
; Total bytes of code 41
```
```assembly
; Benchmark.Test1+Comparer.Compare(Int32, Int32)
       mov       eax,edx
       sub       eax,r8d
       ret
; Total bytes of code 6
```

## .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```assembly
; Benchmark.Test1.Value()
       lea       rdx,[rcx+20]
       mov       r8d,[rcx+18]
       mov       eax,[rcx+1C]
       mov       rcx,[rdx]
       mov       r10,[rdx+8]
       mov       edx,r8d
       mov       r8d,eax
       jmp       r10
; Total bytes of code 27
```

## .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```assembly
; Benchmark.Test1.Interface()
       mov       r8,[rcx+8]
       mov       edx,[rcx+18]
       mov       r11d,[rcx+1C]
       mov       rax,offset MT_Benchmark.Test1+Comparer
       cmp       [r8],rax
       jne       short M00_L00
       mov       eax,edx
       sub       eax,r11d
       ret
M00_L00:
       mov       rcx,r8
       mov       r8d,r11d
       mov       r11,7FF997B40478
       jmp       qword ptr [r11]
; Total bytes of code 51
```

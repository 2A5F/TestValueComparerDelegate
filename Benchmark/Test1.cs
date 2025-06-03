using BenchmarkDotNet.Attributes;
using TestValueComparerDelegate;

namespace Benchmark;

[MemoryDiagnoser(false)]
[DisassemblyDiagnoser(maxDepth: 1000, exportDiff: true)]
public class Test1
{
    private IComparer<int> Compare = new Comparer();
    private Comparison<int> DelegateComparison;
    private ValueComparison<int> ValueComparison;

    public Test1()
    {
        DelegateComparison = Compare.Compare;
        ValueComparison = ValueComparison<int>.Create(Compare);
    }
    
    public class Comparer : IComparer<int>
    {
        public int Compare(int x, int y) => x - y;
    }
    
    [Params(123)]
    public int A { get; set; }

    [Params(456)]
    public int B { get; set; }

    [Benchmark(Baseline = true)]
    public int Delegate() => DelegateComparison(A, B);
    
    [Benchmark]
    public int Value() => ValueComparison.Compare(A, B);
    
    [Benchmark]
    public int Interface() => Compare.Compare(A, B);
}

using TestValueComparerDelegate;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void Test1()
    {
        var a = ValueComparison<int>.Create(new Comparer());
        var r = a.Compare(123, 456);
        Assert.That(r, Is.EqualTo(123 - 456));
    }

    public class Comparer : IComparer<int>
    {
        public int Compare(int x, int y) => x - y;
    }
}

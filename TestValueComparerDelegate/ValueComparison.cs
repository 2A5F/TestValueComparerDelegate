using System.Reflection;
using System.Runtime.CompilerServices;
using InlineIL;
using static InlineIL.IL.Emit;

namespace TestValueComparerDelegate;

public readonly unsafe struct ValueComparison<T>(object obj, void* ptr) : IComparer<T>
{
    private readonly object obj = obj;
    private readonly void* ptr = ptr;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public int Compare(T? x, T? y)
    {
        Ldarg_0();
        Ldfld(FieldRef.Field(typeof(ValueComparison<T>), nameof(obj)));
        Ldarg(nameof(x));
        Ldarg(nameof(y));
        Ldarg_0();
        Ldfld(FieldRef.Field(typeof(ValueComparison<T>), nameof(ptr)));
        Calli(StandAloneMethodSig.ManagedMethod(CallingConventions.HasThis, typeof(int), typeof(T?), typeof(T?)));
        return IL.Return<int>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ValueComparison<T> Create(IComparer<T> obj)
    {
        Ldarg(nameof(obj));
        Dup();
        Ldvirtftn(MethodRef.Method(typeof(IComparer<T>), nameof(IComparer<T>.Compare)));
        Newobj(MethodRef.Constructor(typeof(ValueComparison<T>), typeof(object), typeof(void*)));
        return IL.Return<ValueComparison<T>>();
    }
}

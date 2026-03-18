#if NET8_0_OR_GREATER
using System.Reflection;
using System.Runtime.CompilerServices;
using Autodesk.Revit.ApplicationServices;
using BenchmarkDotNet.Attributes;
using Nice3point.BenchmarkDotNet.Revit;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

public class UnsafeAccessorsBenchmarks : RevitApiBenchmark
{
    [Benchmark]
    public ControlledApplication UnsafeAccessorsSingleton()
    {
        return UnsafeAccessors.CreateControlledApplication(Application);
    }

    [Benchmark]
    public ControlledApplication ReflectionSingleton()
    {
        return (ControlledApplication)Activator.CreateInstance(
            typeof(ControlledApplication),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            [Application],
            null)!;
    }
}

file static class UnsafeAccessors
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    public static extern ControlledApplication CreateControlledApplication(Application application);
}
#endif
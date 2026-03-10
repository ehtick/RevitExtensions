using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Nice3point.BenchmarkDotNet.Revit;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

/// <summary>
///     Provides test values for benchmarks, avoiding early JIT resolution of Revit API structs.
/// </summary>
file static class Constants
{
    public static BuiltInParameter Parameter => BuiltInParameter.ALL_MODEL_DESCRIPTION;
}

[MemoryDiagnoser]
public class ToParameterBenchmark : RevitApiBenchmark
{
    private Document _document;

    private static readonly Assembly Assembly = Assembly.GetAssembly(typeof(Parameter))!;
    private static readonly Type ADocumentType = Assembly.GetType("ADocument")!;
    private static readonly Type ElementIdType = Assembly.GetType("ElementId")!;
    private static readonly MethodInfo GetADocumentMethod = typeof(Document).GetMethod("getADocument", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
    private static readonly ConstructorInfo ParameterConstructor = typeof(Parameter).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, [ADocumentType.MakePointerType(), ElementIdType.MakePointerType()], null)!;

    protected override void OnGlobalSetup()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);
    }

    protected override void OnGlobalCleanup()
    {
        _document?.Close(false);
    }

    [Benchmark]
    public Parameter ReflectionPinned()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var parameterType = typeof(Parameter);
        var assembly = Assembly.GetAssembly(parameterType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var parameterConstructor = parameterType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        var elementId = (long)Constants.Parameter;
        var aDocument = getADocumentMethod.Invoke(_document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var parameter = (Parameter)parameterConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return parameter;
    }

    [Benchmark]
    public Parameter CachedPinned()
    {
        var elementId = (long)Constants.Parameter;
        var aDocument = GetADocumentMethod.Invoke(_document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var parameter = (Parameter)ParameterConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return parameter;
    }
#if NET
    [Benchmark]
    public unsafe Parameter ReflectionUnsafe()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var parameterType = typeof(Parameter);
        var assembly = Assembly.GetAssembly(parameterType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var parameterConstructor = parameterType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        var elementId = (long)Constants.Parameter;
        var aDocument = getADocumentMethod.Invoke(_document, null);
        var parameter = (Parameter)parameterConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return parameter;
    }

    [Benchmark(Baseline = true)]
    public unsafe Parameter CachedUnsafe()
    {
        var elementId = (long)Constants.Parameter;
        var aDocument = GetADocumentMethod.Invoke(_document, null);
        var parameter = (Parameter)ParameterConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return parameter;
    }
#endif
}
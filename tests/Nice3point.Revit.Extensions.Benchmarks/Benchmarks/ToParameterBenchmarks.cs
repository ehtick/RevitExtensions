using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Nice3point.Revit.Extensions.Benchmarks.Abstractions;
#if NET
using System.Runtime.CompilerServices;
#endif

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

/// <summary>
///     Provides test values for benchmarks, avoiding early JIT resolution of Revit API structs.
/// </summary>
file static class Arguments
{
    public const BuiltInParameter Parameter = BuiltInParameter.ALL_MODEL_DESCRIPTION;
}

public class ToParameterBenchmark : RevitDocumentBenchmark
{
    private static readonly Assembly Assembly = Assembly.GetAssembly(typeof(Parameter))!;
    private static readonly Type ADocumentType = Assembly.GetType("ADocument")!;
    private static readonly Type ElementIdType = Assembly.GetType("ElementId")!;
    private static readonly MethodInfo GetADocumentMethod = typeof(Document).GetMethod("getADocument", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
    private static readonly ConstructorInfo ParameterConstructor = typeof(Parameter).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, [ADocumentType.MakePointerType(), ElementIdType.MakePointerType()], null)!;

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

        var elementId = (long)Arguments.Parameter;
        var aDocument = getADocumentMethod.Invoke(Document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var parameter = (Parameter)parameterConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return parameter;
    }

    [Benchmark]
    public Parameter CachedPinned()
    {
        var elementId = (long)Arguments.Parameter;
        var aDocument = GetADocumentMethod.Invoke(Document, null);

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

        var elementId = (long)Arguments.Parameter;
        var aDocument = getADocumentMethod.Invoke(Document, null);
        var parameter = (Parameter)parameterConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return parameter;
    }

    [Benchmark(Baseline = true)]
    public unsafe Parameter CachedUnsafe()
    {
        var elementId = (long)Arguments.Parameter;
        var aDocument = GetADocumentMethod.Invoke(Document, null);
        var parameter = (Parameter)ParameterConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return parameter;
    }
#endif
}
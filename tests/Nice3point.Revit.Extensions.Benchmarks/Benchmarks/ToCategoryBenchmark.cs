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
    public const BuiltInCategory Category = BuiltInCategory.OST_Walls;
}

public class ToCategoryBenchmark : RevitDocumentBenchmark
{
    private static readonly Assembly Assembly = Assembly.GetAssembly(typeof(Category))!;
    private static readonly Type ADocumentType = Assembly.GetType("ADocument")!;
    private static readonly Type ElementIdType = Assembly.GetType("ElementId")!;
    private static readonly MethodInfo GetADocumentMethod = typeof(Document).GetMethod("getADocument", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
    private static readonly ConstructorInfo CategoryConstructor = typeof(Category).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, [ADocumentType.MakePointerType(), ElementIdType.MakePointerType()], null)!;

    [Benchmark]
    public Category ReflectionPinned()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var categoryType = typeof(Category);
        var assembly = Assembly.GetAssembly(categoryType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var categoryConstructor = categoryType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        var elementId = (long)Arguments.Category;
        var aDocument = getADocumentMethod.Invoke(Document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var category = (Category)categoryConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return category;
    }

    [Benchmark]
    public Category CachedPinned()
    {
        var elementId = (long)Arguments.Category;
        var aDocument = GetADocumentMethod.Invoke(Document, null);

        var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
        var category = (Category)CategoryConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
        handle.Free();

        return category;
    }
#if NET

    [Benchmark]
    public unsafe Category ReflectionUnsafe()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        var documentType = typeof(Document);
        var categoryType = typeof(Category);
        var assembly = Assembly.GetAssembly(categoryType)!;
        var aDocumentType = assembly.GetType("ADocument")!;
        var elementIdType = assembly.GetType("ElementId")!;
        var getADocumentMethod = documentType.GetMethod("getADocument", bindingFlags)!;
        var categoryConstructor = categoryType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

        var elementId = (long)Arguments.Category;
        var aDocument = getADocumentMethod.Invoke(Document, null);
        var category = (Category)categoryConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return category;
    }

    [Benchmark(Baseline = true)]
    public unsafe Category CachedUnsafe()
    {
        var elementId = (long)Arguments.Category;
        var aDocument = GetADocumentMethod.Invoke(Document, null);
        var category = (Category)CategoryConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

        return category;
    }
#endif
}
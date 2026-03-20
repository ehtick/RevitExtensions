#if REVIT2025_OR_GREATER
using Autodesk.Revit.DB.Structure;
using BenchmarkDotNet.Attributes;
using Nice3point.Revit.Extensions.Benchmarks.Abstractions;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

[DocumentSource("Structural")]
public class RebarSpliceTypeUtilsBenchmark : RevitDocumentBenchmark
{
    [Benchmark]
    public IList<ElementId> GetAllRebarCrankTypes()
    {
        return RebarSpliceTypeUtils.GetAllRebarSpliceTypes(Document);
    }

    [Benchmark]
    public ICollection<ElementId> FilteredElementCollector()
    {
        return new FilteredElementCollector(Document)
            .OfCategory(BuiltInCategory.OST_RebarSpliceType)
            .ToElementIds();
    }
}
#endif
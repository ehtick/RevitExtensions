#if REVIT2026_OR_GREATER
using Autodesk.Revit.DB.Structure;
using BenchmarkDotNet.Attributes;
using Nice3point.Revit.Extensions.Benchmarks.Abstractions;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

[DocumentSource("Structural")]
public class RebarCrankTypeUtilsBenchmark : RevitDocumentBenchmark
{
    [Benchmark]
    public IList<ElementId> GetAllRebarCrankTypes()
    {
        return RebarCrankTypeUtils.GetAllRebarCrankTypes(Document);
    }

    [Benchmark]
    public ICollection<ElementId> FilteredElementCollector()
    {
        return new FilteredElementCollector(Document)
            .OfCategory(BuiltInCategory.OST_RebarCrankType)
            .ToElementIds();
    }
}
#endif
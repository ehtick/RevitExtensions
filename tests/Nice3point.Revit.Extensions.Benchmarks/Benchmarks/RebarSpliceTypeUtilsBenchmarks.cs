using Autodesk.Revit.DB.Structure;
using BenchmarkDotNet.Attributes;
using Nice3point.BenchmarkDotNet.Revit;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

public class RebarSpliceTypeUtilsBenchmarks : RevitApiBenchmark
{
    private Document _document;

    protected override void OnGlobalSetup()
    {
        var samplesPath = $@"C:\Program Files\Autodesk\Revit {Application.VersionNumber}\Samples";

        var targetPath = Directory
            .EnumerateFiles(samplesPath, "*.rvt")
            .FirstOrDefault(path => path.Contains("structural", StringComparison.OrdinalIgnoreCase));

        if (targetPath == null) throw new InvalidOperationException("Cannot find structural file to run benchmark");

        _document = Application.OpenDocumentFile(targetPath);
    }

    protected override void OnGlobalCleanup()
    {
        _document?.Close(false);
    }
#if REVIT2025_OR_GREATER

    [Benchmark]
    public IList<ElementId> GetAllRebarCrankTypes()
    {
        return RebarSpliceTypeUtils.GetAllRebarSpliceTypes(_document);
    }

    [Benchmark]
    public ICollection<ElementId> FilteredElementCollector()
    {
        return new FilteredElementCollector(_document)
            .OfCategory(BuiltInCategory.OST_RebarSpliceType)
            .ToElementIds();
    }
#endif
}
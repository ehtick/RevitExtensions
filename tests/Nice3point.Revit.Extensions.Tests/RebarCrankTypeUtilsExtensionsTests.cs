#if REVIT2026_OR_GREATER
using Nice3point.Revit.Extensions.Structure;
using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class RebarCrankTypeUtilsExtensionsTests : RevitApiTest
{
    private static readonly string SamplesPath = $@"C:\Program Files\Autodesk\Revit {Application.VersionNumber}\Samples";

    [Before(Class)]
    public static void ValidateSamples()
    {
        if (!Directory.Exists(SamplesPath))
        {
            Skip.Test($"Samples folder not found at {SamplesPath}");
            return;
        }

        if (!Directory.EnumerateFiles(SamplesPath, "*.rvt").Any())
        {
            Skip.Test($"No .rfa files found in {SamplesPath}");
        }
    }

    public static string? GetSampleFile()
    {
        if (!Directory.Exists(SamplesPath))
        {
            return null;
        }

        return Directory.EnumerateFiles(SamplesPath, "*.rvt")
            .Select(path => new FileInfo(path))
            .OrderBy(info => info.Length)
            .Select(info => info.FullName)
            .FirstOrDefault();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    [MethodDataSource(nameof(GetSampleFile))]
    public async Task Distance_ParallelLines_ReturnsCorrectDistance(string filePath)
    {
        const string typeName = "Rebar type";
        Document? document = null;

        try
        {
            document = Application.OpenDocumentFile(filePath);

            using var transaction = new Transaction(document);
            transaction.Start("Create rebar type");
            var elementType = document!.Create.NewRebarCrankType(typeName);
            transaction.Commit();

            await Assert.That(elementType).IsNotNull();
            await Assert.That(elementType.Name).IsEqualTo(typeName);
        }
        finally
        {
            document?.Close(false);
        }
    }
}
#endif
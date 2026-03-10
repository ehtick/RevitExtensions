using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ApplicationExtensionsTests : RevitApiTest
{
    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task AsControlledApplication_ValidApplication_ReturnsNotNull()
    {
        var controlledApplication = Application.AsControlledApplication();

        await Assert.That(controlledApplication).IsNotNull();
        await Assert.That(controlledApplication.VersionBuild).IsNotNullOrEmpty();
    }
}
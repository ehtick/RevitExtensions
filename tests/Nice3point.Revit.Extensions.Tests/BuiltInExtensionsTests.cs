using Nice3point.TUnit.Revit;
using Nice3point.TUnit.Revit.Executors;
using TUnit.Core.Executors;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class BuiltInExtensionsTests : RevitApiTest
{
    private static Document _document = null!;

    [Before(Class)]
    [HookExecutor<RevitThreadExecutor>]
    public static void Setup()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);
    }

    [After(Class)]
    [HookExecutor<RevitThreadExecutor>]
    public static void Cleanup()
    {
        _document.Close(false);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToParameter_ValidBuiltInParameter_ReturnsParameter()
    {
        var parameter = BuiltInParameter.ELEM_CATEGORY_PARAM.ToParameter(_document);

        await Assert.That(parameter).IsNotNull();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToParameter_ValidBuiltInParameter_ReturnsCorrectDefinitionName()
    {
        var parameter = BuiltInParameter.ELEM_CATEGORY_PARAM.ToParameter(_document);

        await Assert.That(parameter.Definition.Name).IsNotNull().And.IsNotEmpty();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToParameter_DifferentParameters_ReturnDifferentDefinitions()
    {
        var parameter1 = BuiltInParameter.ELEM_CATEGORY_PARAM.ToParameter(_document);
        var parameter2 = BuiltInParameter.ELEM_TYPE_PARAM.ToParameter(_document);

        await Assert.That(parameter1.Definition.Name).IsNotEqualTo(parameter2.Definition.Name);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToCategory_ValidBuiltInCategory_ReturnsCategory()
    {
        var category = BuiltInCategory.OST_Walls.ToCategory(_document);

        await Assert.That(category).IsNotNull();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToCategory_ValidBuiltInCategory_ReturnsCorrectName()
    {
        var category = BuiltInCategory.OST_Walls.ToCategory(_document);

        await Assert.That(category.Name).IsNotNull().And.IsNotEmpty();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToCategory_ValidBuiltInCategory_ReturnsMatchingId()
    {
        var category = BuiltInCategory.OST_Walls.ToCategory(_document);

        await Assert.That(category.Id.AreEquals(BuiltInCategory.OST_Walls)).IsTrue();
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToCategory_DifferentCategories_ReturnDifferentNames()
    {
        var walls = BuiltInCategory.OST_Walls.ToCategory(_document);
        var doors = BuiltInCategory.OST_Doors.ToCategory(_document);

        await Assert.That(walls.Name).IsNotEqualTo(doors.Name);
    }

    [Test]
    [TestExecutor<RevitThreadExecutor>]
    public async Task ToCategory_DifferentCategories_ReturnDifferentIds()
    {
        var walls = BuiltInCategory.OST_Walls.ToCategory(_document);
        var doors = BuiltInCategory.OST_Doors.ToCategory(_document);

        await Assert.That(walls.Id).IsNotEqualTo(doors.Id);
    }
}
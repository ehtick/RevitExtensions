using Nice3point.Revit.Extensions.Tests.Abstractions;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ElementIdExtensionsTests : RevitFamilySampleTest
{
    [Test]
    public async Task IsCategory_BuiltInCategory_MatchingCategory_ReturnsTrue()
    {
        var wallCategoryId = new ElementId(BuiltInCategory.OST_Walls);

        var result = wallCategoryId.IsCategory(BuiltInCategory.OST_Walls);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsCategory_BuiltInCategory_DifferentCategory_ReturnsFalse()
    {
        var wallCategoryId = new ElementId(BuiltInCategory.OST_Walls);

        var result = wallCategoryId.IsCategory(BuiltInCategory.OST_Doors);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsCategory_BuiltInParameter_MatchingParameter_ReturnsTrue()
    {
        var parameterId = new ElementId(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        var result = parameterId.IsParameter(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsCategory_BuiltInParameter_DifferentParameter_ReturnsFalse()
    {
        var parameterId = new ElementId(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);

        var result = parameterId.IsParameter(BuiltInParameter.WALL_TOP_OFFSET);

        await Assert.That(result).IsFalse();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElement_ValidElementId_ReturnsElement(string path)
    {
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsNotElementType()
            .ToElementIds();

        var firstId = elementIds.FirstOrDefault();
        if (firstId is null)
        {
            Skip.Test("No elements found in document");
            return;
        }

        var element = firstId.ToElement(document);

        await Assert.That(element).IsNotNull();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElement_InvalidElementId_ReturnsNull(string path)
    {
        var document = FamilyDocuments[path];

#if REVIT2024_OR_GREATER
        var invalidId = new ElementId(999999999L);
#else
        var invalidId = new ElementId(999999999);
#endif

        var element = invalidId.ToElement(document);

        await Assert.That(element).IsNull();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElementGeneric_ValidElementId_ReturnsTypedElement(string path)
    {
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsElementType()
            .ToElementIds();

        var firstId = elementIds.FirstOrDefault();
        if (firstId is null)
        {
            Skip.Test("No element types found in document");
            return;
        }

        var elementType = firstId.ToElement<ElementType>(document);

        using (Assert.Multiple())
        {
            await Assert.That(elementType).IsNotNull();
            await Assert.That(elementType).IsAssignableTo<ElementType>();
        }
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElements_MultipleElementIds_ReturnsAllElements(string path)
    {
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsNotElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        var elements = elementIds.ToElements(document);

        await Assert.That(elements.Count).IsEqualTo(elementIds.Count);
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElements_EmptyCollection_ReturnsEmptyList(string path)
    {
        var document = FamilyDocuments[path];
        var elementIds = new List<ElementId>();
        var elements = elementIds.ToElements(document);

        await Assert.That(elements).IsEmpty();
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToElementsGeneric_MultipleElementIds_ReturnsTypedElements(string path)
    {
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        var elementTypes = elementIds.ToElements<ElementType>(document);

        using (Assert.Multiple())
        {
            await Assert.That(elementTypes.Count).IsEqualTo(elementIds.Count);
            await Assert.That(elementTypes).All().Satisfy(source => source.IsAssignableTo<ElementType>());
        }
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToOrderedElements_MultipleElementIds_PreservesOrder(string path)
    {
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsNotElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        var orderedElements = elementIds.ToOrderedElements(document);

        using (Assert.Multiple())
        {
            await Assert.That(orderedElements.Count).IsEqualTo(elementIds.Count);
            for (var i = 0; i < elementIds.Count; i++)
            {
                await Assert.That(orderedElements[i].Id).IsEqualTo(elementIds[i]);
            }
        }
    }

    [Test]
    [MethodDataSource(nameof(RevitFamilies))]
    public async Task ToOrderedElementsGeneric_MultipleElementIds_PreservesOrderAndType(string path)
    {
        var document = FamilyDocuments[path];
        var elementIds = new FilteredElementCollector(document)
            .WhereElementIsElementType()
            .ToElementIds()
            .Take(5)
            .ToList();

        var orderedElementTypes = elementIds.ToOrderedElements<ElementType>(document);

        using (Assert.Multiple())
        {
            await Assert.That(orderedElementTypes.Count).IsEqualTo(elementIds.Count);
            for (var i = 0; i < elementIds.Count; i++)
            {
                await Assert.That(orderedElementTypes[i].Id).IsEqualTo(elementIds[i]);
            }

            await Assert.That(orderedElementTypes).All().Satisfy(source => source.IsAssignableTo<ElementType>());
        }
    }
}
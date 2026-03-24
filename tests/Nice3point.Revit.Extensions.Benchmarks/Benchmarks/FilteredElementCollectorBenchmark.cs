using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Nice3point.BenchmarkDotNet.Revit;

namespace Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

// BenchmarkDotNet v0.15.8, Windows 11 (10.0.22631.6199/23H2/2023Update/SunValley3)
// AMD Ryzen 5 5600 3.50GHz, 1 CPU, 12 logical and 6 physical cores
// .NET SDK 10.0.201
//   [Host]  : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v3
//   LongRun : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v3
//
// Job=LongRun  BuildConfiguration=Release.R26  IterationCount=100  
// LaunchCount=3  WarmupCount=15  
//
// | Method                                    | Mean         | Error     | StdDev     | Median       | Allocated |
// |------------------------------------------ |-------------:|----------:|-----------:|-------------:|----------:|
// | OfClass<T>()                              |   297.467 us | 0.8463 us |  4.2967 us |   296.717 us |     968 B |
// | OfCategory(BuiltInCategory)               |   279.339 us | 0.8071 us |  4.1631 us |   278.527 us |    1208 B |
// | OfClass<T>().OfCategory(BuiltInCategory)  |   301.139 us | 1.1096 us |  5.7532 us |   300.211 us |    1096 B |
// | OfCategory(BuiltInCategory).OfClass<T>()  |   297.676 us | 0.7585 us |  3.9328 us |   296.923 us |    1096 B |
// |                                           |              |           |            |              |           |
// | AllInstances                              |   304.595 us | 1.1358 us |  5.8487 us |   303.412 us |     968 B |
// | ViewInstances                             |    14.121 us | 0.0284 us |  0.1462 us |    14.087 us |    1096 B |
// |                                           |              |           |            |              |           |
// | GetElementCount()                         |   151.927 us | 0.3282 us |  1.6958 us |   151.737 us |     160 B |
// | ToElementIds().Count                      |   184.771 us | 0.3011 us |  1.5314 us |   184.753 us |   18168 B |
// | Enumerable.Count()                        |   136.529 us | 0.3897 us |  2.0136 us |   136.457 us |     216 B |
// |                                           |              |           |            |              |           |
// | OfClass<T>().Instances()                  |   303.470 us | 0.9673 us |  4.9983 us |   302.739 us |     968 B |
// | Instances().OfClass<T>()                  |   305.126 us | 1.0905 us |  5.6638 us |   304.919 us |     968 B |
// |                                           |              |           |            |              |           |
// | FirstElement()                            |     2.128 us | 0.0093 us |  0.0483 us |     2.120 us |     240 B |
// | Enumerable.FirstOrDefault()               |     2.611 us | 0.0151 us |  0.0782 us |     2.598 us |     296 B |
// |                                           |              |           |            |              |           |
// | FirstElementId().Value>0                  |     1.862 us | 0.0076 us |  0.0395 us |     1.862 us |     208 B |
// | Enumerable.Any()                          |     2.138 us | 0.0116 us |  0.0598 us |     2.132 us |     216 B |
// |                                           |              |           |            |              |           |
// | ToElementIds()                            |   189.291 us | 0.4413 us |  2.2366 us |   188.902 us |   18168 B |
// | ToElements()                              |   288.256 us | 0.8150 us |  4.2041 us |   288.092 us |   27256 B |
// | Cast<T>().ToList()                        |   298.507 us | 0.5834 us |  2.9565 us |   298.241 us |   31136 B |
// | OfType<T>().ToList()                      |   303.560 us | 1.3730 us |  6.9457 us |   302.739 us |   31136 B |
// |                                           |              |           |            |              |           |
// | OfCategories()                            |   470.244 us | 1.1825 us |  6.0360 us |   468.901 us |    1704 B |
// | OfClasses()                               |   552.679 us | 1.3446 us |  6.9601 us |   552.087 us |    1904 B |
// | OfClass().UnionWith().OfClass()           | 1,482.382 us | 2.2758 us | 11.3879 us | 1,480.994 us |    1800 B |
// |                                           |              |           |            |              |           |
// | WhereParameter().NotEquals(ElementId)     |   360.371 us | 0.8299 us |  4.2061 us |   360.502 us |    1096 B |
// | Enumerable.Where(AsElementId)             |   329.343 us | 1.2754 us |  6.6018 us |   328.282 us |    2136 B |
// | WhereParameter().IsGreaterThan(double)    |   320.781 us | 0.6834 us |  3.4634 us |   319.996 us |    1608 B |
// | Enumerable.Where(AsDouble)                |   336.169 us | 1.1254 us |  5.8154 us |   335.881 us |    1584 B |
// | WhereParameter().Contains(string)         |   148.230 us | 0.3802 us |  1.9268 us |   148.178 us |    1240 B |
// | Enumerable.Where(Name.Contains)           |   147.366 us | 0.3899 us |  1.9794 us |   147.219 us |    1120 B |
// |                                           |              |           |            |              |           |
// | OnLevel()                                 |   483.299 us | 0.7832 us |  3.8384 us |   482.674 us |    1080 B |
// | OfCategory(BuiltInCategory).OnLevel()     |   626.395 us | 1.3714 us |  6.8875 us |   625.510 us |    1304 B |
// | OnLevel().OfCategory(BuiltInCategory)     |   626.440 us | 0.9650 us |  4.9518 us |   626.281 us |    1304 B |

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class FilteredElementCollectorBenchmark : RevitApiBenchmark
{
    private Document _document = null!;
    private Level _groundFloor = null!;
    private Level _firstFloor = null!;
    private View _floorPlan = null!;

    protected override void OnGlobalSetup()
    {
        _document = Application.NewProjectDocument(UnitSystem.Metric);

        using (RevitApiContext.BeginFailureSuppressionScope())
        {
            using var transaction = new Transaction(_document, "Seed model");
            transaction.Start();

            CreateLevels();
            CreateWalls();
            CreateGrids();
            CreateViews();

            transaction.Commit();
        }
    }

    protected override void OnGlobalCleanup()
    {
        _document?.Close(false);
    }

    [Benchmark(Description = "FirstElement()")]
    [BenchmarkCategory("FirstElement")]
    public Element FirstElement()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .FirstElement();
    }

    [Benchmark(Description = "Enumerable.FirstOrDefault()")]
    [BenchmarkCategory("FirstElement")]
    public Element LinqFirstOrDefault()
    {
        return Enumerable.FirstOrDefault(
            new FilteredElementCollector(_document)
                .WhereElementIsElementType());
    }

    [Benchmark(Description = "GetElementCount()")]
    [BenchmarkCategory("ElementCount")]
    public int GetElementCount()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .GetElementCount();
    }

    [Benchmark(Description = "ToElementIds().Count")]
    [BenchmarkCategory("ElementCount")]
    public int ToElementIdsCount()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .ToElementIds()
            .Count;
    }

    [Benchmark(Description = "Enumerable.Count()")]
    [BenchmarkCategory("ElementCount")]
    public int LinqCount()
    {
        return Enumerable.Count(
            new FilteredElementCollector(_document)
                .WhereElementIsElementType());
    }

    [Benchmark(Description = "FirstElementId().Value>0")]
    [BenchmarkCategory("HasElements")]
    public bool Any()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
#if REVIT2024_OR_GREATER
            .FirstElementId().Value > 0;
#else
            .FirstElementId().IntegerValue > 0;
#endif
    }

    [Benchmark(Description = "Enumerable.Any()")]
    [BenchmarkCategory("HasElements")]
    public bool LinqAny()
    {
        return Enumerable.Any(
            new FilteredElementCollector(_document)
                .WhereElementIsElementType());
    }

    [Benchmark(Description = "ToElementIds()")]
    [BenchmarkCategory("Materialization")]
    public ICollection<ElementId> ToElementIds()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .ToElementIds();
    }

    [Benchmark(Description = "ToElements()")]
    [BenchmarkCategory("Materialization")]
    public IList<Element> ToElements()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .ToElements();
    }

    [Benchmark(Description = "Cast<T>().ToList()")]
    [BenchmarkCategory("Materialization")]
    public List<ElementType> CastToList()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .Cast<ElementType>()
            .ToList();
    }

    [Benchmark(Description = "OfType<T>().ToList()")]
    [BenchmarkCategory("Materialization")]
    public List<ElementType> OfTypeToList()
    {
        return new FilteredElementCollector(_document)
            .WhereElementIsElementType()
            .OfType<ElementType>()
            .ToList();
    }

    [Benchmark(Description = "OfClass<T>().Instances()")]
    [BenchmarkCategory("FilterOrder")]
    public IList<Element> ClassThenInstances()
    {
        return _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .ToElements();
    }

    [Benchmark(Description = "Instances().OfClass<T>()")]
    [BenchmarkCategory("FilterOrder")]
    public IList<Element> InstancesThenClass()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .ToElements();
    }

    [Benchmark(Description = "OfClass<T>()")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> ClassFilter()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .ToElements();
    }

    [Benchmark(Description = "OfCategory(BuiltInCategory)")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> CategoryFilter()
    {
        return new FilteredElementCollector(_document)
            .Instances()
            .OfCategory(BuiltInCategory.OST_Walls)
            .ToElements();
    }

    [Benchmark(Description = "OfClass<T>().OfCategory(BuiltInCategory)")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> ClassThenCategory()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .OfCategory(BuiltInCategory.OST_Walls)
            .ToElements();
    }

    [Benchmark(Description = "OfCategory(BuiltInCategory).OfClass<T>()")]
    [BenchmarkCategory("ClassVsCategory")]
    public IList<Element> CategoryThenClass()
    {
        return new FilteredElementCollector(_document)
            .Instances()
            .OfCategory(BuiltInCategory.OST_Walls)
            .OfClass<Wall>()
            .ToElements();
    }

    [Benchmark(Description = "WhereParameter().NotEquals(ElementId)")]
    [BenchmarkCategory("ParameterFilter")]
    public IList<Element> ParameterNotEqualsElementId()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .WhereParameter(BuiltInParameter.WALL_BASE_CONSTRAINT).NotEquals(_groundFloor.Id)
            .ToElements();
    }

    [Benchmark(Description = "Enumerable.Where(AsElementId)")]
    [BenchmarkCategory("ParameterFilter")]
    public List<Element> LinqWhereElementId()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .Where(element => element.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT)?.AsElementId() != _groundFloor.Id)
            .ToList();
    }

    [Benchmark(Description = "WhereParameter().IsGreaterThan(double)")]
    [BenchmarkCategory("ParameterFilter")]
    public IList<Element> ParameterGreaterThanDouble()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).IsGreaterThan(0d, 1e-9)
            .ToElements();
    }

    [Benchmark(Description = "Enumerable.Where(AsDouble)")]
    [BenchmarkCategory("ParameterFilter")]
    public List<Element> LinqWhereDouble()
    {
        return _document.CollectElements()
            .Instances()
            .OfClass<Wall>()
            .Where(element => element.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM)?.AsDouble() > 0d)
            .ToList();
    }

    [Benchmark(Description = "WhereParameter().Contains(string)")]
    [BenchmarkCategory("ParameterFilter")]
    public IList<Element> ParameterContainsString()
    {
        return _document.CollectElements()
            .Types()
            .OfClass<WallType>()
            .WhereParameter(BuiltInParameter.ALL_MODEL_TYPE_NAME).Contains("Wall")
            .ToElements();
    }

    [Benchmark(Description = "Enumerable.Where(Name.Contains)")]
    [BenchmarkCategory("ParameterFilter")]
    public List<Element> LinqWhereNameContains()
    {
        return _document.CollectElements()
            .Types()
            .OfClass<WallType>()
            .Where(element => element.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_NAME).AsString().Contains("Wall"))
            .ToList();
    }

    [Benchmark(Description = "OnLevel()")]
    [BenchmarkCategory("QuickVsSlowFilter")]
    public IList<Element> LevelFilterAlone()
    {
        return _document.CollectElements()
            .Instances()
            .OnLevel(_groundFloor)
            .ToElements();
    }

    [Benchmark(Description = "OfCategory(BuiltInCategory).OnLevel()")]
    [BenchmarkCategory("QuickVsSlowFilter")]
    public IList<Element> CategoryThenLevel()
    {
        return _document.CollectElements()
            .Instances()
            .OfCategories(BuiltInCategory.OST_Walls)
            .OnLevel(_groundFloor)
            .ToElements();
    }

    [Benchmark(Description = "OnLevel().OfCategory(BuiltInCategory)")]
    [BenchmarkCategory("QuickVsSlowFilter")]
    public IList<Element> LevelThenCategory()
    {
        return _document.CollectElements()
            .Instances()
            .OnLevel(_groundFloor)
            .OfCategories(BuiltInCategory.OST_Walls)
            .ToElements();
    }

    [Benchmark(Description = "AllInstances")]
    [BenchmarkCategory("CollectorScope")]
    public IList<Element> AllInstances()
    {
        return _document.CollectElements()
            .OfClass<Wall>()
            .Instances()
            .ToElements();
    }

    [Benchmark(Description = "ViewInstances")]
    [BenchmarkCategory("CollectorScope")]
    public IList<Element> ViewInstances()
    {
        return new FilteredElementCollector(_document, _floorPlan.Id)
            .OfClass<Wall>()
            .Instances()
            .ToElements();
    }

    [Benchmark(Description = "OfCategories()")]
    [BenchmarkCategory("MultiFilter")]
    public IList<Element> MultiCategoryFilter()
    {
        return _document.CollectElements()
            .Instances()
            .OfCategories(BuiltInCategory.OST_Walls, BuiltInCategory.OST_Grids, BuiltInCategory.OST_Levels)
            .ToElements();
    }

    [Benchmark(Description = "OfClasses()")]
    [BenchmarkCategory("MultiFilter")]
    public IList<Element> MultiClassFilter()
    {
        return _document.CollectElements()
            .OfClasses(typeof(Wall), typeof(Grid), typeof(Level))
            .ToElements();
    }

    [Benchmark(Description = "OfClass().UnionWith().OfClass()")]
    [BenchmarkCategory("MultiFilter")]
    public ICollection<ElementId> UnionWithFilter()
    {
        var walls = new FilteredElementCollector(_document).OfClass(typeof(Wall));
        var grids = new FilteredElementCollector(_document).OfClass(typeof(Grid));
        var levels = new FilteredElementCollector(_document).OfClass(typeof(Level));

        return walls.UnionWith(grids).UnionWith(levels).ToElementIds();
    }

    private void CreateLevels()
    {
        _groundFloor = Level.Create(_document, 0);
        _groundFloor.Name = "Ground Floor";

        _firstFloor = Level.Create(_document, 3);
        _firstFloor.Name = "First Floor";
    }

    private void CreateWalls()
    {
        Wall.Create(_document, Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(10, 0, 0), new XYZ(10, 6, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(10, 6, 0), new XYZ(0, 6, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(0, 6, 0), new XYZ(0, 0, 0)), _groundFloor.Id, false);
        Wall.Create(_document, Line.CreateBound(new XYZ(5, -1, 0), new XYZ(5, 7, 0)), _groundFloor.Id, false);

        Wall.Create(_document, Line.CreateBound(new XYZ(0, 0, 3), new XYZ(10, 0, 3)), _firstFloor.Id, false);
    }

    private void CreateGrids()
    {
        Grid.Create(_document, Line.CreateBound(new XYZ(0, -2, 0), new XYZ(0, 8, 0)));
        Grid.Create(_document, Line.CreateBound(new XYZ(5, -2, 0), new XYZ(5, 8, 0)));
        Grid.Create(_document, Line.CreateBound(new XYZ(10, -2, 0), new XYZ(10, 8, 0)));
    }

    private void CreateViews()
    {
        var viewFamilyType = new FilteredElementCollector(_document)
            .OfClass(typeof(ViewFamilyType))
            .Cast<ViewFamilyType>()
            .First(type => type.ViewFamily == ViewFamily.FloorPlan);

        _floorPlan = ViewPlan.Create(_document, viewFamilyType.Id, _groundFloor.Id);
    }
}
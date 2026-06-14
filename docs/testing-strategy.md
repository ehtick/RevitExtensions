# Testing Strategy

Tests cover the library's **custom logic**, not the Revit API itself. Tests run inside a real Revit process against real Revit objects — never against mocks.

## What to Test

* **Custom logic only.** Test extensions that add behavior beyond a direct API call.
    * Test: `BoundingBox.Contains()` — custom containment logic.
    * Test: `Line.Distance()` — custom distance calculation.
    * Test: `ToOrderedElements()` — custom ordering logic.
    * Skip: `element.Copy()` — thin wrapper over `ElementTransformUtils.CopyElement`.
    * Skip: `element.JoinGeometry()` — thin wrapper over `JoinGeometryUtils.JoinGeometry`.
* **Edge cases:** null inputs, empty collections, boundary values.
* **No UI tests:** skip Ribbon, ContextMenu, and `UIApplication` extensions.

## Framework

* **TUnit** for test declarations and assertions, with **Shouldly** for readable assertions.
* **Nice3point.TUnit.Revit** for Revit API access. Tests execute on the Revit thread.
* The assembly applies `[TestExecutor<RevitThreadExecutor>]` globally (`TestsConfiguration.cs`); individual Revit-thread hooks use `[HookExecutor<RevitThreadExecutor>]`.
* Tests live in `tests/Nice3point.Revit.Extensions.Tests`.

## Test Context

* Inherit from a shared sample base under `tests/Nice3point.Revit.Extensions.Tests/Abstractions`:
    * `RevitModelSampleTest` — opens `.rvt` sample models.
    * `RevitFamilySampleTest` — opens `.rfa` sample families.
    * `RevitApiReportTest` — report-style API coverage.
* Sample files are resolved from the installed Revit `Samples` folder via `RevitEnvironment.MajorVersion`. Each document is copied to a temp path, opened under a failure-suppression scope, and closed/deleted in the matching teardown hook.
* When the Revit `Samples` folder is missing, the sample arrays are empty — guard tests so they skip cleanly rather than fail.

## Sample-Driven Test Pattern

Drive tests across the available sample files so coverage reflects real models:

```csharp
public class MyExtensionTests : RevitModelSampleTest
{
    [Test]
    public async Task MyExtension_ValidModel_ReturnsExpectedResult()
    {
        foreach (var document in ModelDocuments.Values)
        {
            var result = document.SomeExtension();
            await Assert.That(result).IsNotNull();
        }
    }
}
```

## Version Coverage

* Tests build per Revit configuration (`Debug.RNN`). Prefer the latest supported debug configuration unless the change is version-specific.
* When changing version-specific behavior, run or document coverage for each affected `Debug.RNN` configuration the project declares.

## Benchmarks

* Performance-sensitive changes get a BenchmarkDotNet benchmark in `tests/Nice3point.Revit.Extensions.Benchmarks` (Nice3point.BenchmarkDotNet.Revit), with a shared Revit document fixture under `Abstractions`.

# Project Structure

Nice3point.Revit.Extensions is a single-purpose library of fluent extension methods over the Revit API. The solution separates the shipped library from tests and benchmarks. Keep code in the project that owns the runtime responsibility.

## Solution Groups

* **`/source`**: the shipped NuGet package.
    * `source/Nice3point.Revit.Extensions`: all public extension methods over Revit API types. This is the only packable project.
* **`/tests`**: Revit-aware verification projects.
    * `tests/Nice3point.Revit.Extensions.Tests`: TUnit and Nice3point.TUnit.Revit tests that execute inside the Revit process.
    * `tests/Nice3point.Revit.Extensions.Benchmarks`: BenchmarkDotNet projects (Nice3point.BenchmarkDotNet.Revit) for measuring extension performance.
* **`/build`**: ModularPipelines build system, packaging, changelog, and NuGet publishing tasks.
* **Root level**:
    * Configuration: `Directory.Build.props`, `Directory.Packages.props`, `global.json`, `renovate.json`.
    * Documentation: `Readme.md`, `Changelog.md`, `Contributing.md`.
    * Agent guidelines: `CLAUDE.md`, `AGENTS.md`, `.junie/AGENTS.md`, `docs/`.
    * CI/CD: `.github/workflows`.

## Source Layout

Extension classes are grouped by the Revit API surface they wrap. One file per Revit type or utility class, named `<Type>Extensions.cs`.

* `source/Nice3point.Revit.Extensions/*.cs`: extensions over core Revit API types (`Element`, `ElementId`, `Document`, `Parameter`, `Category`, `Color`, geometry, etc.).
* `source/Nice3point.Revit.Extensions/UtilsExtensions`: fluent wrappers over Revit `*Utils` static classes (e.g. `JoinGeometryUtils`, `ElementTransformUtils`, `SolidUtils`). One file per Utils class.
* `source/Nice3point.Revit.Extensions/ManagersExtensions`:  fluent wrappers over Revit `*Manager` types (e.g. `GlobalParametersManager`, `SpatialFieldManager`).
* `source/Nice3point.Revit.Extensions/UIFrameworkExtensions`: Ribbon and UI Framework helpers (UI-only, not unit-tested).
* `source/Nice3point.Revit.Extensions/Internal`: non-public helpers (unsafe accessors, ribbon panel infrastructure, context menu creation, color format utilities). Not part of the public API.

## Change Placement

* Put extensions over a core Revit type in the matching root-level `<Type>Extensions.cs`, or create one if it does not exist.
* Put wrappers over a Revit `*Utils` class in `UtilsExtensions/<UtilsClass>Extensions.cs`.
* Put wrappers over a Revit `*Manager` class in `ManagersExtensions/<Manager>Extensions.cs`.
* Put Ribbon/UI helpers in `UIFrameworkExtensions`.
* Put private implementation details, reflection accessors, and format helpers in `Internal`; never expose them publicly.
* Put unit coverage for custom logic in `tests/Nice3point.Revit.Extensions.Tests`.
* Put performance measurements in `tests/Nice3point.Revit.Extensions.Benchmarks`.

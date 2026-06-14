# Nice3point.Revit.Extensions Agent Instructions

Nice3point.Revit.Extensions is a public NuGet library of fluent extension methods over the Revit API. `source/Nice3point.Revit.Extensions` is the only shipped project; it wraps Revit API types, utilities, and managers behind a type-safe, chainable surface and adds ergonomics, never new behavior.

## Non-Negotiables

* Never break an existing public API. Deprecate with `[Obsolete]` + `CodeTemplate`; keep the old method functional.
* Use C# 14 `extension(Type) { }` blocks. Mutation methods return the source object for chaining.
* Wrap the Revit API directly; do not reimplement it. Mark read-only methods `[Pure]`, mark public classes `[PublicAPI]`.
* When a method works through `element.Document`/`element.Id`, provide a sibling `ElementId` overload that takes an explicit `Document`, with a descriptive name.
* Every extension must compile under every supported Revit configuration (`Debug.RNN`/`Release.RNN`) declared by the project SDK. Gate version-specific APIs with `#if REVIT2024_OR_GREATER`-style directives.
* Test only custom logic in `tests/Nice3point.Revit.Extensions.Tests` (TUnit on the Revit thread); skip thin Revit-API wrappers and all UI extensions.
* Update `Readme.md`, `Changelog.md`, and XML docs in the same change as any public-surface change.

## Specialized Docs

Before making related changes, read the matching file:

* [Project Structure](./docs/project-structure.md) - solution layout, source grouping, and change placement.
* [Architecture](./docs/architecture.md) - design goals, fluent API, extension model, and ElementId overloads.
* [Code Style](./docs/code-style.md) - C# 14 extension syntax, naming, file structure, XML docs, and error handling.
* [Backward Compatibility](./docs/backward-compatibility.md) - the Obsolete + CodeTemplate pattern and breaking-change rules.
* [Documentation](./docs/documentation.md) - Readme, Changelog, and XML documentation requirements.
* [Testing Strategy](./docs/testing-strategy.md) - TUnit/Revit tests, sample-driven coverage, and benchmarks.
* [Revit Best Practices](./docs/revit-best-practices.md) - Revit API usage, version matrix, performance, and internal helpers.
* [Package Management](./docs/package-management.md) - centralized NuGet and Revit-version package rules.

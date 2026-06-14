# Revit Best Practices

The library is consumed inside the Revit process, often on hot paths. Extensions must respect Revit API constraints and stay allocation-conscious.

## Wrapping the Revit API

* An extension wraps the Revit API; it does not reimplement it. Call the underlying API directly.
* Do not hide expensive API calls behind harmless-looking names — keep the cost of a call discoverable from its name.
* Read-only operations are `[Pure]`.
* Let Revit exceptions propagate; document them. See [Code Style](./code-style.md).

## Revit Versions

The active version comes from the `$(RevitVersion)` build property; the project (SDK `Nice3point.Revit.Sdk`) declares the full configuration list.

* Use conditional compilation (`#if REVIT2024_OR_GREATER`, etc.) only where the Revit API genuinely differs between versions.
* Keep shared behavior version-neutral wherever possible.
* Apply directives consistently across related methods so a type's surface stays coherent per version.
* Every extension must compile under every `Debug.RNN`/`Release.RNN` configuration declared by the project.
* Version-specific package versions belong in `Directory.Packages.props`. See [Package Management](./package-management.md).

## Performance

* **Avoid LINQ on hot paths.** Use traditional loops where allocations or iterator overhead matter.
* **Pre-size collections** when the count is known.
* **Use `StringBuilder`** for non-trivial string concatenation.
* **Prefer batch Revit APIs** over per-element calls.
* **Minimize transaction scope** in any extension that opens a transaction.
* Validate performance-sensitive changes with a benchmark under `tests/Nice3point.Revit.Extensions.Benchmarks`. See [Testing Strategy](./testing-strategy.md).

## Internal Helpers

* Reflection-based access (unsafe accessors), color/format utilities, and ribbon infrastructure live in `source/Nice3point.Revit.Extensions/Internal` and stay non-public.
* Do not expose `Internal` types through the public API surface.

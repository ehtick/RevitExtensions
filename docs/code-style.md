# Strict C# Production Style

All code must meet production-quality standards. "It works" is not enough; code must be clean, readable, and self-explanatory. This is a public library — its style is part of its API.

## General Principles

* **Modern C#:** target the latest language version. Use C# 14 `extension(Type) { }` blocks for all new extensions.
* **Explicit over implicit:** code should be self-explanatory; avoid hidden behavior and unclear defaults.
* **Pure functions:** mark read-only operations with `[Pure]`.
* **Nullable safety:** nullable reference types are enabled; treat nullability warnings as defects.
* **JetBrains Annotations:** use JetBrains annotations (`[PublicAPI]`, `[Pure]`, `CodeTemplate`) where they improve analysis and intent.

## Naming

* **Clarity is king.** Names must be descriptive and never abbreviated.
    * Bad: `elem`, `doc`, `param`, `ctx`.
    * Good: `element`, `document`, `parameter`, `context`.
* **Follow Revit API naming conventions:**
    * Passive voice for an operation tested on an object: `CanBeDeleted`, `CanBeMirrored`, `CanBeConvertedToFaceHostBased`.
    * Active voice when the object performs the action: `CanElementCutElement`.
* **Disambiguate `ElementId` overloads.** When a name would be too generic on `ElementId`, keep it specific: `elementId.MoveGlobalParameterUpOrder(document)`, not `elementId.MoveUpOrder(document)`.
* Avoid single-letter variables except in very short loops or lambdas.

## File & Class Structure

* **File-scoped namespaces.** All extension classes use `namespace Nice3point.Revit.Extensions;` or RevitAPIUI.dll related use `namespace Nice3point.Revit.Extensions.UI;` with `// ReSharper disable once CheckNamespace` above it (the file lives in a subfolder but the namespace stays flat for discoverability).
* **One type group per file**, named `<Type>Extensions.cs`.
* **`[PublicAPI]`** on every public extension class.
* **`extension(Type) { }` blocks** group all members that extend the same type.

```csharp
// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

[PublicAPI]
public static class ElementExtensions
{
    extension(Element element)
    {
        public Element JoinGeometry(Element secondElement)
        {
            JoinGeometryUtils.JoinGeometry(element.Document, element, secondElement);
            return element;
        }
    }
}
```

## XML Documentation

* Document every public method with a `<summary>`.
* For wrappers over the Revit API, copy the summary from the corresponding Revit API documentation.
* Document each parameter with `<param>` and the return value with `<returns>`.
* Document the Revit API exceptions a method can throw with `<exception>`.
* Keep comments concise and update them in the same change as the behavior.

## Error Handling

* **Let Revit exceptions propagate.** Never swallow Revit API exceptions; document them instead.
* **Validate only custom logic.** Validate inputs for behavior the library itself adds, not for thin wrappers where Revit already validates.
* Prefer semantic exceptions over generic `Exception` for any custom logic.

## Compilation Directives

* Use `#if REVIT2024_OR_GREATER` (and similar) for version-specific Revit APIs.
* Apply directives consistently across related methods so a type's surface is coherent per version.
* See [Revit Best Practices](./revit-best-practices.md) for the version matrix.

# Architecture & Design Principles

Nice3point.Revit.Extensions exists to make the Revit API pleasant to consume from C#. 
Every public member is an extension method that wraps a Revit API call behind a fluent, type-safe, discoverable surface. 

## Core Design Goals

* **Type safety:** use generics and nullable reference types to push errors to compile time.
* **Performance:** minimize allocations and follow optimized Revit API access patterns; this is a hot-path library consumed inside Revit.
* **Discoverability:** group extensions by the Revit type they extend so they surface naturally in IntelliSense on that type.
* **Backward compatibility:** never break an existing public API. See [Backward Compatibility](./backward-compatibility.md).

## Extension Method Model

* Use C# 14 `extension(Type type) { }` blocks instead of legacy `static` methods with `this` parameters.
* Group related members under a single `extension(Type)` / `extension<T>(Type<T>)` block.
* Generic forms (`extension<T>(...)`) and static-context forms (`extension(Type)`) are available where the wrapped API is generic or static.

```csharp
extension(Element element)
{
    public Element JoinGeometry(Element secondElement)
    {
        JoinGeometryUtils.JoinGeometry(element.Document, element, secondElement);
        return element; // enable chaining
    }
}
```

## Method Chaining

All mutation methods return the source object so calls can be chained. A method that would naturally return `void` returns the element it operated on instead.

## ElementId Overload Pattern

When a method operates through `element.Document` and `element.Id`, provide a sibling overload on `ElementId` that takes an explicit `Document`. Keep names descriptive when the bare name would be ambiguous on `ElementId`.

```csharp
extension(Element element)
{
    public bool CanBeDeleted() => DocumentValidation.CanDeleteElement(element.Document, element.Id);
}

extension(ElementId elementId)
{
    public bool CanBeDeleted(Document document) => DocumentValidation.CanDeleteElement(document, elementId);
}
```

* Use descriptive names so the `ElementId` context is never ambiguous: `elementId.MoveGlobalParameterUpOrder(document)`, not `elementId.MoveUpOrder(document)`.

## Design Rules

* Wrap the Revit API; do not reimplement it. A wrapper must call the underlying Revit API directly.
* Read-only operations are marked `[Pure]`.
* Keep the public surface in `[PublicAPI]`-marked classes; keep implementation detail in `Internal`.
* Do not leak `Internal` helpers (unsafe accessors, format utilities) into the public API.
* Version-specific Revit API differences are isolated behind compilation directives, not duplicated types. See [Revit Best Practices](./revit-best-practices.md).

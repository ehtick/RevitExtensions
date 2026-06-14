# Backward Compatibility

This is a public library with downstream consumers. A breaking change breaks other people's builds. **Never delete or change an existing public API.** Deprecate it instead.

## Obsolete Pattern

To rename or replace a method, mark the old one `[Obsolete]` and keep it functional. Provide a JetBrains `CodeTemplate` so ReSharper/Rider can auto-convert call sites.

```csharp
[Obsolete("Use CanBeMirrored() instead")]
[CodeTemplate(
    searchTemplate: "$expr$.CanMirrorElement()",
    Message = "CanMirrorElement is obsolete, use CanBeMirrored instead",
    ReplaceTemplate: "$expr$.CanBeMirrored()",
    ReplaceMessage = "Replace with CanBeMirrored()")]
public bool CanMirrorElement()
{
    return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
}
```

## Obsolete Guidelines

* **Message:** a clear explanation that names the replacement method.
* **CodeTemplate:** provide the ReSharper auto-conversion pattern (`searchTemplate` → `ReplaceTemplate`).
* **Implementation:** the obsolete method must call the **original Revit API**, not the new method — this avoids recursion and keeps it working independently.
* **No `EditorBrowsable`:** do not add `[EditorBrowsable(EditorBrowsableState.Never)]` to obsolete `Element` extension methods.

## Breaking Changes

* **Method signatures:** never change an existing signature. Add a new method instead.
* **Return types:** never change a return type, except the allowed `void` → source-object change for chaining.
* **Parameters:** add new parameters only as optional, and only at the end of the list.
* **Renaming:** use the Obsolete pattern above; keep the old method functional indefinitely.
* Document every change — additions, deprecations, and behavior changes — in [the Changelog](./documentation.md).

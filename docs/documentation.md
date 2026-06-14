# Documentation Requirements

Documentation ships with the package. Every change to the public surface updates the Readme, the Changelog, and the XML docs in the same commit.

## Readme.md

* **Usage examples:** every new extension category must have a usage example.
* **ElementId overloads go in the existing section.** When adding an `ElementId` overload for a method that already has an `Element` example, add it to the same section — do not create a new one.

  ```csharp
  // Element extension
  element.Copy(new XYZ(1, 1, 0));

  // ElementId extension (added to the same section)
  elementId.Copy(document, new XYZ(1, 1, 0));
  ```
* **No new sections for variants.** Overloads and `ElementId` siblings belong with their primary method.

## Changelog.md

* Update the current preview/release version section.
* Categorize every change:
    * **New Features:** new extension methods, new `ElementId` overloads.
    * **Breaking Changes:** renamed methods, changed behavior.
    * **Improvements:** performance, refactoring.
    * **Bug Fixes:** corrections to existing functionality.
* Provide migration examples at the end of the section for any breaking change or deprecation.
* **Document all changes,** not only major ones.

## XML Documentation

* **Summary:** describe what the method does. For Revit API wrappers, copy the summary from the Revit API documentation.
* **Parameters:** document each parameter with context.
* **Returns:** describe the meaning of the return value.
* **Exceptions:** document every Revit API exception the method can throw.

See [Code Style](./code-style.md) for the in-code XML doc conventions and [Backward Compatibility](./backward-compatibility.md) for deprecation messaging.

# Package Management

The solution uses centralized NuGet package management. All versions live in `Directory.Packages.props`.

## Rules

* Define every package version in `Directory.Packages.props`. Do not add `<Version>` to individual `PackageReference` items.
* Keep Revit-version-specific packages conditional on `$(RevitVersion)`. Revit API packages float to `$(RevitVersion).*`; the Toolkit, TUnit.Revit, and BenchmarkDotNet.Revit packages are pinned per version.
* Keep shared dependency versions unconditional unless they truly vary by Revit version.
* Use `GlobalPackageReference` only for packages that apply solution-wide (currently `Polyfill` and `JetBrains.Annotations.Sources`).
* Revit API references in the library are `PrivateAssets="all"` — they are build-time only and never flow to consumers of the package.

## Adding Dependencies

1. Add the package version to `Directory.Packages.props`.
2. Add a versionless `PackageReference` to the project that uses it.
3. Keep dependency scope narrow. The shipped library should stay dependency-light — prefer existing packages and platform/Revit APIs before introducing a new dependency.

## Updating Dependencies

* When updating Revit-specific packages, verify all supported Revit versions (`R19`–`R27`) still resolve.
* Keep dependency updates focused and easy to review; do not mix them with feature work.
* Run the relevant build/tests after any dependency change.
* Renovate (`renovate.json`) manages routine version bumps.

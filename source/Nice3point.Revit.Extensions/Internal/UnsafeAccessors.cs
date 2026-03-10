#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
using Autodesk.Revit.ApplicationServices;

namespace Nice3point.Revit.Extensions.Internal;

internal static class UnsafeAccessors
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    public static extern ControlledApplication CreateControlledApplication(Application application);
}
#endif
#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
using Autodesk.Revit.UI;
using RibbonItem = Autodesk.Windows.RibbonItem;

namespace Nice3point.Revit.Extensions.Internal;

internal static class UnsafeAccessors
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    public static extern RibbonPanel CreateRibbonPanel(Autodesk.Windows.RibbonPanel panel, string tabId);

    [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "m_ItemsNameDictionary")]
    public static extern ref Dictionary<string, Dictionary<string, RibbonPanel>> GetCachedTabs(UIApplication? uiApplication);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "m_RibbonItem")]
    public static extern ref RibbonItem GetInternalItem(Autodesk.Revit.UI.RibbonItem itemPointer);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "m_RibbonPanel")]
    public static extern ref Autodesk.Windows.RibbonPanel GetInternalPanel(RibbonPanel panelPointer);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "addItemToRowPanel")]
    public static extern object AddItemToRowPanel(RibbonPanel panelPointer, object panel, RibbonItemData itemData);
}
#endif
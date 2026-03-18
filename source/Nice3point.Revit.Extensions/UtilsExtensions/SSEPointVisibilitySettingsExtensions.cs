#if REVIT2024_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SSEPointVisibilitySettings"/> class.
/// </summary>
[PublicAPI]
public static class SsePointVisibilitySettingsExtensions
{
    /// <param name="category">The source category.</param>
    extension(Category category)
    {
        /// <summary>Gets the SSE point visibility for the given category.</summary>
        /// <param name="document">The document.</param>
        /// <returns>
        ///    The visibility of the given category. True means the SSE points are visible.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The category is not valid for SSE.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool GetSsePointVisibility(Document document)
        {
            return SSEPointVisibilitySettings.GetVisibility(document, category.Id);
        }

        /// <summary>Sets the SSE point visibility for the given category.</summary>
        /// <param name="document">The document.</param>
        /// <param name="isVisible">
        ///    The visibility of the given category. True means the SSE points are visible.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The category is not valid for SSE.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void SetSsePointVisibility(Document document, bool isVisible)
        {
            SSEPointVisibilitySettings.SetVisibility(document, category.Id, isVisible);
        }
    }
}
#endif
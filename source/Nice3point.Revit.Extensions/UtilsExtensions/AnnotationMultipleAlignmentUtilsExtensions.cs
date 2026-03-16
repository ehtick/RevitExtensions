#if REVIT2025_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AnnotationMultipleAlignmentUtils"/> class.
/// </summary>
[PublicAPI]
public static class AnnotationMultipleAlignmentUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Returns true if element can be aligned to other similar elements.</summary>
        /// <returns>
        ///    True if the element can be aligned using the multiple alignment commands, false otherwise.
        /// </returns>
        public bool SupportsMultiAlign => AnnotationMultipleAlignmentUtils.ElementSupportsMultiAlign(element);

        /// <summary>
        ///    Gets the four corners of the alignable element in model space without its leaders.
        /// </summary>
        /// <returns>The array of the four corner points for the alignable element.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element element does not support multiple alignment behavior.
        ///    -or-
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<XYZ> GetAnnotationOutlineWithoutLeaders()
        {
            return AnnotationMultipleAlignmentUtils.GetAnnotationOutlineWithoutLeaders(element);
        }

        /// <summary>Moves the element while keeping the leader end points anchored.</summary>
        /// <param name="moveVector">The move vector for translating the element.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element element does not support multiple alignment behavior.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to move element.
        /// </exception>
        public void MoveWithAnchoredLeaders(XYZ moveVector)
        {
            AnnotationMultipleAlignmentUtils.MoveWithAnchoredLeaders(element, moveVector);
        }
    }
}
#endif
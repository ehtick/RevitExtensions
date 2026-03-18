#if REVIT2022_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.BoundaryValidation"/> class.
/// </summary>
[PublicAPI]
public static class BoundaryValidationExtensions
{
    extension(CurveLoop)
    {
        /// <summary>
        ///    Identifies whether the given curve loops compose a valid horizontal boundary.
        /// </summary>
        /// <remarks>
        ///    The curve loops are valid if projections of the loops onto a horizontal(XY) plane do not intersect each other;
        ///    each curve loop is closed; input curves do not contain any helical curve;
        ///    and each loop is planar and lies on a plane parallel to the horizontal(XY) plane, but not necessarily the same plane.
        /// </remarks>
        /// <param name="curveLoops">The curve loops to be checked.</param>
        /// <returns>
        ///    True if the given curve loops are valid as described above, false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static bool IsValidHorizontalBoundary(IList<CurveLoop> curveLoops)
        {
            return BoundaryValidation.IsValidHorizontalBoundary(curveLoops);
        }
#if REVIT2023_OR_GREATER

        /// <summary>
        ///    Indicates if the given curve loops compose a valid boundary on the sketch plane.
        /// </summary>
        /// <remarks>
        ///    The curve loops are valid if projections of the loops onto the sketch plane do not intersect each other;
        ///    each curve loop is closed; input curves do not contain any helical curve;
        ///    and each loop is planar and lies on a plane parallel to the sketch plane, but not necessarily the same plane.
        /// </remarks>
        /// <param name="sketchPlane">The sketch plane.</param>
        /// <param name="curveLoops">The curve loops to be checked.</param>
        /// <returns>
        ///    True if the given curve loops are valid as described above, false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static bool IsValidBoundaryOnSketchPlane(SketchPlane sketchPlane, IList<CurveLoop> curveLoops)
        {
            return BoundaryValidation.IsValidBoundaryOnSketchPlane(sketchPlane, curveLoops);
        }

        /// <summary>
        ///    Indicates if the given curve loops compose a valid boundary on the view's detail sketch plane.
        /// </summary>
        /// <remarks>
        ///    The curve loops are valid if projections of the loops onto the views's detail sketch plane do not intersect each other;
        ///    each curve loop is closed; input curves do not contain any helical curve;
        ///    and each loop is planar and lies on a plane parallel to the views's detail sketch plane, but not necessarily the same plane.
        /// </remarks>
        /// <param name="document">The document.</param>
        /// <param name="viewId">The view Id.</param>
        /// <param name="curveLoops">The curve loops to be checked.</param>
        /// <returns>
        ///    True if the given curve loops are valid as described above, false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static bool IsValidBoundaryOnView(Document document, ElementId viewId, IList<CurveLoop> curveLoops)
        {
            return BoundaryValidation.IsValidBoundaryOnView(document, viewId, curveLoops);
        }
#endif
    }
}
#endif
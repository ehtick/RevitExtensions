#if REVIT2025_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarSpliceUtils"/> class.
/// </summary>
[PublicAPI]
public static class RebarSpliceUtilsExtensions
{
    /// <param name="rebar">The source rebar.</param>
    extension(Rebar rebar)
    {
        /// <summary>Verifies if the rebar can be spliced with the provided line.</summary>
        /// <remarks>
        ///   <p> If the provided linePlaneNormal is parallel with the bar plane normal the bounded line will be used to calculate the splice geometries where the bar will be split. </p>
        ///   <p> Otherwise, the line will be extended to exceed the bounding box of the bar. </p>
        /// </remarks>
        /// <param name="spliceOptions">The RebarSpliceOptions.</param>
        /// <param name="line">The line to splice the rebar with.</param>
        /// <param name="linePlaneNormal">The normal that determines the plane of the line.</param>
        /// <returns>
        ///    Will return RebarSpliceError.Success if it's possible to splice with line or other enum value corresponding to the error that occurred.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public RebarSpliceError CanBeSpliced(RebarSpliceOptions spliceOptions, Line line, XYZ linePlaneNormal)
        {
            return RebarSpliceUtils.CanRebarBeSpliced(rebar, spliceOptions, line, linePlaneNormal);
        }

        /// <summary>Verifies if the rebar can be spliced with the provided line.</summary>
        /// <remarks>
        ///   <p> This method uses the view normal in case of a 2D view and the current workplane normal in case of a 3D view. </p>
        ///   <p> If the view normal is parallel with the bar plane normal the bounded line will be used to calculate the splice geometries where the bar will be split. </p>
        ///   <p> Otherwise, the line will be extended to exceed the bounding box of the bar. </p>
        /// </remarks>
        /// <param name="spliceOptions">The RebarSpliceOptions.</param>
        /// <param name="line">The line to splice the rebar with.</param>
        /// <param name="viewId">Based on the view it will be determined the plane of the line.</param>
        /// <returns>
        ///    Will return RebarSpliceError.Success if it's possible to splice with line or other enum value corresponding to the error that occurred.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public RebarSpliceError CanBeSpliced(RebarSpliceOptions spliceOptions, Line line, ElementId viewId)
        {
            return RebarSpliceUtils.CanRebarBeSpliced(rebar, spliceOptions, line, viewId);
        }

        /// <summary>Verifies if the rebar can be spliced with the RebarSpliceGeometry.</summary>
        /// <param name="spliceOptions">The RebarSpliceOptions.</param>
        /// <param name="spliceGeometry">The RebarSpliceGeometry.</param>
        /// <returns>
        ///    Will return RebarSpliceError.Success if it's possible to splice with point or other enum value corresponding to the error that occurred.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public RebarSpliceError CanBeSpliced(RebarSpliceOptions spliceOptions, RebarSpliceGeometry spliceGeometry)
        {
            return RebarSpliceUtils.CanRebarBeSpliced(rebar, spliceOptions, spliceGeometry);
        }

        /// <summary>
        ///    This function calculates the lap direction given a RebarSpliceGeometry and a RebarSplicePosition.
        /// </summary>
        /// <remarks>
        ///    This function determines first the point where the RebarSpliceGeometry will cut the rebar.
        ///    Based on the RebarSplicePosition value it will determine the direction where the lap will go, RebarSplicePosition.End1 meaning towards the start of the splice chain and RebarSplicePosition.End2 towards the end.
        ///    For more information about the splice chain concept please check Autodesk.Revit.DB.Structure.RebarSpliceUtils.GetSpliceChain(Rebar rebar).
        ///    This function will throw exception if it's called with RebarSplicePosition.Middle.
        /// </remarks>
        /// <param name="spliceGeometry">The splice geometry.</param>
        /// <param name="splicePosition">The splice position.</param>
        /// <returns>The lap direction.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    RebarSplicePosition should not be Middle.
        ///    -or-
        ///    The rebar cannot be spliced with the provided RebarSpliceGeometry.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        [Pure]
        public XYZ GetLapDirectionForSpliceGeometryAndPosition(RebarSpliceGeometry spliceGeometry, RebarSplicePosition splicePosition)
        {
            return RebarSpliceUtils.GetLapDirectionForSpliceGeometryAndPosition(rebar, spliceGeometry, splicePosition);
        }

        /// <summary>
        ///    Returns all the rebars that are part of a splice chain with the input rebar.
        /// </summary>
        /// <remarks>
        ///    The elements in the output list are in exactly the same order that they are constrained to each other.
        ///    The first element in the array is the one whose Rebar Plane, Out of Plane Extent and Edge are not constrained to a rebar from the chain (second is constrained to first, third to second and so on).
        ///    The output list will also contain the input rebar id.
        ///    If the rebar is not in a splice chain the returned list will contain just one element which is the id of the input rebar.
        /// </remarks>
        /// <returns>The splice chain.</returns>
        [Pure]
        public IList<ElementId> GetSpliceChain()
        {
            return RebarSpliceUtils.GetSpliceChain(rebar);
        }

        /// <summary>
        ///    Computes a list of RebarSpliceGeometry which respects the rules. This list can be used to splice the Rebar.
        /// </summary>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="spliceRules">The splice rules.</param>
        /// <returns>
        ///    Returns the result of the operation. If the operation was successful the RebarSpliceByRulesResult.Error member will have "RebarSpliceByRulesError.Success" value.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    RebarSplicePosition should be Middle.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public RebarSpliceByRulesResult GetSpliceGeometries(RebarSpliceOptions spliceOptions, RebarSpliceRules spliceRules)
        {
            return RebarSpliceUtils.GetSpliceGeometries(rebar.Document, rebar.Id, spliceOptions, spliceRules);
        }

        /// <summary>Splice a rebar with a line.</summary>
        /// <remarks>
        ///   <p> If the provided linePlaneNormal is parallel with the bar plane normal the bounded line will be used to calculate the splice geometries where the bar will be split. </p>
        ///   <p> Otherwise, the line will be extended to exceed the bounding box of the bar. </p>
        /// </remarks>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="line">The line used for splice.</param>
        /// <param name="linePlaneNormal">The normal of the plane of the line.</param>
        /// <returns>Returns the ids of the rebars that are considered to be spliced.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Free Form Rebars, or Shape Driven Rebars that are Multiplanar or have a shape that whose definition is RebarShapeDefinitionByArc can't be spliced.
        ///    Also, if the Rebar is member of a Group it cannot be spliced.
        ///    -or-
        ///    The rebar cannot be spliced with the provided line.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public IList<ElementId> Splice(RebarSpliceOptions spliceOptions, Line line, XYZ linePlaneNormal)
        {
            return RebarSpliceUtils.SpliceRebar(rebar.Document, rebar.Id, spliceOptions, line, linePlaneNormal);
        }

        /// <summary>Splice a Rebar with a line.</summary>
        /// <remarks>
        ///   <p> This method uses the view normal in case of a 2D view and the current workplane normal in case of a 3D view. </p>
        ///   <p> If the view normal is parallel with the bar plane normal the bounded line will be used to calculate the splice geometries where the bar will be split. </p>
        ///   <p> Otherwise, the line will be extended to exceed the bounding box of the bar. </p>
        /// </remarks>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="line">The line used for splice.</param>
        /// <param name="viewId">Based on the view it will be determined the plane of the line.</param>
        /// <returns>Returns the ids of the rebars that are considered to be spliced.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Free Form Rebars, or Shape Driven Rebars that are Multiplanar or have a shape that whose definition is RebarShapeDefinitionByArc can't be spliced.
        ///    Also, if the Rebar is member of a Group it cannot be spliced.
        ///    -or-
        ///    The rebar cannot be spliced with the provided line.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public IList<ElementId> Splice(RebarSpliceOptions spliceOptions, Line line, ElementId viewId)
        {
            return RebarSpliceUtils.SpliceRebar(rebar.Document, rebar.Id, spliceOptions, line, viewId);
        }

        /// <summary>Splice a rebar with a list of RebarSpliceGeometry.</summary>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="spliceGeometries">
        ///    A list of RebarSpliceGeometry that indicates where the rebar will be spliced.
        /// </param>
        /// <returns>Returns the ids of the rebars that are considered to be spliced.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Free Form Rebars, or Shape Driven Rebars that are Multiplanar or have a shape that whose definition is RebarShapeDefinitionByArc can't be spliced.
        ///    Also, if the Rebar is member of a Group it cannot be spliced.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public IList<ElementId> Splice(RebarSpliceOptions spliceOptions, IList<RebarSpliceGeometry> spliceGeometries)
        {
            return RebarSpliceUtils.SpliceRebar(rebar.Document, rebar.Id, spliceOptions, spliceGeometries);
        }

        /// <summary>
        ///    This method will unify the rebars by removing the splice between them. A new rebar will be created because of this operation.
        ///    The curves of the resulted rebar will be the curves of the first rebar continued by the curves of the second rebar.
        ///    The resulted rebar will take data from the first rebar. (e.g.. layout, moved/removed bars, etc.).
        /// </summary>
        /// <param name="secondRebarId">Second Rebar id.</param>
        /// <returns>
        ///    Returns the id of the new rebar. In case that unify operation fails, it will return invalidElementId
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Rebar elements must have a splice connection between them in order to be unified.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ElementId UnifyRebarsIntoOne(ElementId secondRebarId)
        {
            return RebarSpliceUtils.UnifyRebarsIntoOne(rebar.Document, rebar.Id, secondRebarId);
        }
    }

    /// <param name="elementId">The rebar element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>
        ///    Computes a list of RebarSpliceGeometry which respects the rules. This list can be used to splice the Rebar.
        /// </summary>
        /// <param name="document">The document</param>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="spliceRules">The splice rules.</param>
        /// <returns>
        ///    Returns the result of the operation. If the operation was successful the RebarSpliceByRulesResult.Error member will have "RebarSpliceByRulesError.Success" value.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    RebarSplicePosition should be Middle.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public RebarSpliceByRulesResult GetRebarSpliceGeometries(Document document, RebarSpliceOptions spliceOptions, RebarSpliceRules spliceRules)
        {
            return RebarSpliceUtils.GetSpliceGeometries(document, elementId, spliceOptions, spliceRules);
        }

        /// <summary>Splice a rebar with a line.</summary>
        /// <remarks>
        ///   <p> If the provided linePlaneNormal is parallel with the bar plane normal the bounded line will be used to calculate the splice geometries where the bar will be split. </p>
        ///   <p> Otherwise, the line will be extended to exceed the bounding box of the bar. </p>
        /// </remarks>
        /// <param name="document">The document</param>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="line">The line used for splice.</param>
        /// <param name="linePlaneNormal">The normal of the plane of the line.</param>
        /// <returns>Returns the ids of the rebars that are considered to be spliced.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Free Form Rebars, or Shape Driven Rebars that are Multiplanar or have a shape that whose definition is RebarShapeDefinitionByArc can't be spliced.
        ///    Also, if the Rebar is member of a Group it cannot be spliced.
        ///    -or-
        ///    The rebar cannot be spliced with the provided line.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public IList<ElementId> SpliceRebar(Document document, RebarSpliceOptions spliceOptions, Line line, XYZ linePlaneNormal)
        {
            return RebarSpliceUtils.SpliceRebar(document, elementId, spliceOptions, line, linePlaneNormal);
        }

        /// <summary>Splice a Rebar with a line.</summary>
        /// <remarks>
        ///   <p> This method uses the view normal in case of a 2D view and the current workplane normal in case of a 3D view. </p>
        ///   <p> If the view normal is parallel with the bar plane normal the bounded line will be used to calculate the splice geometries where the bar will be split. </p>
        ///   <p> Otherwise, the line will be extended to exceed the bounding box of the bar. </p>
        /// </remarks>
        /// <param name="document">The document</param>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="line">The line used for splice.</param>
        /// <param name="viewId">Based on the view it will be determined the plane of the line.</param>
        /// <returns>Returns the ids of the rebars that are considered to be spliced.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Free Form Rebars, or Shape Driven Rebars that are Multiplanar or have a shape that whose definition is RebarShapeDefinitionByArc can't be spliced.
        ///    Also, if the Rebar is member of a Group it cannot be spliced.
        ///    -or-
        ///    The rebar cannot be spliced with the provided line.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public IList<ElementId> SpliceRebar(Document document, RebarSpliceOptions spliceOptions, Line line, ElementId viewId)
        {
            return RebarSpliceUtils.SpliceRebar(document, elementId, spliceOptions, line, viewId);
        }

        /// <summary>Splice a rebar with a list of RebarSpliceGeometry.</summary>
        /// <param name="document">The document</param>
        /// <param name="spliceOptions">The rebar splice options.</param>
        /// <param name="spliceGeometries">
        ///    A list of RebarSpliceGeometry that indicates where the rebar will be spliced.
        /// </param>
        /// <returns>Returns the ids of the rebars that are considered to be spliced.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Free Form Rebars, or Shape Driven Rebars that are Multiplanar or have a shape that whose definition is RebarShapeDefinitionByArc can't be spliced.
        ///    Also, if the Rebar is member of a Group it cannot be spliced.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public IList<ElementId> SpliceRebar(Document document, RebarSpliceOptions spliceOptions, IList<RebarSpliceGeometry> spliceGeometries)
        {
            return RebarSpliceUtils.SpliceRebar(document, elementId, spliceOptions, spliceGeometries);
        }

        /// <summary>
        ///    This method will unify the rebars by removing the splice between them. A new rebar will be created because of this operation.
        ///    The curves of the resulted rebar will be the curves of the first rebar continued by the curves of the second rebar.
        ///    The resulted rebar will take data from the first rebar. (e.g.. layout, moved/removed bars, etc.).
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="secondRebarId">Second Rebar id.</param>
        /// <returns>
        ///    Returns the id of the new rebar. In case that unify operation fails, it will return invalidElementId
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Rebar elements must have a splice connection between them in order to be unified.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ElementId UnifyRebarsIntoOne(Document document, ElementId secondRebarId)
        {
            return RebarSpliceUtils.UnifyRebarsIntoOne(document, elementId, secondRebarId);
        }
    }
}
#endif
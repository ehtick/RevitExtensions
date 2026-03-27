#if REVIT2025_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;
using Nice3point.Revit.Extensions.Internal;
using Document = Autodesk.Revit.Creation.Document;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarSpliceTypeUtils"/> class.
/// </summary>
[PublicAPI]
public static class RebarSpliceTypeUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>Creates a Rebar Splice Type element.</summary>
        /// <param name="typeName">The Rebar Splice Type name.</param>
        /// <returns>The Rebar Splice Type.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The given value for typeName cannot be an empty string.
        ///    -or-
        ///    The given value for typeName cannot include prohibited characters.
        ///    -or-
        ///    The given value for typeName is already in use by another Rebar Splice Type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ElementType NewRebarSpliceType(string typeName)
        {
            var dbDocument = UnsafeAccessors.GetDocument(document);
            return RebarSpliceTypeUtils.CreateRebarSpliceType(dbDocument, typeName);
        }
    }

    /// <param name="rebarSpliceType">The source rebar splice type element.</param>
    extension(ElementType rebarSpliceType)
    {
        /// <summary>Gets the lap length multiplier value.</summary>
        /// <returns>Returns the lap length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        [Pure]
        public double GetRebarSpliceLapLengthMultiplier()
        {
            return RebarSpliceTypeUtils.GetLapLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id);
        }

        /// <summary>Identifies the way bars are shifted in the splice relation.</summary>
        /// <returns>Returns the way bars are shifted in the splice relation.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        [Pure]
        public RebarSpliceShiftOption GetRebarSpliceShiftOption()
        {
            return RebarSpliceTypeUtils.GetShiftOption(rebarSpliceType.Document, rebarSpliceType.Id);
        }

        /// <summary>Gets the stagger multiplier value.</summary>
        /// <returns>Returns the stagger length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        [Pure]
        public double GetRebarSpliceStaggerLengthMultiplier()
        {
            return RebarSpliceTypeUtils.GetStaggerLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id);
        }

        /// <summary>Sets the lap length multiplier value.</summary>
        /// <param name="lapLengthMultiplier">The lap length multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for lapLengthMultiplier must be non-negative.
        /// </exception>
        public void SetRebarSpliceLapLengthMultiplier(double lapLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetLapLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id, lapLengthMultiplier);
        }

        /// <summary>Sets the way bars are shifted in the splice relation.</summary>
        /// <param name="shiftOption">The way bars are shifted in the splice relation.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        public void SetRebarSpliceShiftOption(RebarSpliceShiftOption shiftOption)
        {
            RebarSpliceTypeUtils.SetShiftOption(rebarSpliceType.Document, rebarSpliceType.Id, shiftOption);
        }

        /// <summary>Sets the lap length multiplier value.</summary>
        /// <param name="staggerLengthMultiplier">The stagger multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for staggerLengthMultiplier must be non-negative.
        /// </exception>
        public void SetRebarSpliceStaggerLengthMultiplier(double staggerLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetStaggerLengthMultiplier(rebarSpliceType.Document, rebarSpliceType.Id, staggerLengthMultiplier);
        }
    }

    /// <param name="rebarSpliceTypeId">The rebar splice type element id.</param>
    extension(ElementId rebarSpliceTypeId)
    {
        /// <summary>Gets the lap length multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <returns>Returns the lap length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetRebarSpliceLapLengthMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarSpliceTypeUtils.GetLapLengthMultiplier(document, rebarSpliceTypeId);
        }

        /// <summary>Identifies the way bars are shifted in the splice relation.</summary>
        /// <param name="document">The document.</param>
        /// <returns>Returns the way bars are shifted in the splice relation.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public RebarSpliceShiftOption GetRebarSpliceShiftOption(Autodesk.Revit.DB.Document document)
        {
            return RebarSpliceTypeUtils.GetShiftOption(document, rebarSpliceTypeId);
        }

        /// <summary>Gets the stagger multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <returns>Returns the stagger length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetRebarSpliceStaggerLengthMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarSpliceTypeUtils.GetStaggerLengthMultiplier(document, rebarSpliceTypeId);
        }

        /// <summary>Sets the lap length multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <param name="lapLengthMultiplier">The lap length multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for lapLengthMultiplier must be non-negative.
        /// </exception>
        public void SetRebarSpliceLapLengthMultiplier(Autodesk.Revit.DB.Document document, double lapLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetLapLengthMultiplier(document, rebarSpliceTypeId, lapLengthMultiplier);
        }

        /// <summary>Sets the way bars are shifted in the splice relation.</summary>
        /// <param name="document">The document.</param>
        /// <param name="shiftOption">The way bars are shifted in the splice relation.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        public void SetRebarSpliceShiftOption(Autodesk.Revit.DB.Document document, RebarSpliceShiftOption shiftOption)
        {
            RebarSpliceTypeUtils.SetShiftOption(document, rebarSpliceTypeId, shiftOption);
        }

        /// <summary>Sets the lap length multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <param name="staggerLengthMultiplier">The stagger multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for staggerLengthMultiplier must be non-negative.
        /// </exception>
        public void SetRebarSpliceStaggerLengthMultiplier(Autodesk.Revit.DB.Document document, double staggerLengthMultiplier)
        {
            RebarSpliceTypeUtils.SetStaggerLengthMultiplier(document, rebarSpliceTypeId, staggerLengthMultiplier);
        }
    }
}
#endif
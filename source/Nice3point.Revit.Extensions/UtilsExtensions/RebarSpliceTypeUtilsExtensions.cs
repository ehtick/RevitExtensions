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
        [Pure]
        public ElementType NewRebarSpliceType(string typeName)
        {
#if NET8_0_OR_GREATER
            var dbDocument = UnsafeAccessors.GetDocument(document);
#else
            throw new NotSupportedException("Revit 2026 and greater do not support .NET lower than 8.0");
#endif
            return RebarSpliceTypeUtils.CreateRebarSpliceType(dbDocument, typeName);
        }
    }

    /// <param name="element">The source rebar splice type element.</param>
    extension(Element element)
    {
        /// <summary>Gets the lap length multiplier value.</summary>
        /// <returns>Returns the lap length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        [Pure]
        public double GetRebarSpliceLapLengthMultiplier()
        {
            return RebarSpliceTypeUtils.GetLapLengthMultiplier(element.Document, element.Id);
        }

        /// <summary>Identifies the way bars are shifted in the splice relation.</summary>
        /// <returns>Returns the way bars are shifted in the splice relation.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        [Pure]
        public RebarSpliceShiftOption GetRebarSpliceShiftOption()
        {
            return RebarSpliceTypeUtils.GetShiftOption(element.Document, element.Id);
        }

        /// <summary>Gets the stagger multiplier value.</summary>
        /// <returns>Returns the stagger length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarSpliceTypeId doesn't represent a valid Rebar Splice Type. It should be an ElementType of BuiltInCategory.OST_RebarSpliceType category.
        /// </exception>
        [Pure]
        public double GetRebarSpliceStaggerLengthMultiplier()
        {
            return RebarSpliceTypeUtils.GetStaggerLengthMultiplier(element.Document, element.Id);
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
            RebarSpliceTypeUtils.SetLapLengthMultiplier(element.Document, element.Id, lapLengthMultiplier);
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
            RebarSpliceTypeUtils.SetShiftOption(element.Document, element.Id, shiftOption);
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
            RebarSpliceTypeUtils.SetStaggerLengthMultiplier(element.Document, element.Id, staggerLengthMultiplier);
        }
    }

    /// <param name="elementId">The rebar splice type element id.</param>
    extension(ElementId elementId)
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
            return RebarSpliceTypeUtils.GetLapLengthMultiplier(document, elementId);
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
            return RebarSpliceTypeUtils.GetShiftOption(document, elementId);
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
            return RebarSpliceTypeUtils.GetStaggerLengthMultiplier(document, elementId);
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
            RebarSpliceTypeUtils.SetLapLengthMultiplier(document, elementId, lapLengthMultiplier);
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
            RebarSpliceTypeUtils.SetShiftOption(document, elementId, shiftOption);
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
            RebarSpliceTypeUtils.SetStaggerLengthMultiplier(document, elementId, staggerLengthMultiplier);
        }
    }
}
#endif
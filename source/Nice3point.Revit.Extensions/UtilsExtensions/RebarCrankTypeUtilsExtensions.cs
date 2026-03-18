#if REVIT2026_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;
using Nice3point.Revit.Extensions.Internal;
using Document = Autodesk.Revit.Creation.Document;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarCrankTypeUtils"/> class.
/// </summary>
[PublicAPI]
public static class RebarCrankTypeUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>Creates a Rebar Crank Type element.</summary>
        /// <param name="typeName">The Rebar Crank Type name.</param>
        /// <returns>The Rebar Crank Type.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The given value for typeName cannot be an empty string.
        ///    -or-
        ///    The given value for typeName cannot include prohibited characters.
        ///    -or-
        ///    The given value for typeName is already in use by another Rebar Crank Type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ElementType NewRebarCrankType(string typeName)
        {
#if NET8_0_OR_GREATER
            var dbDocument = UnsafeAccessors.GetDocument(document);
#else
            throw new NotSupportedException("Revit 2026 and greater do not support .NET lower than 8.0");
#endif
            return RebarCrankTypeUtils.CreateRebarCrankType(dbDocument, typeName);
        }
    }

    /// <param name="element">The source rebar crank type element.</param>
    extension(Element element)
    {
        /// <summary>Gets the crank length multiplier value.</summary>
        /// <returns>Returns the crank length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        [Pure]
        public double GetRebarCrankLengthMultiplier()
        {
            return RebarCrankTypeUtils.GetCrankLengthMultiplier(element.Document, element.Id);
        }

        /// <summary>Gets the crank offset multiplier value.</summary>
        /// <returns>Returns the crank offset multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        [Pure]
        public double GetRebarCrankOffsetMultiplier()
        {
            return RebarCrankTypeUtils.GetCrankOffsetMultiplier(element.Document, element.Id);
        }

        /// <summary>Gets the crank ratio value.</summary>
        /// <remarks>The crank slope is 1/crankRatio.</remarks>
        /// <returns>Returns the crank ratio value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        [Pure]
        public double GetRebarCrankRatio()
        {
            return RebarCrankTypeUtils.GetCrankRatio(element.Document, element.Id);
        }

        /// <summary>Sets the crank length multiplier value.</summary>
        /// <param name="crankLengthMultiplier">The crank length multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for crankLengthMultiplier must be positive.
        /// </exception>
        public void SetRebarCrankLengthMultiplier(double crankLengthMultiplier)
        {
            RebarCrankTypeUtils.SetCrankLengthMultiplier(element.Document, element.Id, crankLengthMultiplier);
        }

        /// <summary>Sets the crank offset multiplier value.</summary>
        /// <param name="crankOffsetMultiplier">The crank offset multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for crankOffsetMultiplier must be positive.
        /// </exception>
        public void SetRebarCrankOffsetMultiplier(double crankOffsetMultiplier)
        {
            RebarCrankTypeUtils.SetCrankOffsetMultiplier(element.Document, element.Id, crankOffsetMultiplier);
        }

        /// <summary>Sets the crank ratio value.</summary>
        /// <remarks>The crank slope is 1/crankRatio.</remarks>
        /// <param name="crankRatio">The crank ratio value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for crankRatio must be positive.
        /// </exception>
        public void SetRebarCrankRatio(double crankRatio)
        {
            RebarCrankTypeUtils.SetCrankRatio(element.Document, element.Id, crankRatio);
        }
    }

    /// <param name="elementId">The rebar crank type element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Gets the crank length multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <returns>Returns the crank length multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetRebarCrankLengthMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarCrankTypeUtils.GetCrankLengthMultiplier(document, elementId);
        }

        /// <summary>Gets the crank offset multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <returns>Returns the crank offset multiplier value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetRebarCrankOffsetMultiplier(Autodesk.Revit.DB.Document document)
        {
            return RebarCrankTypeUtils.GetCrankOffsetMultiplier(document, elementId);
        }

        /// <summary>Gets the crank ratio value.</summary>
        /// <remarks>The crank slope is 1/crankRatio.</remarks>
        /// <param name="document">The document.</param>
        /// <returns>Returns the crank ratio value.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public double GetRebarCrankRatio(Autodesk.Revit.DB.Document document)
        {
            return RebarCrankTypeUtils.GetCrankRatio(document, elementId);
        }

        /// <summary>Sets the crank length multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <param name="crankLengthMultiplier">The crank length multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for crankLengthMultiplier must be positive.
        /// </exception>
        public void SetRebarCrankLengthMultiplier(Autodesk.Revit.DB.Document document, double crankLengthMultiplier)
        {
            RebarCrankTypeUtils.SetCrankLengthMultiplier(document, elementId, crankLengthMultiplier);
        }

        /// <summary>Sets the crank offset multiplier value.</summary>
        /// <param name="document">The document.</param>
        /// <param name="crankOffsetMultiplier">The crank offset multiplier value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for crankOffsetMultiplier must be positive.
        /// </exception>
        public void SetRebarCrankOffsetMultiplier(Autodesk.Revit.DB.Document document, double crankOffsetMultiplier)
        {
            RebarCrankTypeUtils.SetCrankOffsetMultiplier(document, elementId, crankOffsetMultiplier);
        }

        /// <summary>Sets the crank ratio value.</summary>
        /// <remarks>The crank slope is 1/crankRatio.</remarks>
        /// <param name="document">The document.</param>
        /// <param name="rebarCrankTypeId">The Rebar Crank Type id.</param>
        /// <param name="crankRatio">The crank ratio value.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The rebarCrankTypeId doesn't represent a valid Rebar Crank Type. It should be an ElementType of BuiltInCategory.OST_RebarCrankType category.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The given value for crankRatio must be positive.
        /// </exception>
        public void SetRebarCrankRatio(Autodesk.Revit.DB.Document document, double crankRatio)
        {
            RebarCrankTypeUtils.SetCrankRatio(document, elementId, crankRatio);
        }
    }
}
#endif
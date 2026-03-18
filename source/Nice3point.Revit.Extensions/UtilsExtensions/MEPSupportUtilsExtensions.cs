#if REVIT2024_OR_GREATER
using JetBrains.Annotations;
using Document = Autodesk.Revit.Creation.Document;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.MEPSupportUtils"/> class.
/// </summary>
[PublicAPI]
public static class MepSupportUtilsExtensions
{
    extension(Document _)
    {
        /// <summary>Create family based stiffener on the specified fabrication ductwork.</summary>
        /// <param name="familySymbol">The stiffener FamilySymbol.</param>
        /// <param name="host">The host ductwork.</param>
        /// <param name="distanceFromHostEnd">
        ///    The distance from the host primary end to place the hosted instance. Units are in feet (ft).
        /// </param>
        /// <returns>The new stiffener family instance.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Invalid familySymbolId for stiffeners.
        ///    -or-
        ///    Host is not a straight ductwork.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The distance from host primary end is out of range.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The profiles of family symbol and host are mismatch.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.RegenerationFailedException">
        ///    Failed to create stiffener due to document regenerate error.
        /// </exception>
        public FamilyInstance NewDuctworkStiffener(FamilySymbol familySymbol, Element host, double distanceFromHostEnd)
        {
            return MEPSupportUtils.CreateDuctworkStiffener(familySymbol.Document, familySymbol.Id, host.Id, distanceFromHostEnd);
        }
    }
}
#endif
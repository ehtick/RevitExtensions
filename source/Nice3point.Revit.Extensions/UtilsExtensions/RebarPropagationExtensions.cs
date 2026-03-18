#if REVIT2023_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarPropagation"/> class.
/// </summary>
[PublicAPI]
public static class RebarPropagationExtensions
{
    /// <param name="sourceRebars">The source rebars.</param>
    extension(IList<Rebar> sourceRebars)
    {
        /// <summary>
        ///    It will copy the source rebars, will align them to the destination face based on the source face and adapt them to destination host.
        /// </summary>
        /// <remarks>
        ///   <p>The source and destination hosts represented by the source and destination references can be the same element or can be difereent elements. They can also be of different categories</p>
        ///   <p>The destination host must be able to host rebar.</p>
        ///   <p>The source rebars should not be gourp members.</p>
        ///   <p>This method uses its own transaction, so it's not permitted to be invoked in an active transaction.</p>
        /// </remarks>
        /// <param name="document">A document.</param>
        /// <param name="sourceFaceReference">A reference to a face in the source host.</param>
        /// <param name="destinationFaceReference">A reference to a face in the destions host.</param>
        /// <returns>The newly created rebars.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element that contains the destinationFaceReference is not a valid rebar host.
        ///    -or-
        ///    The rebars should be from the same host as the source face reference.
        ///    -or-
        ///    The rebars that are group members can't be propagated.
        ///    -or-
        ///    The references should represent faces that have same type of surface.
        ///    -or-
        ///    The source and destination face references should be different.
        ///    -or-
        ///    This method uses its own transaction, so it's not permitted to be invoked in an active transaction.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ISet<ElementId> AlignByFace(Document document, Reference sourceFaceReference, Reference destinationFaceReference)
        {
            return RebarPropagation.AlignByFace(document, sourceRebars, sourceFaceReference, destinationFaceReference);
        }

        /// <summary>
        ///    It will copy the source rebars, will align them in the same way as how the source host is aligned to destination host and will adapt them to the destinaion host.
        /// </summary>
        /// <remarks>
        ///   <p>The source and destination hosts should be of the same category.</p>
        ///   <p>The source and destination hosts should be different elements.</p>
        ///   <p>The destination host must be able to host rebar.</p>
        ///   <p>The source rebars should not be gourp members.</p>
        ///   <p>This method uses its own transaction, so it's not permitted to be invoked in an active transaction.</p>
        /// </remarks>
        /// <param name="document">A document.</param>
        /// <param name="destinationHost">
        ///    The destination host where the new rebar will be created.
        /// </param>
        /// <returns>The newly created rebars.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    There are no source rebars to propagate.
        ///    -or-
        ///    The rebars should be from the same host.
        ///    -or-
        ///    The rebars that are group members can't be propagated.
        ///    -or-
        ///    destinationHost is not a valid rebar host.
        ///    -or-
        ///    The source and destination hosts should be of the same category.
        ///    -or-
        ///    The source and destination hosts must be different elements.
        ///    -or-
        ///    This method uses its own transaction, so it's not permitted to be invoked in an active transaction.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ISet<ElementId> AlignByHost(Document document, Element destinationHost)
        {
            return RebarPropagation.AlignByHost(document, sourceRebars, destinationHost);
        }
    }
}
#endif
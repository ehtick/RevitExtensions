#if REVIT2022_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternallyTaggedGeometryValidation"/> class.
/// </summary>
[PublicAPI]
public static class ExternallyTaggedGeometryValidationExtensions
{
    /// <param name="geometry">The source geometry object.</param>
    extension(GeometryObject geometry)
    {
        /// <summary>Makes sure that the input geometry object is not a Solid.</summary>
        /// <remarks>This validation is different than negating IsSolid().</remarks>
        /// <returns>True if the supplied geometry object is not a Solid.</returns>
        public bool IsNonSolid => ExternallyTaggedGeometryValidation.IsNonSolid(geometry);

        /// <summary>Makes sure that the input geometry object is a Solid.</summary>
        /// <remarks>This validation is different than negating IsNonSolid().</remarks>
        /// <returns>True if the supplied geometry object is a Solid.</returns>
        public bool IsSolid => ExternallyTaggedGeometryValidation.IsSolid(geometry);
#if REVIT2024_OR_GREATER

        /// <summary>Makes sure that the input geometry object does not have sub-nodes.</summary>
        /// <returns>True if the supplied geometry object does not have sub-nodes.</returns>
        public bool LacksSubnodes => ExternallyTaggedGeometryValidation.LacksSubnodes(geometry);
#endif  
    }
}
#endif
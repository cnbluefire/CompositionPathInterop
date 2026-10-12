using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace CompositionPathInterop.Interop
{
    [GeneratedComClass]
    internal partial class GeometryWrapper : ID2D1PathGeometry
    {
        private readonly IGeometryWriter geometry;

        public GeometryWrapper(IGeometryWriter geometry)
        {
            this.geometry = geometry;
        }

        int ID2D1Geometry.CombineWithGeometry_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.CompareWithGeometry_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.ComputeArea_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.ComputeLength_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.ComputePointAtLength_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.FillContainsPoint_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.GetBounds_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Resource.GetFactory_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1PathGeometry.GetFigureCount_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1PathGeometry.GetSegmentCount_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.GetWidenedBounds_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1PathGeometry.Open_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.Outline_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.Simplify_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1PathGeometry.Stream(nint geometrySink)
        {
            if (geometrySink == 0)
            {
                return HResults.E_INVALIDARG;
            }

            using var sinkWrapper = new GeometrySinkWrapper(geometrySink);
            geometry.WriteTo(sinkWrapper);

            return HResults.S_OK;
        }

        int ID2D1Geometry.StrokeContainsPoint_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.Tessellate_Placeholder() => HResults.E_NOTIMPL;

        int ID2D1Geometry.Widen_Placeholder() => HResults.E_NOTIMPL;
    }
}

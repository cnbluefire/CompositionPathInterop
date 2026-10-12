using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace CompositionPathInterop.Interop
{
    [GeneratedComInterface]
    [Guid("2cd90691-12e2-11dc-9fed-001143a055f9")]
    internal partial interface ID2D1Resource
    {
        [PreserveSig] unsafe int GetFactory_Placeholder();

    }

    [GeneratedComInterface]
    [Guid("2cd906a1-12e2-11dc-9fed-001143a055f9")]
    internal partial interface ID2D1Geometry : ID2D1Resource
    {
        [PreserveSig] int GetBounds_Placeholder();
        [PreserveSig] int GetWidenedBounds_Placeholder();
        [PreserveSig] int StrokeContainsPoint_Placeholder();
        [PreserveSig] int FillContainsPoint_Placeholder();
        [PreserveSig] int CompareWithGeometry_Placeholder();
        [PreserveSig] int Simplify_Placeholder();
        [PreserveSig] int Tessellate_Placeholder();
        [PreserveSig] int CombineWithGeometry_Placeholder();
        [PreserveSig] int Outline_Placeholder();
        [PreserveSig] int ComputeArea_Placeholder();
        [PreserveSig] int ComputeLength_Placeholder();
        [PreserveSig] int ComputePointAtLength_Placeholder();
        [PreserveSig] int Widen_Placeholder();
    }

    [GeneratedComInterface]
    [Guid("2cd906a5-12e2-11dc-9fed-001143a055f9")]
    internal partial interface ID2D1PathGeometry : ID2D1Geometry
    {
        [PreserveSig] int Open_Placeholder();
        [PreserveSig] int Stream(nint geometrySink);
        [PreserveSig] int GetSegmentCount_Placeholder();
        [PreserveSig] int GetFigureCount_Placeholder();
    }
}

using CompositionPathInterop.Interop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Windows.Graphics;
using WinRT;

namespace CompositionPathInterop
{
    public static class GeometrySourceFactory
    {
        public unsafe static Windows.Graphics.IGeometrySource2D Create(IGeometryWriter geometry)
        {
            var source = new GeometrySource2D(geometry);
            
            var ptr = (nint)ComInterfaceMarshaller<GeometrySource2D>.ConvertToUnmanaged(source);
            try
            {
                return (Windows.Graphics.IGeometrySource2D)WinRT.IInspectable.FromAbi(ptr);
            }
            finally
            {
                Marshal.Release(ptr);
            }
        }

    }

}

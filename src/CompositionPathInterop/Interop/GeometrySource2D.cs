using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WinRT;

namespace CompositionPathInterop.Interop
{
    [GeneratedComClass]
    internal partial class GeometrySource2D : Interop.IGeometrySource2D, Interop.IGeometrySource2DInterop
    {
        private static readonly nint pIids;
        private static readonly int iidCount;
        private static readonly nint hClassName;
        unsafe static GeometrySource2D()
        {
            iidCount = 4;
            pIids = (nint)NativeMemory.Alloc((nuint)(sizeof(Guid) * iidCount));
            var span = new Span<Guid>((void*)pIids, iidCount);
            span[0] = WinRT.Interop.IID.IID_IUnknown;
            span[1] = WinRT.Interop.IID.IID_IInspectable;
            span[2] = typeof(Interop.IGeometrySource2D).GUID;
            span[3] = typeof(Interop.IGeometrySource2DInterop).GUID;

            hClassName = MarshalString.FromManaged(typeof(GeometrySource2D).FullName);
        }

        private readonly IGeometryWriter geometry;

        internal GeometrySource2D(IGeometryWriter geometry)
        {
            this.geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
        }

        unsafe int Interop.IInspectable.GetIids(int* iidCount, Guid** iids)
        {
            if (iidCount == null || iids == null)
            {
                return HResults.E_INVALIDARG;
            }

            *iidCount = GeometrySource2D.iidCount;
            *iids = (Guid*)pIids;

            return HResults.S_OK;
        }

        unsafe int Interop.IInspectable.GetRuntimeClassName(nint* className)
        {
            if (className == null)
            {
                return HResults.E_INVALIDARG;
            }

            *className = hClassName;
            return HResults.S_OK;
        }

        unsafe int Interop.IInspectable.GetTrustLevel(TrustLevel* trustLevel)
        {
            if (trustLevel == null)
            {
                return HResults.E_INVALIDARG;
            }

            *trustLevel = TrustLevel.BaseTrust;
            return HResults.S_OK;
        }

        unsafe int IGeometrySource2DInterop.GetGeometry(nint* geometry)
        {
            if (geometry == null)
            {
                return HResults.E_INVALIDARG;
            }

            var geometryWrapper = new GeometryWrapper(this.geometry);

            var ptr = (nint)ComInterfaceMarshaller<GeometryWrapper>.ConvertToUnmanaged(geometryWrapper);
            try
            {
                var hr = Marshal.QueryInterface(ptr, typeof(ID2D1Geometry).GUID, out var ppv);
                if (hr != 0) Marshal.ThrowExceptionForHR(hr);
                *geometry = ppv;
                return HResults.S_OK;
            }
            finally
            {
                Marshal.Release(ptr);
            }
        }

        unsafe int IGeometrySource2DInterop.TryGetGeometryUsingFactory(nint factory, nint* geometry)
        {
            return HResults.E_NOTIMPL;
        }

    }
}

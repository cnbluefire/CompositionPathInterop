using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WinRT;

namespace CompositionPathInterop.Interop
{

    [Guid("0657AF73-53FD-47CF-84FF-C8492D2A80A3")]
    [GeneratedComInterface]
    internal partial interface IGeometrySource2DInterop
    {
        [PreserveSig] unsafe int GetGeometry(nint* geometry);

        [PreserveSig] unsafe int TryGetGeometryUsingFactory(nint factory, nint* geometry);
    }

    [Guid("CAFF7902-670C-4181-A624-DA977203B845")]
    [GeneratedComInterface]
    internal unsafe partial interface IGeometrySource2D : IInspectable;

    [Guid("AF86E2E0-B12D-4c6a-9C5A-D7AA65101E90")]
    [GeneratedComInterface]
    internal partial interface IInspectable
    {
        [PreserveSig] unsafe int GetIids(int* iidCount, Guid** iids);

        [PreserveSig] unsafe int GetRuntimeClassName(nint* className);

        [PreserveSig] unsafe int GetTrustLevel(TrustLevel* trustLevel);
    }
}

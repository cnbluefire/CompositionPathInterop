using System;
using System.Collections.Generic;
using System.Text;

namespace CompositionPathInterop.Interop
{
    internal static class HResults
    {
        public const int S_OK = 0;
        public const int E_INVALIDARG = unchecked((int)0x80070057);
        public const int E_NOTIMPL = unchecked((int)0x80004001);
        public const int E_FAIL = unchecked((int)0x80004005);

    }
}

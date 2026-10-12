using System;
using System.Runtime.InteropServices;

namespace CompositionPathInterop.Tests
{
    internal static class NativeMethods
    {
        // ---------------------------------------------------------------
        // CoreMessaging: DispatcherQueue (already used before)
        // ---------------------------------------------------------------
        [StructLayout(LayoutKind.Sequential)]
        private struct DispatcherQueueOptions
        {
            public int dwSize;
            public int threadType;    // DQTYPE_THREAD_CURRENT = 2
            public int apartmentType; // DQTAT_COM_STA = 2
        }

        [DllImport("CoreMessaging.dll", SetLastError = true)]
        private static extern int CreateDispatcherQueueController(
            DispatcherQueueOptions options, out nint dispatcherQueueController);

        /// <summary>
        /// Creates a DispatcherQueue on the current thread and returns an owning
        /// pointer. Compositor requires one on the calling thread.
        /// </summary>
        public static nint CreateDispatcherQueue()
        {
            var options = new DispatcherQueueOptions
            {
                dwSize = Marshal.SizeOf<DispatcherQueueOptions>(),
                threadType = 2,
                apartmentType = 2,
            };

            int hr = CreateDispatcherQueueController(options, out nint controller);
            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
            return controller;
        }

        // ---------------------------------------------------------------
        // DPI awareness
        // ---------------------------------------------------------------
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDpiAwarenessContext(nint value);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDPIAware();

        public static void EnablePerMonitorDpiAwareness()
        {
            // DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 == (HANDLE)-4
            // Only available on Windows 10 1703+. Fall back to system DPI aware.
            try
            {
                if (!SetProcessDpiAwarenessContext(new nint(-4)))
                {
                    SetProcessDPIAware();
                }
            }
            catch (EntryPointNotFoundException)
            {
                SetProcessDPIAware();
            }
        }

        // ---------------------------------------------------------------
        // Win32 window class / window
        // ---------------------------------------------------------------
        public const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
        public const uint WS_EX_TOPMOST = 0x00000008;
        public const int SW_SHOW = 5;
        public const int IDC_ARROW = 32512;
        public const int BLACK_BRUSH = 4;   // stock object index

        public const uint WM_DESTROY = 0x0002;

        public delegate nint WndProcDelegate(nint hWnd, uint msg, nint wParam, nint lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WNDCLASSEXW
        {
            public uint cbSize;
            public uint style;
            public unsafe delegate* unmanaged[Stdcall]<nint, uint, nint, nint, nint> lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public nint hInstance;
            public nint hIcon;
            public nint hCursor;
            public nint hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)] public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)] public string lpszClassName;
            public nint hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT { public int x; public int y; }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public nint hwnd;
            public uint message;
            public nint wParam;
            public nint lParam;
            public uint time;
            public POINT pt;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern ushort RegisterClassExW(ref WNDCLASSEXW lpwcx);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern nint CreateWindowExW(
            uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle,
            int x, int y, int nWidth, int nHeight,
            nint hWndParent, nint hMenu, nint hInstance, nint lpParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyWindow(nint hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(nint hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(nint hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(nint hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern nint DefWindowProcW(nint hWnd, uint msg, nint wParam, nint lParam);

        [DllImport("user32.dll")]
        public static extern nint LoadCursorW(nint hInstance, nint lpCursorName);

        [DllImport("gdi32.dll")]
        public static extern nint GetStockObject(int i);

        [DllImport("user32.dll")]
        public static extern sbyte GetMessageW(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern nint DispatchMessageW(ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);
    }
}

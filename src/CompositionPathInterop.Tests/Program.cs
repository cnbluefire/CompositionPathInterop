using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.System;
using Windows.UI.Composition;
using Windows.UI.Composition.Desktop;

namespace CompositionPathInterop.Tests
{
    internal class Program
    {
        private static readonly string[] PathData = [
            "M12.00 17.27L12.48 17.56L12.97 17.85L13.45 18.14L13.93 18.44L14.41 18.73L14.90 19.02L15.38 19.31L15.86 19.60L16.34 19.89L16.83 20.18L17.31 20.47L17.79 20.77L18.16 20.89L18.03 20.34L17.90 19.80L17.77 19.25L17.64 18.70L17.51 18.15L17.39 17.60L17.26 17.05L17.13 16.50L17.00 15.95L16.87 15.40L16.75 14.85L16.62 14.31L16.71 13.83L17.13 13.46L17.56 13.09L17.98 12.72L18.41 12.35L18.84 11.98L19.26 11.61L19.69 11.24L20.11 10.87L20.54 10.50L20.97 10.14L21.39 9.77L21.82 9.40L21.68 9.21L21.12 9.16L20.55 9.12L19.99 9.07L19.43 9.02L18.87 8.97L18.31 8.93L17.75 8.88L17.18 8.83L16.62 8.78L16.06 8.74L15.50 8.69L14.94 8.64L14.64 8.23L14.42 7.71L14.20 7.19L13.98 6.67L13.76 6.15L13.54 5.63L13.32 5.11L13.10 4.60L12.88 4.08L12.66 3.56L12.44 3.04L12.22 2.52L12.00 2.00L11.78 2.52L11.56 3.04L11.34 3.56L11.12 4.08L10.90 4.60L10.68 5.11L10.46 5.63L10.24 6.15L10.02 6.67L9.80 7.19L9.58 7.71L9.36 8.23L9.06 8.64L8.50 8.69L7.94 8.74L7.38 8.78L6.82 8.83L6.25 8.88L5.69 8.93L5.13 8.97L4.57 9.02L4.01 9.07L3.45 9.12L2.88 9.16L2.32 9.21L2.18 9.40L2.61 9.77L3.03 10.14L3.46 10.50L3.89 10.87L4.31 11.24L4.74 11.61L5.16 11.98L5.59 12.35L6.02 12.72L6.44 13.09L6.87 13.46L7.29 13.83L7.38 14.31L7.25 14.85L7.13 15.40L7.00 15.95L6.87 16.50L6.74 17.05L6.61 17.60L6.49 18.15L6.36 18.70L6.23 19.25L6.10 19.80L5.97 20.34L5.84 20.89L6.21 20.77L6.69 20.47L7.17 20.18L7.66 19.89L8.14 19.60L8.62 19.31L9.10 19.02L9.59 18.73L10.07 18.44L10.55 18.14L11.03 17.85L11.52 17.56Z",
            "M12.00 21.35L11.65 21.03L11.30 20.71L10.94 20.39L10.59 20.07L10.24 19.75L9.89 19.43L9.53 19.11L9.17 18.78L8.83 18.47L8.48 18.14L8.13 17.82L7.78 17.49L7.43 17.16L7.10 16.84L6.75 16.50L6.40 16.16L6.07 15.83L5.75 15.50L5.41 15.14L5.09 14.80L4.77 14.43L4.46 14.07L4.16 13.70L3.87 13.33L3.59 12.94L3.33 12.55L3.08 12.14L2.85 11.73L2.63 11.29L2.46 10.87L2.30 10.41L2.17 9.95L2.08 9.47L2.02 9.01L2.00 8.54L2.02 8.06L2.07 7.59L2.17 7.12L2.30 6.67L2.48 6.22L2.69 5.80L2.94 5.39L3.22 5.01L3.54 4.65L3.89 4.33L4.26 4.03L4.66 3.77L5.08 3.54L5.51 3.36L5.97 3.21L6.43 3.10L6.90 3.03L7.38 3.00L7.85 3.01L8.33 3.06L8.79 3.15L9.25 3.28L9.70 3.44L10.14 3.63L10.55 3.86L10.95 4.13L11.33 4.42L11.68 4.74L12.00 5.09L12.32 4.74L12.67 4.42L13.05 4.13L13.45 3.86L13.86 3.63L14.30 3.44L14.75 3.27L15.21 3.15L15.67 3.06L16.15 3.01L16.62 3.00L17.10 3.03L17.57 3.10L18.03 3.21L18.49 3.36L18.92 3.55L19.34 3.77L19.74 4.03L20.12 4.33L20.46 4.65L20.78 5.01L21.06 5.39L21.31 5.80L21.52 6.22L21.70 6.67L21.83 7.12L21.93 7.59L21.98 8.06L22.00 8.54L21.98 9.01L21.92 9.47L21.83 9.95L21.70 10.41L21.54 10.87L21.37 11.29L21.15 11.73L20.92 12.14L20.67 12.55L20.41 12.94L20.13 13.33L19.84 13.70L19.54 14.07L19.23 14.43L18.91 14.80L18.59 15.15L18.25 15.50L17.93 15.84L17.60 16.17L17.25 16.51L16.91 16.84L16.57 17.16L16.22 17.49L15.87 17.82L15.52 18.15L15.17 18.47L14.83 18.78L14.47 19.11L14.11 19.44L13.76 19.76L13.41 20.07L13.06 20.39L12.71 20.71L12.35 21.03Z",
            "M23.00 5.50L23.00 6.54L23.00 7.58L23.00 8.62L23.00 9.66L23.00 10.69L23.00 11.73L23.00 12.77L23.00 13.81L23.00 14.85L23.00 15.89L23.00 16.93L23.00 17.97L23.00 19.00L23.00 20.04L22.85 21.07L22.45 22.02L21.81 22.84L20.99 23.47L20.03 23.86L19.00 24.00L17.97 24.00L16.93 24.00L15.89 24.00L14.85 24.00L13.81 24.00L12.77 24.00L11.73 24.00L10.70 23.88L9.74 23.49L8.91 22.87L8.18 22.13L7.45 21.39L6.73 20.65L6.00 19.91L5.27 19.17L4.54 18.43L3.81 17.69L3.08 16.95L2.35 16.21L1.63 15.47L1.10 14.73L1.85 14.00L2.66 13.37L3.66 13.44L4.57 13.95L5.47 14.46L6.37 14.98L7.27 15.49L8.00 15.71L8.00 14.67L8.00 13.63L8.00 12.59L8.00 11.55L8.00 10.51L8.00 9.48L8.00 8.44L8.00 7.40L8.00 6.36L8.00 5.32L8.00 4.28L8.19 3.27L8.95 2.60L9.97 2.57L10.77 3.20L11.00 4.20L11.00 5.24L11.00 6.28L11.00 7.31L11.00 8.35L11.00 9.39L11.00 10.43L11.47 11.00L12.00 10.49L12.00 9.45L12.00 8.41L12.00 7.37L12.00 6.34L12.00 5.30L12.00 4.26L12.00 3.22L12.00 2.18L12.04 1.15L12.60 0.30L13.58 0.00L14.52 0.40L14.99 1.30L15.00 2.34L15.00 3.38L15.00 4.42L15.00 5.45L15.00 6.49L15.00 7.53L15.00 8.57L15.00 9.61L15.00 10.65L15.69 11.00L16.00 10.27L16.00 9.24L16.00 8.20L16.00 7.16L16.00 6.12L16.00 5.08L16.00 4.04L16.00 3.00L16.09 1.97L16.75 1.20L17.76 1.02L18.64 1.52L19.00 2.48L19.00 3.52L19.00 4.55L19.00 5.59L19.00 6.63L19.00 7.67L19.00 8.71L19.00 9.75L19.00 10.79L19.83 11.00L20.00 10.13L20.00 9.10L20.00 8.06L20.00 7.02L20.00 5.98L20.10 4.95L20.77 4.19L21.78 4.03L22.66 4.54Z",
            "M21.00 16.00L21.00 15.36L21.00 14.72L21.00 14.09L20.53 13.71L19.99 13.37L19.45 13.03L18.91 12.69L18.37 12.36L17.83 12.02L17.29 11.68L16.75 11.34L16.21 11.00L15.67 10.67L15.13 10.33L14.58 9.99L14.04 9.65L13.50 9.31L13.00 8.96L13.00 8.32L13.00 7.68L13.00 7.04L13.00 6.40L13.00 5.77L13.00 5.13L13.00 4.49L13.00 3.85L12.97 3.22L12.73 2.63L12.26 2.21L11.66 2.01L11.03 2.07L10.48 2.39L10.12 2.91L10.00 3.53L10.00 4.17L10.00 4.81L10.00 5.45L10.00 6.08L10.00 6.72L10.00 7.36L10.00 8.00L10.00 8.63L9.77 9.14L9.23 9.48L8.69 9.82L8.15 10.16L7.61 10.50L7.06 10.83L6.52 11.17L5.98 11.51L5.44 11.85L4.90 12.19L4.36 12.52L3.82 12.86L3.28 13.20L2.74 13.54L2.20 13.88L2.00 14.40L2.00 15.04L2.00 15.68L2.30 15.91L2.91 15.72L3.52 15.52L4.13 15.33L4.74 15.14L5.35 14.95L5.95 14.76L6.56 14.57L7.17 14.38L7.78 14.19L8.39 14.00L9.00 13.81L9.61 13.62L10.00 13.73L10.00 14.36L10.00 15.00L10.00 15.64L10.00 16.28L10.00 16.91L10.00 17.55L10.00 18.19L10.00 18.83L9.63 19.28L9.12 19.66L8.61 20.04L8.10 20.43L8.00 21.02L8.00 21.65L8.28 21.92L8.89 21.74L9.51 21.57L10.12 21.39L10.73 21.22L11.35 21.04L11.96 21.13L12.57 21.31L13.19 21.48L13.80 21.66L14.41 21.83L15.00 21.97L15.00 21.34L15.00 20.70L14.65 20.24L14.14 19.85L13.63 19.47L13.12 19.09L13.00 18.51L13.00 17.87L13.00 17.23L13.00 16.60L13.00 15.96L13.00 15.32L13.00 14.68L13.00 14.05L13.09 13.53L13.70 13.72L14.30 13.91L14.91 14.10L15.52 14.29L16.13 14.48L16.74 14.67L17.35 14.86L17.96 15.05L18.57 15.24L19.17 15.43L19.78 15.62L20.39 15.81Z",
            "M7.00 2.00L7.00 2.47L7.00 2.93L7.00 3.40L7.00 3.87L7.00 4.34L7.00 4.80L7.00 5.27L7.00 5.74L7.00 6.21L7.00 6.67L7.00 7.14L7.00 7.61L7.00 8.08L7.00 8.54L7.00 9.01L7.00 9.48L7.00 9.95L7.00 10.41L7.00 10.88L7.00 11.35L7.00 11.82L7.00 12.28L7.00 12.75L7.22 13.00L7.69 13.00L8.15 13.00L8.62 13.00L9.09 13.00L9.56 13.00L10.00 13.02L10.00 13.49L10.00 13.96L10.00 14.43L10.00 14.89L10.00 15.36L10.00 15.83L10.00 16.30L10.00 16.76L10.00 17.23L10.00 17.70L10.00 18.17L10.00 18.63L10.00 19.10L10.00 19.57L10.00 20.04L10.00 20.50L10.00 20.97L10.00 21.44L10.00 21.91L10.19 21.68L10.42 21.27L10.66 20.87L10.89 20.47L11.13 20.06L11.37 19.66L11.60 19.25L11.84 18.85L12.07 18.45L12.31 18.04L12.54 17.64L12.78 17.24L13.01 16.83L13.25 16.43L13.49 16.02L13.72 15.62L13.96 15.22L14.19 14.81L14.43 14.41L14.66 14.01L14.90 13.60L15.13 13.20L15.37 12.79L15.61 12.39L15.84 11.99L16.08 11.58L16.31 11.18L16.55 10.77L16.78 10.37L16.96 10.00L16.49 10.00L16.03 10.00L15.56 10.00L15.09 10.00L14.62 10.00L14.16 10.00L13.69 10.00L13.22 10.00L13.11 9.78L13.32 9.36L13.53 8.94L13.74 8.53L13.95 8.11L14.16 7.69L14.36 7.27L14.57 6.85L14.78 6.44L14.99 6.02L15.20 5.60L15.41 5.18L15.62 4.76L15.83 4.35L16.04 3.93L16.25 3.51L16.45 3.09L16.66 2.67L16.87 2.25L16.82 2.00L16.35 2.00L15.88 2.00L15.41 2.00L14.95 2.00L14.48 2.00L14.01 2.00L13.54 2.00L13.08 2.00L12.61 2.00L12.14 2.00L11.67 2.00L11.21 2.00L10.74 2.00L10.27 2.00L9.80 2.00L9.34 2.00L8.87 2.00L8.40 2.00L7.93 2.00L7.47 2.00Z",
            "M12.00 3.00L12.00 3.43L12.00 3.87L12.00 4.30L12.00 4.73L12.00 5.17L12.00 5.60L12.00 6.04L12.00 6.47L12.00 6.90L12.00 7.34L12.00 7.77L12.00 8.20L12.00 8.64L12.00 9.07L12.00 9.50L12.00 9.94L12.00 10.37L12.00 10.80L12.00 11.24L12.00 11.67L12.00 12.11L12.00 12.54L12.00 12.97L12.00 13.41L11.74 13.41L11.35 13.24L10.93 13.12L10.51 13.03L10.07 13.00L9.64 13.02L9.21 13.08L8.79 13.19L8.39 13.34L8.00 13.53L7.64 13.77L7.30 14.05L7.00 14.36L6.73 14.70L6.50 15.06L6.31 15.45L6.16 15.86L6.06 16.28L6.01 16.71L6.00 17.15L6.04 17.58L6.13 18.00L6.26 18.42L6.43 18.81L6.65 19.19L6.91 19.54L7.20 19.86L7.52 20.14L7.88 20.39L8.26 20.60L8.66 20.77L9.07 20.89L9.50 20.97L9.93 21.00L10.36 20.98L10.79 20.92L11.21 20.81L11.62 20.66L12.01 20.46L12.37 20.22L12.70 19.95L13.01 19.64L13.27 19.30L13.50 18.93L13.69 18.54L13.84 18.13L13.94 17.71L13.99 17.28L14.00 16.85L14.00 16.41L14.00 15.98L14.00 15.55L14.00 15.11L14.00 14.68L14.00 14.25L14.00 13.81L14.00 13.38L14.00 12.95L14.00 12.51L14.00 12.08L14.00 11.65L14.00 11.21L14.00 10.78L14.00 10.34L14.00 9.91L14.00 9.48L14.00 9.04L14.00 8.61L14.00 8.18L14.00 7.74L14.00 7.31L14.12 7.00L14.56 7.00L14.99 7.00L15.43 7.00L15.86 7.00L16.29 7.00L16.73 7.00L17.16 7.00L17.59 7.00L18.00 6.97L18.00 6.54L18.00 6.11L18.00 5.67L18.00 5.24L18.00 4.80L18.00 4.37L18.00 3.94L18.00 3.50L18.00 3.07L17.64 3.00L17.20 3.00L16.77 3.00L16.34 3.00L15.90 3.00L15.47 3.00L15.04 3.00L14.60 3.00L14.17 3.00L13.73 3.00L13.30 3.00L12.87 3.00L12.43 3.00Z",
        ];

        static void Main(string[] args)
        {
            NativeMethods.CreateDispatcherQueue();

            var compositor = new Compositor();

            var hWnd = ShowWindow(100, 100, 400, 400);
            var root = BuildCompositionTarget(hWnd, compositor);
            BuildCompositionGeometry(compositor, root);

            PumpMessages();
        }

        public static nint ShowWindow(int x, int y, int width, int height)
        {
            NativeMethods.EnablePerMonitorDpiAwareness();

            var wc = new NativeMethods.WNDCLASSEXW
            {
                cbSize = (uint)Marshal.SizeOf<NativeMethods.WNDCLASSEXW>(),
                style = 0,
                lpfnWndProc = &StaticWndProc,
                hCursor = NativeMethods.LoadCursorW(default, (nint)NativeMethods.IDC_ARROW),
                hbrBackground = NativeMethods.GetStockObject(NativeMethods.BLACK_BRUSH),
                lpszClassName = "CompositionPathInterop",
            };

            ushort atom = NativeMethods.RegisterClassExW(ref wc);
            if (atom == 0 && Marshal.GetLastWin32Error() != 1410 /* ERROR_CLASS_ALREADY_EXISTS */)
            {
                throw new InvalidOperationException("RegisterClassExW failed: " + Marshal.GetLastWin32Error());
            }

            var hWnd = NativeMethods.CreateWindowExW(
                NativeMethods.WS_EX_TOPMOST,
                "CompositionPathInterop",
                "CompositionPathInterop",
                NativeMethods.WS_OVERLAPPEDWINDOW,
                x, y, width, height,
                default, default, default, default);
            if (hWnd == default)
            {
                throw new InvalidOperationException("CreateWindowExW failed: " + Marshal.GetLastWin32Error());
            }

            NativeMethods.ShowWindow(hWnd, NativeMethods.SW_SHOW);
            NativeMethods.UpdateWindow(hWnd);

            return hWnd;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static nint StaticWndProc(nint hWnd, uint msg, nint wParam, nint lParam)
        {
            if (msg == NativeMethods.WM_DESTROY)
            {
                NativeMethods.PostQuitMessage(0);
                return default;
            }
            return NativeMethods.DefWindowProcW(hWnd, msg, wParam, lParam);
        }

        private static void PumpMessages()
        {
            while (NativeMethods.GetMessageW(out var msg, default, 0, 0) != 0)
            {
                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessageW(ref msg);
            }
        }

        private static unsafe ContainerVisual BuildCompositionTarget(nint hWnd, Compositor compositor)
        {
            Guid IID_ICompositorDesktopInterop = new Guid("29E691FA-4567-4DCA-B319-D0F207EB6807");

            // 1) Obtain the raw IUnknown* of the projected Compositor and QI for
            //    ICompositorDesktopInterop.
            nint pCompositor = WinRT.MarshalInspectable<Compositor>.FromManaged(compositor);
            Guid iid = IID_ICompositorDesktopInterop;
            int hr = Marshal.QueryInterface(pCompositor, in iid, out nint pInterop);
            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            // 2) CreateDesktopWindowTarget(HWND, BOOL isTopmost, IDesktopWindowTarget**)
            nint pTarget;
            var createDesktopWindowTarget =
                (delegate* unmanaged[Stdcall]<nint, nint, int, nint*, int>)(*(void***)pInterop)[3];
            hr = createDesktopWindowTarget(pInterop, hWnd, 1 /*isTopmost*/, &pTarget);
            Marshal.Release(pInterop);
            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            var target = DesktopWindowTarget.FromAbi(pTarget);

            // 3) Build the visual tree purely out of Composition objects.
            var root = compositor.CreateContainerVisual();
            root.RelativeSizeAdjustment = Vector2.One;

            // 3a) A solid, full-window background. If this shows up we know the
            //     Composition/DComp path is live and on top of the GDI window.
            var background = compositor.CreateSpriteVisual();
            background.RelativeSizeAdjustment = Vector2.One;
            background.Brush = compositor.CreateColorBrush(Windows.UI.Color.FromArgb(255, 24, 70, 200));
            root.Children.InsertAtTop(background);

            target.Root = root;

            return root;
        }

        private static void BuildCompositionGeometry(Compositor compositor, ContainerVisual root)
        {
            var pathGeometry = compositor.CreatePathGeometry();
            var shape = compositor.CreateSpriteShape(pathGeometry);
            shape.FillBrush = compositor.CreateColorBrush(Windows.UI.Color.FromArgb(255, 230, 30, 30));

            var shapeVisual = compositor.CreateShapeVisual();
            shapeVisual.Size = new Vector2(24, 24);
            shapeVisual.Scale = new Vector3(10f, 10f, 1);
            shapeVisual.Shapes.Add(shape);
            root.Children.InsertAtTop(shapeVisual);


            var linearEasingFunction = compositor.CreateLinearEasingFunction();
            var animation = compositor.CreatePathKeyFrameAnimation();

            for (int i = 0; i <= PathData.Length; i++)
            {
                var path = PathData[i < PathData.Length ? i : 0];
                var geometrySource = GeometrySourceFactory.Create(new PathGeometryWriter(path));
                var compositionPath = new CompositionPath(geometrySource);
                animation.InsertKeyFrame(1f / PathData.Length * i, compositionPath, linearEasingFunction);
            }

            animation.Duration = TimeSpan.FromSeconds(PathData.Length * 2);
            animation.IterationBehavior = AnimationIterationBehavior.Forever;

            pathGeometry.StartAnimation("Path", animation);
        }
    }
}

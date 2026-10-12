using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace CompositionPathInterop.Interop
{
    internal unsafe static class ID2D1SimplifiedGeometrySinkMethods
    {
        public static void SetFillMode(nint thisPtr, FillMode fillMode) =>
            ((delegate* unmanaged[Stdcall]<nint, FillMode, void>)(*(void***)thisPtr)[3])(thisPtr, fillMode);

        public static void SetSegmentFlags(nint thisPtr, SegmentFlags vertexFlags) =>
            ((delegate* unmanaged[Stdcall]<nint, SegmentFlags, void>)(*(void***)thisPtr)[4])(thisPtr, vertexFlags);

        public static void BeginFigure(nint thisPtr, PointF startPoint, FigureBegin figureBegin) =>
            ((delegate* unmanaged[Stdcall]<nint, PointF, FigureBegin, void>)(*(void***)thisPtr)[5])(thisPtr, startPoint, figureBegin);

        public static void AddLines(nint thisPtr, PointF* points, uint pointsCount) =>
            ((delegate* unmanaged[Stdcall]<nint, PointF*, uint, void>)(*(void***)thisPtr)[6])(thisPtr, points, pointsCount);

        public static void AddBeziers(nint thisPtr, BezierSegment* beziers, uint beziersCount) =>
            ((delegate* unmanaged[Stdcall]<nint, BezierSegment*, uint, void>)(*(void***)thisPtr)[7])(thisPtr, beziers, beziersCount);

        public static void EndFigure(nint thisPtr, FigureEnd figureEnd) =>
            ((delegate* unmanaged[Stdcall]<nint, FigureEnd, void>)(*(void***)thisPtr)[8])(thisPtr, figureEnd);

        public static int Close(nint thisPtr) =>
            ((delegate* unmanaged[Stdcall]<nint, int>)(*(void***)thisPtr)[9])(thisPtr);
    }

    internal unsafe static class ID2D1GeometrySinkMethods
    {
        public static void AddLine(nint thisPtr, PointF point) =>
            ((delegate* unmanaged[Stdcall]<nint, PointF, void>)(*(void***)thisPtr)[10])(thisPtr, point);

        public static void AddBezier(nint thisPtr, BezierSegment* bezier) =>
            ((delegate* unmanaged[Stdcall]<nint, BezierSegment*, void>)(*(void***)thisPtr)[11])(thisPtr, bezier);

        public static void AddQuadraticBezier(nint thisPtr, QuadraticBezierSegment* bezier) =>
            ((delegate* unmanaged[Stdcall]<nint, QuadraticBezierSegment*, void>)(*(void***)thisPtr)[12])(thisPtr, bezier);

        public static void AddQuadraticBeziers(nint thisPtr, QuadraticBezierSegment* beziers, uint beziersCount) =>
            ((delegate* unmanaged[Stdcall]<nint, QuadraticBezierSegment*, uint, void>)(*(void***)thisPtr)[13])(thisPtr, beziers, beziersCount);

        public static void AddArc(nint thisPtr, ArcSegment* arc) =>
            ((delegate* unmanaged[Stdcall]<nint, ArcSegment*, void>)(*(void***)thisPtr)[14])(thisPtr, arc);
    }
}

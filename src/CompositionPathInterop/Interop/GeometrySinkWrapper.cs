using System;
using System.Collections.Generic;
using System.Text;

namespace CompositionPathInterop.Interop
{
    internal class GeometrySinkWrapper : IGeometrySink, IDisposable
    {
        private nint geometrySink;

        public GeometrySinkWrapper(nint geometrySink)
        {
            this.geometrySink = geometrySink;
        }

        public unsafe void AddArc(in ArcSegment arc)
        {
            fixed (ArcSegment* pArc = &arc)
            {
                var thisPtr = geometrySink;
                if (thisPtr != 0)
                    ID2D1GeometrySinkMethods.AddArc(thisPtr, pArc);
            }
        }

        public unsafe void AddBezier(in BezierSegment bezier)
        {
            fixed (BezierSegment* pBezier = &bezier)
            {
                var thisPtr = geometrySink;
                if (thisPtr != 0)
                    ID2D1GeometrySinkMethods.AddBezier(thisPtr, pBezier);
            }
        }

        public unsafe void AddBeziers(in ReadOnlySpan<BezierSegment> beziers)
        {
            fixed (BezierSegment* pBeziers = beziers)
            {
                var thisPtr = geometrySink;
                if (thisPtr != 0)
                    ID2D1SimplifiedGeometrySinkMethods.AddBeziers(thisPtr, pBeziers, (uint)beziers.Length);
            }
        }

        public void AddLine(in PointF point)
        {
            var thisPtr = geometrySink;
            if (thisPtr != 0)
                ID2D1GeometrySinkMethods.AddLine(thisPtr, point);
        }

        public unsafe void AddLines(in ReadOnlySpan<PointF> points)
        {
            fixed (PointF* pointsPtr = points)
            {
                var thisPtr = geometrySink;
                if (thisPtr != 0)
                    ID2D1SimplifiedGeometrySinkMethods.AddLines(thisPtr, pointsPtr, (uint)points.Length);
            }
        }

        public unsafe void AddQuadraticBezier(in QuadraticBezierSegment bezier)
        {
            fixed (QuadraticBezierSegment* pBezier = &bezier)
            {
                var thisPtr = geometrySink;
                if (thisPtr != 0)
                    ID2D1GeometrySinkMethods.AddQuadraticBezier(thisPtr, pBezier);
            }
        }

        public unsafe void AddQuadraticBeziers(in ReadOnlySpan<QuadraticBezierSegment> beziers)
        {
            fixed (QuadraticBezierSegment* pBeziers = beziers)
            {
                var thisPtr = geometrySink;
                if (thisPtr != 0)
                    ID2D1GeometrySinkMethods.AddQuadraticBeziers(thisPtr, pBeziers, (uint)beziers.Length);
            }
        }

        public void BeginFigure(in PointF startPoint, FigureBegin figureBegin)
        {
            var thisPtr = geometrySink;
            if (thisPtr != 0)
                ID2D1SimplifiedGeometrySinkMethods.BeginFigure(thisPtr, startPoint, figureBegin);
        }

        public void EndFigure(FigureEnd figureEnd)
        {
            var thisPtr = geometrySink;
            if (thisPtr != 0)
                ID2D1SimplifiedGeometrySinkMethods.EndFigure(thisPtr, figureEnd);
        }

        public void SetFillMode(FillMode fillMode)
        {
            var thisPtr = geometrySink;
            if (thisPtr != 0)
                ID2D1SimplifiedGeometrySinkMethods.SetFillMode(thisPtr, fillMode);
        }

        public void SetSegmentFlags(SegmentFlags vertexFlags)
        {
            var thisPtr = geometrySink;
            if (thisPtr != 0)
                ID2D1SimplifiedGeometrySinkMethods.SetSegmentFlags(thisPtr, vertexFlags);
        }


        public void Dispose()
        {
            var thisPtr = Interlocked.Exchange(ref geometrySink, 0);
            if (thisPtr != 0)
                ID2D1SimplifiedGeometrySinkMethods.Close(thisPtr);
        }

    }
}

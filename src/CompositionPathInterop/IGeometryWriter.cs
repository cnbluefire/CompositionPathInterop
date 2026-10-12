using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CompositionPathInterop
{
    public interface IGeometryWriter
    {
        void WriteTo(IGeometrySink sink);
    }

    public enum FillMode : int
    {
        Alternate,
        Winding
    }

    [Flags]
    public enum SegmentFlags : int
    {
        None = 0x0,
        ForceUnstroked = 0x1,
        ForceRoundLineJoin = 0x2
    }

    public enum FigureBegin : int
    {
        Filled = 0,
        Hollow = 1
    }

    public enum FigureEnd : int
    {
        Open = 0,
        Closed = 1
    }

    public enum SweepDirection : int
    {
        CounterClockwise = 0,
        Clockwise = 1,
    }

    public enum ArcSize : int
    {
        SizeSmall = 0,
        SizeLarge = 1
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PointF
    {
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SizeF
    {
        public float Width;
        public float Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct QuadraticBezierSegment
    {
        public PointF Point1;
        public PointF Point2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BezierSegment
    {
        public PointF Point1;
        public PointF Point2;
        public PointF Point3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ArcSegment
    {
        public PointF Point;
        public SizeF Size;
        public float RotationAngle;
        public SweepDirection SweepDirection;
        public ArcSize ArcSize;
    }
}

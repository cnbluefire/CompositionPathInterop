namespace CompositionPathInterop
{
    public interface IGeometrySink
    {
        void SetFillMode(FillMode fillMode);

        void SetSegmentFlags(SegmentFlags vertexFlags);

        void BeginFigure(in PointF startPoint, FigureBegin figureBegin);

        void AddLine(in PointF point);

        void AddBezier(in BezierSegment bezier);

        void AddQuadraticBezier(in QuadraticBezierSegment bezier);

        void AddArc(in ArcSegment arc);

        void AddLines(in ReadOnlySpan<PointF> points);

        void AddBeziers(in ReadOnlySpan<BezierSegment> beziers);

        void AddQuadraticBeziers(in ReadOnlySpan<QuadraticBezierSegment> beziers);

        void EndFigure(FigureEnd figureEnd);
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CompositionPathInterop.Tests
{
    public sealed class PathGeometryWriter : IGeometryWriter
    {
        public PathGeometryWriter(string pathData, FillMode defaultFillMode = FillMode.Winding)
        {
            PathData = pathData;
            DefaultFillMode = defaultFillMode;
        }

        public string PathData { get; }

        public FillMode DefaultFillMode { get; }

        public void WriteTo(IGeometrySink sink)
        {
            GeometrySinkPathParser.Parse(PathData, sink, DefaultFillMode);
        }

    }

    internal static class GeometrySinkPathParser
    {
        public static void Parse(string pathData, IGeometrySink sink, FillMode defaultFillMode = FillMode.Winding)
        {
            sink.SetFillMode(defaultFillMode);
            sink.SetSegmentFlags(SegmentFlags.None);

            var parser = new Parser(pathData, sink);
            parser.Parse();
        }

        private class Parser
        {
            private readonly string _data;
            private readonly IGeometrySink _sink;
            private int _pos;

            private char _command;
            private char _lastCommand;
            private PointF _current;
            private PointF _start;
            private PointF _lastControlPoint;
            private bool _hasLastControlPoint;
            private bool _figureOpen;

            public Parser(string data, IGeometrySink sink)
            {
                _data = data;
                _sink = sink;
            }

            public void Parse()
            {
                while (_pos < _data.Length)
                {
                    SkipWhitespaceAndCommas();
                    if (_pos >= _data.Length)
                        break;

                    char c = _data[_pos];

                    // F0 / F1 填充规则
                    if (c == 'F')
                    {
                        _pos++; // 跳过 F
                        int fill = (int)ReadNumber();
                        _sink.SetFillMode(fill == 0 ? FillMode.Alternate : FillMode.Winding);
                        continue;
                    }

                    if (IsCommand(c))
                    {
                        _command = c;
                        _pos++;
                    }
                    else
                    {
                        if (_lastCommand == '\0')
                            throw new FormatException($"路径必须以命令开头，位置 {_pos}");

                        _command = _lastCommand;
                    }

                    switch (char.ToUpperInvariant(_command))
                    {
                        case 'M':
                            ParseMoveTo();
                            break;
                        case 'L':
                            ParseLineTo();
                            break;
                        case 'H':
                            ParseHorizontalLineTo();
                            break;
                        case 'V':
                            ParseVerticalLineTo();
                            break;
                        case 'C':
                            ParseCubicBezierTo();
                            break;
                        case 'S':
                            ParseSmoothCubicBezierTo();
                            break;
                        case 'Z':
                            ParseClosePath();
                            break;
                        default:
                            throw new FormatException($"暂不支持的命令: {_command}");
                    }

                    _lastCommand = _command;
                }

                if (_figureOpen)
                {
                    _sink.EndFigure(FigureEnd.Open);
                    _figureOpen = false;
                }
            }

            private void ParseMoveTo()
            {
                bool relative = _command == 'm';

                PointF p = ReadPoint(relative);

                if (_figureOpen)
                {
                    _sink.EndFigure(FigureEnd.Open);
                    _figureOpen = false;
                }

                _sink.BeginFigure(p, FigureBegin.Filled);
                _figureOpen = true;

                _current = p;
                _start = p;
                _hasLastControlPoint = false;

                // M 后面如果还有数字，按隐式 LineTo 处理
                while (HasMoreNumbers())
                {
                    p = ReadPoint(relative);
                    _sink.AddLine(p);
                    _current = p;
                }
            }

            private void ParseLineTo()
            {
                bool relative = _command == 'l';

                do
                {
                    PointF p = ReadPoint(relative);
                    _sink.AddLine(p);
                    _current = p;
                    _hasLastControlPoint = false;
                }
                while (HasMoreNumbers());
            }

            private void ParseHorizontalLineTo()
            {
                bool relative = _command == 'h';

                do
                {
                    float x = ReadNumber();
                    if (relative)
                        x += _current.X;

                    PointF p = new PointF { X = x, Y = _current.Y };
                    _sink.AddLine(p);
                    _current = p;
                    _hasLastControlPoint = false;
                }
                while (HasMoreNumbers());
            }

            private void ParseVerticalLineTo()
            {
                bool relative = _command == 'v';

                do
                {
                    float y = ReadNumber();
                    if (relative)
                        y += _current.Y;

                    PointF p = new PointF { X = _current.X, Y = y };
                    _sink.AddLine(p);
                    _current = p;
                    _hasLastControlPoint = false;
                }
                while (HasMoreNumbers());
            }

            private void ParseCubicBezierTo()
            {
                bool relative = _command == 'c';

                do
                {
                    PointF p1 = ReadPoint(relative);
                    PointF p2 = ReadPoint(relative);
                    PointF p3 = ReadPoint(relative);

                    _sink.AddBezier(new BezierSegment
                    {
                        Point1 = p1,
                        Point2 = p2,
                        Point3 = p3
                    });

                    _current = p3;
                    _lastControlPoint = p2;
                    _hasLastControlPoint = true;
                    _lastCommand = _command; // 让连续 S/s 能正确反射
                }
                while (HasMoreNumbers());
            }

            private void ParseSmoothCubicBezierTo()
            {
                bool relative = _command == 's';

                do
                {
                    PointF p2 = ReadPoint(relative);
                    PointF p3 = ReadPoint(relative);

                    PointF p1;
                    if (_hasLastControlPoint &&
                        (char.ToUpperInvariant(_lastCommand) == 'C' ||
                         char.ToUpperInvariant(_lastCommand) == 'S'))
                    {
                        // 反射上一个控制点
                        p1 = new PointF
                        {
                            X = 2 * _current.X - _lastControlPoint.X,
                            Y = 2 * _current.Y - _lastControlPoint.Y
                        };
                    }
                    else
                    {
                        p1 = _current;
                    }

                    _sink.AddBezier(new BezierSegment
                    {
                        Point1 = p1,
                        Point2 = p2,
                        Point3 = p3
                    });

                    _current = p3;
                    _lastControlPoint = p2;
                    _hasLastControlPoint = true;
                    _lastCommand = _command;
                }
                while (HasMoreNumbers());
            }

            private void ParseClosePath()
            {
                if (_figureOpen)
                {
                    _sink.EndFigure(FigureEnd.Closed);
                    _figureOpen = false;
                }

                _current = _start;
                _hasLastControlPoint = false;
            }

            private PointF ReadPoint(bool relative)
            {
                float x = ReadNumber();
                float y = ReadNumber();

                if (relative)
                {
                    x += _current.X;
                    y += _current.Y;
                }

                return new PointF { X = x, Y = y };
            }

            private float ReadNumber()
            {
                SkipWhitespaceAndCommas();

                int start = _pos;

                if (_pos < _data.Length && (_data[_pos] == '+' || _data[_pos] == '-'))
                    _pos++;

                bool hasDot = false;

                while (_pos < _data.Length)
                {
                    char c = _data[_pos];

                    if (c >= '0' && c <= '9')
                    {
                        _pos++;
                    }
                    else if (c == '.' && !hasDot)
                    {
                        hasDot = true;
                        _pos++;
                    }
                    else if (c == 'e' || c == 'E')
                    {
                        _pos++;
                        if (_pos < _data.Length && (_data[_pos] == '+' || _data[_pos] == '-'))
                            _pos++;

                        while (_pos < _data.Length && char.IsDigit(_data[_pos]))
                            _pos++;

                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (start == _pos)
                    throw new FormatException($"期望数字，位置 {_pos}");

                string numStr = _data.Substring(start, _pos - start);
                return float.Parse(numStr, CultureInfo.InvariantCulture);
            }

            private bool HasMoreNumbers()
            {
                SkipWhitespaceAndCommas();

                if (_pos >= _data.Length)
                    return false;

                char c = _data[_pos];
                return c == '-' || c == '+' || c == '.' || char.IsDigit(c);
            }

            private void SkipWhitespaceAndCommas()
            {
                while (_pos < _data.Length &&
                       (char.IsWhiteSpace(_data[_pos]) || _data[_pos] == ','))
                {
                    _pos++;
                }
            }

            private static bool IsCommand(char c)
            {
                return "MmLlHhVvCcSsQqTtAaZzFf".IndexOf(c) >= 0;
            }
        }
    }
}

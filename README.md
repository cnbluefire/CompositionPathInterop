# CompositionPathInterop

A lightweight .NET interop library for creating a `CompositionPath` **without going through Win2D or directly using Direct2D**.

By implementing your own `IGeometryWriter`, you can issue drawing commands to an `IGeometrySink` inside `WriteTo`, and the library turns them into a `CompositionPath`.

## Features

- No Win2D dependency
- No direct Direct2D API usage
- COM interop implementation of `IGeometrySource2D`
- Custom geometry sources supported, such as SVG path parsers or geometry converters
- Target framework: `.NET 10.0-windows10.0.26100.0`

## How It Works

The `CompositionPath` constructor expects an `IGeometrySource2D` object.  
Normally, this object is provided by Win2D, which internally wraps Direct2D. This project instead implements the required COM interfaces and provides a `GeometrySourceFactory` that converts your drawing instructions into a COM-compatible geometry source.

Core interfaces:

- `IGeometryWriter`: Defines how geometry data is written to an `IGeometrySink`.
- `IGeometrySink`: Receives drawing commands similar to `ID2D1GeometrySink`, including `BeginFigure`, `AddLine`, `AddBezier`, `EndFigure`, and others.
- `GeometrySourceFactory.Create(IGeometryWriter)`: Creates an `IGeometrySource2D` instance from an `IGeometryWriter`.

## Getting Started

### 1. Implement Your Own `IGeometryWriter`

```csharp
public sealed class MyGeometryWriter : IGeometryWriter
{
    public void WriteTo(IGeometrySink sink)
    {
        sink.SetFillMode(FillMode.Winding);

        sink.BeginFigure(new PointF(0, 0), FigureBegin.Filled);
        sink.AddLine(new PointF(100, 0));
        sink.AddLine(new PointF(100, 100));
        sink.AddLine(new PointF(0, 100));
        sink.EndFigure(FigureEnd.Closed);
    }
}
```

### 2. Create a `CompositionPath`

```csharp
var geometrySource = GeometrySourceFactory.Create(new MyGeometryWriter());
var compositionPath = new CompositionPath(geometrySource);
```

### 3. Built-in Example: `PathGeometryWriter`

The test project includes a `PathGeometryWriter` that parses SVG path data strings:

```csharp
var writer = new PathGeometryWriter("M 0,0 L 100,0 L 100,100 Z");
var geometrySource = GeometrySourceFactory.Create(writer);
var compositionPath = new CompositionPath(geometrySource);
```

## Project Structure

```text
src/
├── CompositionPathInterop.slnx
├── CompositionPathInterop/
│   ├── CompositionPathInterop.csproj
│   ├── GeometrySource.cs
│   ├── IGeometrySink.cs
│   ├── IGeometryWriter.cs
│   └── Interop/
│       ├── ComInterop.cs
│       ├── GeometrySinkWrapper.cs
│       ├── GeometrySource2D.cs
│       ├── GeometryWrapper.cs
│       ├── HResults.cs
│       ├── ID2D1Geometry.cs
│       ├── ID2D1GeometrySink.cs
│       └── IGeometrySource2D.cs
└── CompositionPathInterop.Tests/
    ├── CompositionPathInterop.Tests.csproj
    ├── NativeMethods.cs
    ├── PathGeometryWriter.cs
    └── Program.cs
```

> `bin` and `obj` directories are intentionally omitted.

## Build and Run

Make sure the .NET 10 SDK, or an SDK supporting `net10.0-windows10.0.26100.0`, is installed.

```bash
dotnet build src/CompositionPathInterop.slnx
```

Run the test program:

```bash
dotnet run --project src/CompositionPathInterop.Tests
```

The test program demonstrates how to create a `CompositionPath` using `PathGeometryWriter`.

## Use Cases

- Reducing application size by avoiding a Win2D dependency
- Generating a `CompositionPath` from custom formats such as SVG, XAML Path, or geometry algorithm output
- Precisely controlling geometry generation without extra Direct2D abstractions

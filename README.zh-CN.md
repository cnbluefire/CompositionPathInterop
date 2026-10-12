# CompositionPathInterop

一个轻量的 .NET 互操作库，用于**不经过 Direct2D 和 Win2D**，直接创建 `Windows.UI.Composition.CompositionPath`。

通过实现自定义的 `IGeometryWriter`，你可以在 `WriteTo` 方法中调用 `IGeometrySink` 提供的方法来描述几何图形，最终生成 `CompositionPath`，从而避免引入 Win2D 或直接调用 D2D API。

## 特性

- 不依赖 Win2D
- 不直接调用 Direct2D
- 通过 COM 互操作实现 `IGeometrySource2D`
- 可自定义几何数据源（例如解析 SVG 路径、从其他格式转换等）
- 目标框架：`.NET 10.0-windows10.0.26100.0`（可向下兼容）

## 工作原理

Windows Composition 的 `CompositionPath` 构造函数需要一个 `IGeometrySource2D` 对象。通常这个对象由 Win2D 提供（内部封装 Direct2D）。  
本项目通过实现 `IGeometrySource2D` 的 COM 接口，并提供一个 `GeometrySourceFactory`，将你的几何绘制指令直接转换为 COM 可识别的几何源。

核心接口：

- `IGeometryWriter`：定义如何将几何数据写入 `IGeometrySink`。
- `IGeometrySink`：接收绘制指令（类似 `ID2D1GeometrySink`），包含 `BeginFigure`、`AddLine`、`AddBezier`、`EndFigure` 等方法。
- `GeometrySourceFactory.Create(IGeometryWriter)`：根据 `IGeometryWriter` 创建 `IGeometrySource2D` 实例。

## 快速开始

### 1. 实现自己的 `IGeometryWriter`

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

### 2. 创建 `CompositionPath`

```csharp
var geometrySource = GeometrySourceFactory.Create(new MyGeometryWriter());
var compositionPath = new CompositionPath(geometrySource);
```

### 3. 内置示例：`PathGeometryWriter`

项目测试中包含一个 `PathGeometryWriter`，可解析 SVG 路径数据字符串：

```csharp
var writer = new PathGeometryWriter("M 0,0 L 100,0 L 100,100 Z");
var geometrySource = GeometrySourceFactory.Create(writer);
var compositionPath = new CompositionPath(geometrySource);
```

## 目录结构

```
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

> `bin` 和 `obj` 目录已忽略，不包含在版本控制中。

## 构建与运行

确保已安装 .NET 10 SDK（或支持 `net10.0-windows10.0.26100.0` 的 SDK）。

```bash
dotnet build src/CompositionPathInterop.slnx
```

运行测试程序：

```bash
dotnet run --project src/CompositionPathInterop.Tests
```

测试程序会演示如何使用 `PathGeometryWriter` 创建 `CompositionPath`。

## 适用场景

- 希望减少应用体积，不想引入 Win2D 依赖
- 需要从自定义格式（如 SVG、XAML Path、几何算法输出）生成 CompositionPath
- 需要精确控制几何生成过程，避免 Direct2D 的额外抽象

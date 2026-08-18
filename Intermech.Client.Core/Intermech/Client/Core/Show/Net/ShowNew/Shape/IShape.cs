
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.IShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

internal interface IShape
{
  /// <summary> Слой в котором лежит  примитив</summary>
  ILayer Layer { get; }

  /// <summary> параметры рисуемой линии(по цвету примитива) </summary>
  IStylus Stylus { get; }

  /// <summary> полная толщина рисуемой линии</summary>
  double Weight { get; }

  /// <summary> толщина рисуемой линии в единицах  примитива </summary>
  double LineWeight { get; }

  /// <summary>габариты примитива</summary>
  RectangleD Bound { get; }

  /// <summary>габариты примитива с учетом толщины пера</summary>
  RectangleD BoundWeight { get; }
}

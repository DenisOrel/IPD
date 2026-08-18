// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.PolyLinePrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Полилиния</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class PolyLinePrimitive(GroupPrimitive owner) : PrimitiveBase(owner)
{
  /// <summary>Точки полилинии</summary>
  private List<Point> points = new List<Point>();
  /// <summary>Флаги шрифта</summary>
  private FontFlags flags;

  /// <summary>Точки полилинии</summary>
  public List<Point> Points
  {
    [DebuggerStepThrough] get => this.points;
  }

  /// <summary>Флаги шрифта</summary>
  public FontFlags Flags
  {
    [DebuggerStepThrough] get => this.flags;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    this.flags = (FontFlags) reader.ReadByte();
    int num = reader.ReadInt32();
    Point empty = Point.Empty;
    this.Points.Add(this.Org);
    for (int index = 0; index < num; ++index)
    {
      empty.X = reader.ReadInt32();
      empty.Y = reader.ReadInt32();
      this.Points.Add(empty);
    }
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    Polyline newDocumentNode = new Polyline();
    this.SetNodeId((DocumentTreeNode) newDocumentNode);
    parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode, false, false);
    this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode);
    return (DocumentTreeNode) newDocumentNode;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    if (!(node is Polyline polyline) || this.Points.Count <= 1)
      return;
    GraphicsPath graphicsPath = new GraphicsPath();
    PointF mm1 = PrimitiveBase.BlankUnitToMm(this.Points[0]);
    PointF mm2 = PrimitiveBase.BlankUnitToMm(this.Points[1]);
    graphicsPath.AddLine(mm1, mm2);
    PointF pt1 = mm2;
    for (int index = 0; index < this.Points.Count; ++index)
    {
      PointF mm3 = PrimitiveBase.BlankUnitToMm(this.Points[index]);
      graphicsPath.AddLine(pt1, mm3);
      pt1 = mm3;
    }
    polyline.Path = graphicsPath;
    if ((this.flags & FontFlags.fBold) != FontFlags.fNone)
      polyline.LineWidth = 0.5f;
    else
      polyline.LineWidth = 0.2f;
  }
}

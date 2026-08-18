// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.RectPrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Прямоугольный примитив</summary>
[Serializable]
public class RectPrimitive : PrimitiveBase
{
  /// <summary>размер</summary>
  public Size Size = Size.Empty;
  /// <summary>Относительная ширина</summary>
  public int relativeWid;
  /// <summary>Относительная высота</summary>
  public int relativeHei;
  /// <summary>automatically resize</summary>
  public GenAlignment autoFill;
  /// <summary>fill or just move to free space?</summary>
  public bool autoMove;
  /// <summary>Должен ли примитив стирать все, что за ним.</summary>
  public bool isTransparent = true;
  /// <summary>Нужно ли перечеркивать содержимое примитива</summary>
  public bool isCrossed;
  /// <summary>Если true, то оставлять охватывающий примитив на одном</summary>
  public bool theMain;
  /// <summary>Нужна рамка</summary>
  public bool needFrame;

  /// <summary>размер в мм</summary>
  public SizeF SizeMm
  {
    [DebuggerStepThrough] get
    {
      return new SizeF(PrimitiveBase.BlankUnitToMm(this.Size.Width), PrimitiveBase.BlankUnitToMm(this.Size.Height));
    }
  }

  /// <summary>Границы в мм</summary>
  public RectangleF BoundsMm
  {
    [DebuggerStepThrough] get => new RectangleF(this.OrgMm, this.SizeMm);
  }

  /// <summary>Относительная ширина. if RelativeWid, then Org.x contains left spacing, Size.x - right </summary>
  public int RelativeWid
  {
    [DebuggerStepThrough] get => this.relativeWid;
  }

  /// <summary>Относительная высота. if RelativeHei, then Org.y contains top spacing, Size.y - bottom </summary>
  public int RelativeHei
  {
    [DebuggerStepThrough] get => this.relativeHei;
  }

  /// <summary>automatically resize</summary>
  public GenAlignment AutoFill
  {
    [DebuggerStepThrough] get => this.autoFill;
  }

  /// <summary>fill or just move to free space?</summary>
  public bool AutoMove
  {
    [DebuggerStepThrough] get => this.autoMove;
  }

  /// <summary>Автозаполнение в свойствах текстовых полей, если они принадлежат области</summary>
  public bool AutoFillTextBox
  {
    [DebuggerStepThrough] get => !this.autoMove;
  }

  /// <summary>Должен ли примитив стирать все, что за ним.</summary>
  public bool IsTransparent
  {
    [DebuggerStepThrough] get => this.isTransparent;
  }

  /// <summary>Нужно ли перечеркивать содержимое примитива</summary>
  public bool IsCrossed
  {
    [DebuggerStepThrough] get => this.isCrossed;
  }

  /// <summary>Если true, то оставлять охватывающий примитив на одном листе,
  /// а этот переносить на следующий нельзя!</summary>
  public bool TheMain
  {
    [DebuggerStepThrough] get => this.theMain;
  }

  /// <summary>Нужна рамка</summary>
  public bool NeedFrame
  {
    [DebuggerStepThrough] get => this.needFrame;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  public RectPrimitive(GroupPrimitive owner)
    : base(owner)
  {
  }

  /// <summary>Конструктор</summary>
  public RectPrimitive()
  {
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    this.Size.Width = reader.ReadInt32();
    this.Size.Height = reader.ReadInt32();
    this.isTransparent = reader.ReadBoolean();
    this.isCrossed = reader.ReadBoolean();
    this.relativeWid = reader.ReadInt32();
    this.relativeHei = reader.ReadInt32();
    this.autoFill = (GenAlignment) reader.ReadByte();
    this.autoMove = reader.ReadBoolean();
    this.theMain = reader.ReadBoolean();
  }

  /// <summary>Вывести отчет о загруженных примитивах</summary>
  /// <returns>Строка с отчетом</returns>
  public override string Report() => $"{base.Report()} \\ {this.SizeMm.ToString()}";

  /// <summary>Преобразовать HorAlignment в ElementHorizontalAlign</summary>
  /// <param name="horAlign">Значение для преобразования</param>
  /// <returns></returns>
  private ElementHorizontalAlign ConvertToElementHorizontalAlign(HorAlignment horAlign)
  {
    switch (horAlign)
    {
      case HorAlignment.haNone:
        return ElementHorizontalAlign.None;
      case HorAlignment.haLeft:
        return ElementHorizontalAlign.Left;
      case HorAlignment.haCenter:
        return ElementHorizontalAlign.Center;
      case HorAlignment.haRight:
        return ElementHorizontalAlign.Right;
      default:
        return ElementHorizontalAlign.None;
    }
  }

  /// <summary>Преобразовать VertAlignment в ElementVerticalAlign</summary>
  /// <param name="vertAlign">Значение для преобразования</param>
  /// <returns></returns>
  private ElementVerticalAlign ConvertToElementVerticalAlign(VertAlignment vertAlign)
  {
    switch (vertAlign)
    {
      case VertAlignment.vaNone:
        return ElementVerticalAlign.None;
      case VertAlignment.vaTop:
        return ElementVerticalAlign.Top;
      case VertAlignment.vaCenter:
        return ElementVerticalAlign.Center;
      case VertAlignment.vaBottom:
        return ElementVerticalAlign.Bottom;
      default:
        return ElementVerticalAlign.None;
    }
  }

  /// <summary>Преобразовать GenAlignment в AutoSizeDirection</summary>
  /// <param name="align">Значение</param>
  /// <returns></returns>
  protected AutoSizeDirection ConvertToAutoSizeDirection(GenAlignment align)
  {
    switch (align)
    {
      case GenAlignment.gaNone:
        return AutoSizeDirection.None;
      case GenAlignment.gaLeft:
        return AutoSizeDirection.Width;
      case GenAlignment.gaTop:
        return AutoSizeDirection.Height;
      case GenAlignment.gaRight:
        return AutoSizeDirection.Width;
      case GenAlignment.gaBottom:
        return AutoSizeDirection.Height;
      default:
        return AutoSizeDirection.None;
    }
  }

  /// <summary>Ячейка находится в варианте или рабочей области</summary>
  public bool IsCellInArea
  {
    [DebuggerStepThrough] get
    {
      GroupPrimitive owner = this.Owner;
      while (true)
      {
        switch (owner)
        {
          case null:
          case Area _:
            goto label_3;
          default:
            owner = owner.Owner;
            continue;
        }
      }
label_3:
      return owner != null && owner is Area;
    }
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    if (node is PageElementNode pageElementNode)
      pageElementNode.Transparent = true;
    if (node is PageData pageData)
      pageData.Size = this.SizeMm;
    if (node is RectangleElement rectangleElement1)
    {
      RectangleF rectangleF = new RectangleF(this.OrgMm, this.SizeMm);
      if (this.relativeWid != 0)
      {
        rectangleElement1.cellMargins.X = rectangleF.X;
        rectangleElement1.cellMargins.Width = rectangleF.Width;
        if (rectangleElement1.ParentCell != null)
          rectangleF.Width = rectangleElement1.ParentCell.bounds.Width * ((float) this.relativeWid / 100f) - rectangleF.X - rectangleF.Width;
      }
      if (this.relativeHei != 0)
      {
        rectangleElement1.cellMargins.Y = rectangleF.Y;
        rectangleElement1.cellMargins.Height = rectangleF.Height;
        if (rectangleElement1.ParentCell != null)
          rectangleF.Height = rectangleElement1.ParentCell.bounds.Height * ((float) this.relativeHei / 100f) - rectangleF.Y - rectangleF.Height;
      }
      if ((double) rectangleF.Width == 0.0)
        rectangleF.Width = rectangleElement1.properBounds.Width;
      if ((double) rectangleF.Height == 0.0)
        rectangleF.Height = rectangleElement1.properBounds.Height;
      if (rectangleElement1.ParentCell != null && !rectangleElement1.ParentCell.IsFixedStructureArea)
      {
        if ((double) rectangleElement1.properBounds.X != (double) RectangleElement.EmptyFloatValue)
          rectangleF.X = rectangleElement1.properBounds.X;
        if ((double) rectangleElement1.properBounds.Y != (double) RectangleElement.EmptyFloatValue)
          rectangleF.Y = rectangleElement1.properBounds.Y;
      }
      rectangleElement1.AssignProperBounds(rectangleF, false, false, false);
      if (this.IsCellInArea)
        rectangleElement1.AssignMinHeight(0.0f, false, false, false);
      else if ((double) rectangleElement1.properBounds.Height != 0.0)
        rectangleElement1.AssignMinHeight(rectangleElement1.properBounds.Height, false, false, true);
      else if (rectangleElement1.ParentCell != null && rectangleElement1.ParentCell.IsRow && (double) rectangleElement1.ParentCell.properBounds.Height != 0.0)
        rectangleElement1.AssignMinHeight(rectangleElement1.ParentCell.properBounds.Height, false, false, true);
      if ((double) rectangleElement1.properBounds.Width != 0.0)
        rectangleElement1.AssignMinWidth(rectangleElement1.properBounds.Width, false, false, true);
      if (this.horAlign != HorAlignment.haNone)
        rectangleElement1.AssignHorzAlign(this.ConvertToElementHorizontalAlign(this.horAlign), false, false);
      if (this.vertAlign != VertAlignment.vaNone)
        rectangleElement1.AssignVertAlign(this.ConvertToElementVerticalAlign(this.vertAlign), false, false);
      rectangleElement1.AssignRelativeWidth((float) this.RelativeWid, false, false);
      rectangleElement1.AssignRelativeHeight((float) this.RelativeHei, false, false);
    }
    if (this.autoFill != GenAlignment.gaNone && rectangleElement1 != null)
      rectangleElement1.SetAttributeValue("BLN.AutoFill", this.autoFill.ToString(), false, false, false);
    if (this.autoMove && rectangleElement1 != null)
      rectangleElement1.SetAttributeValue("BLN.AutoMove", this.autoMove.ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    bool flag;
    if (this.IsCrossed && rectangleElement1 != null)
    {
      RectangleElement rectangleElement2 = rectangleElement1;
      flag = this.IsCrossed;
      string attributeValue = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      rectangleElement2.SetAttributeValue("BLN.IsCrossed", attributeValue, false, false, false);
    }
    if (this.TheMain && rectangleElement1 != null)
    {
      RectangleElement rectangleElement3 = rectangleElement1;
      flag = this.TheMain;
      string attributeValue = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      rectangleElement3.SetAttributeValue("BLN.TheMain", attributeValue, false, false, false);
    }
    if (!(this is TablePrimitive))
    {
      if (!(node is RectangleElement rectangleElement4))
        return;
      if (this.needFrame)
        rectangleElement4.SetOneTypeBorderLine(new BorderLine(0.2f), false);
      else
        rectangleElement4.SetOneTypeBorderLine(new BorderLine(BorderStyles.None, 0.2f), false);
    }
    else
    {
      if (!(node is RectangleElement rectangleElement5))
        return;
      rectangleElement5.SetOneTypeBorderLine(new BorderLine(0.2f), false);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.VirtualColumnCell
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Виртуальная ячейка столбца, содержащая несколько ячеек строки представляющих один столбец</summary>
[Serializable]
public class VirtualColumnCell : VirtualColumn
{
  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields() => base.InitFields();

  /// <summary>Конструктор</summary>
  protected VirtualColumnCell()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="cellsOwner">Владелец столбца</param>
  /// <param name="columnParams">Столбец сетки</param>
  public VirtualColumnCell(TableElement cellsOwner, RowColParams columnParams)
    : base(cellsOwner, columnParams)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="cellsOwner">Владелец столбца</param>
  /// <param name="columnParams">Столбец сетки</param>
  /// <param name="columnCells">Ячейки столбца</param>
  public VirtualColumnCell(
    TableElement cellsOwner,
    RowColParams columnParams,
    IList<DocumentTreeNode> columnCells)
    : base(cellsOwner, columnParams, columnCells)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="cellsOwner">Владелец столбца</param>
  /// <param name="columnParams">Столбец сетки</param>
  /// <param name="columnCells">Ячейки столбца</param>
  public VirtualColumnCell(
    TableElement cellsOwner,
    RowColParams columnParams,
    RectangleElement[] columnCells)
    : base(cellsOwner, columnParams)
  {
    for (int index = 0; index < columnCells.Length; ++index)
      this.AddChildNode((DocumentTreeNode) columnCells[index], false, false);
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected VirtualColumnCell(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Имя узла</summary>
  [Browsable(false)]
  public override string Name
  {
    get => base.Name;
    set => base.Name = value;
  }

  [Browsable(false)]
  public override float? LeftForUser
  {
    get => base.LeftForUser;
    set
    {
      if (!value.HasValue)
        return;
      base.LeftForUser = value;
    }
  }

  [Browsable(false)]
  public override float? RightForUser
  {
    get => base.RightForUser;
    set
    {
      if (!value.HasValue)
        return;
      base.RightForUser = value;
    }
  }

  [Browsable(false)]
  public override float? BottomForUser
  {
    get => base.BottomForUser;
    set
    {
      if (!value.HasValue)
        return;
      base.BottomForUser = value;
    }
  }

  [Browsable(false)]
  public override float? TopForUser
  {
    get => base.TopForUser;
    set
    {
      if (!value.HasValue)
        return;
      base.TopForUser = value;
    }
  }

  [Browsable(false)]
  public override float? WidthForUser
  {
    get => base.WidthForUser;
    set
    {
      if (!value.HasValue)
        return;
      base.WidthForUser = value;
    }
  }

  [Browsable(false)]
  public override float? HeightForUser
  {
    get => base.HeightForUser;
    set
    {
      if (!value.HasValue)
        return;
      base.HeightForUser = value;
    }
  }

  [Browsable(false)]
  public override float SkipCellsBefore
  {
    get => base.SkipCellsBefore;
    set => base.SkipCellsBefore = value;
  }

  [Browsable(false)]
  public override float SkipCellsAfter
  {
    get => base.SkipCellsAfter;
    set => base.SkipCellsAfter = value;
  }

  [Browsable(false)]
  public override float MaxHeight
  {
    get => base.MaxHeight;
    set => base.MaxHeight = value;
  }

  [Browsable(false)]
  public override CellType TableCellType
  {
    get => base.TableCellType;
    set => base.TableCellType = value;
  }

  [Browsable(false)]
  public override TableGridPosition GridPos
  {
    get => base.GridPos;
    set => base.GridPos = value;
  }
}

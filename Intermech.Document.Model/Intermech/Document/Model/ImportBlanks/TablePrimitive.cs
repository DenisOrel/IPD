// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.TablePrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Таблица</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class TablePrimitive(GroupPrimitive owner) : GroupPrimitive(owner)
{
  /// <summary>Ширины столбцов</summary>
  public List<int> colWidths = new List<int>();
  /// <summary>высоты строк</summary>
  public List<int> rowHeights = new List<int>();
  /// <summary>Флаги шрифта</summary>
  public FontFlags flags;
  /// <summary>what primitives user can insert</summary>
  public TypeSet allowInsert;

  /// <summary>Ширины столбцов</summary>
  public List<int> ColWidths
  {
    [DebuggerStepThrough] get => this.colWidths;
  }

  /// <summary>высоты строк</summary>
  public List<int> RowHeights
  {
    [DebuggerStepThrough] get => this.rowHeights;
  }

  /// <summary>Флаги шрифта</summary>
  public FontFlags Flags
  {
    [DebuggerStepThrough] get => this.flags;
  }

  /// <summary>what primitives user can insert</summary>
  public TypeSet AllowInsert
  {
    [DebuggerStepThrough] get => this.allowInsert;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    this.flags = (FontFlags) reader.ReadByte();
    this.allowInsert = (TypeSet) reader.ReadUInt16();
    PrimitiveLoader.LoadIntList(this.colWidths, reader);
    PrimitiveLoader.LoadIntList(this.rowHeights, reader);
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    RectangleF bounds = new RectangleF(this.OrgMm, this.SizeMm);
    CreateTableParams tableParams = new CreateTableParams();
    for (int index = 0; index < this.RowHeights.Count; ++index)
      tableParams.RowList.Add(new RowColParams((TableData) null, false, index, (string) null, PrimitiveBase.BlankUnitToMm(this.RowHeights[index])));
    for (int index = 0; index < this.ColWidths.Count; ++index)
      tableParams.ColumnList.Add(new RowColParams((TableData) null, true, index, (string) null, PrimitiveBase.BlankUnitToMm(this.ColWidths[index])));
    TableElement newDocumentNode = new TableElement((DocumentTreeNode) null, tableParams, bounds, true);
    this.SetNodeId((DocumentTreeNode) newDocumentNode);
    parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode, false, false);
    newDocumentNode.SetReadOnlyForTextRecursive(true);
    this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode);
    return (DocumentTreeNode) newDocumentNode;
  }

  /// <summary>Установить рамку для первой и последней ячеек</summary>
  /// <param name="cell">Ячейка</param>
  protected void SetFirstLastCellFrame(TableElement cell)
  {
    for (int index = 0; index < cell.Nodes.Count; ++index)
    {
      if (cell.Nodes[index] is RectangleElement node)
      {
        if (index == 0)
          node.SetLeftBorderLine(new BorderLine(node.LeftBorderLine.Color, BorderStyles.SolidLine, node.LeftBorderLine.Width, node.LeftBorderLine.SerifWidth), false);
        else if (index == cell.Nodes.Count - 1)
          node.SetRightBorderLine(new BorderLine(node.RightBorderLine.Color, BorderStyles.SolidLine, node.RightBorderLine.Width, node.RightBorderLine.SerifWidth), false);
        if (node is TableElement cell1)
          this.SetFirstLastCellFrame(cell1);
      }
    }
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    float width = (this.Flags & FontFlags.fBold) > FontFlags.fNone ? 0.5f : 0.2f;
    switch (node)
    {
      case TableElement cell:
        cell.AssignDrawGridToBottom(false, false);
        if ((this.Flags & FontFlags.fEllipse) != FontFlags.fNone)
        {
          cell.AssignDrawEllipse(true, false);
          BorderLine borderLine = new BorderLine(BorderStyles.None);
          cell.SetFrameStyleRecursive(borderLine, borderLine, borderLine, borderLine, true);
          cell.SetOneTypeBorderLine(new BorderLine(BorderStyles.SolidLine, width), false);
        }
        else
        {
          BorderLine top = new BorderLine(BorderStyles.SolidLine, width);
          BorderLine left = new BorderLine(BorderStyles.SolidLine, width);
          BorderLine right = new BorderLine(BorderStyles.SolidLine, width);
          BorderLine bottom = new BorderLine(BorderStyles.SolidLine, width);
          if ((this.Flags & FontFlags.fSerif) > FontFlags.fNone)
          {
            left = new BorderLine(BorderStyles.Serif, width);
            right = new BorderLine(BorderStyles.Serif, width);
          }
          cell.SetFrameStyleRecursive(top, left, bottom, right, false);
        }
        if (!cell.DrawEllipse)
          this.SetFirstLastCellFrame(cell);
        for (int index = 0; index < this.ChildList.Count; ++index)
        {
          PrimitiveBase child = this.ChildList[index];
          int x = child.Org.X;
          int y = child.Org.Y;
          if (y >= cell.Nodes.Count || x >= cell.Nodes[y].Nodes.Count)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Document.Model_161"), (object) y, (object) x, (object) this.Id));
          RectangleElement node1 = (RectangleElement) cell.Nodes[y].Nodes[x];
          PrimitiveBase.SetNodeId((DocumentTreeNode) node1, child.Id);
          if (child is RectPrimitive rectPrimitive)
            rectPrimitive.Size = new Size(Convert.ToInt32(node1.properBounds.Width * 20f), Convert.ToInt32(node1.properBounds.Height * 20f));
          child.InitNewDocumentNode((DocumentTreeNode) node1);
          if (!cell.DrawEllipse)
            node1.Borders = (RectangleBorder) null;
        }
        if (this.AllowInsert == (TypeSet) 0)
          break;
        cell.SetAttributeValue("BLN.AllowInsert", this.AllowInsert.ToString(), false, false, false);
        break;
      case ContainerElement containerElement:
        if ((this.Flags & FontFlags.fEllipse) != FontFlags.fNone)
        {
          cell.AssignDrawEllipse(true, false);
          BorderLine borderLine = new BorderLine(BorderStyles.None);
          cell.SetFrameStyleRecursive(borderLine, borderLine, borderLine, borderLine, true);
          cell.SetOneTypeBorderLine(new BorderLine(BorderStyles.SolidLine, width), false);
          break;
        }
        BorderLine top1 = new BorderLine(BorderStyles.SolidLine, width);
        BorderLine innerHorizontal = new BorderLine(BorderStyles.SolidLine, width);
        BorderLine bottom1 = new BorderLine(BorderStyles.SolidLine, width);
        if ((this.Flags & FontFlags.fSerif) > FontFlags.fNone)
        {
          BorderLine left = new BorderLine(BorderStyles.Serif, width);
          BorderLine right = new BorderLine(BorderStyles.Serif, width);
          containerElement.Borders = (RectangleBorder) new CustomBorder(top1, innerHorizontal, bottom1, left, right);
          break;
        }
        BorderLine left1 = new BorderLine(BorderStyles.SolidLine, width);
        BorderLine right1 = new BorderLine(BorderStyles.SolidLine, width);
        containerElement.Borders = (RectangleBorder) new CustomBorder(top1, innerHorizontal, bottom1, left1, right1);
        break;
    }
  }
}

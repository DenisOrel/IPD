// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.Area
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
using System.Globalization;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Вариант</summary>
[Serializable]
public class Area : GroupPrimitive
{
  /// <summary>Строк перед</summary>
  public int strBefore;
  /// <summary>Строк после</summary>
  public int strAfter;
  /// <summary>Могут ли примитивы перекрываться</summary>
  public bool canOverlap;
  /// <summary>?</summary>
  public bool useColWidths;
  /// <summary>Список вариантов</summary>
  public List<PrimitiveBase> variants = new List<PrimitiveBase>();
  /// <summary>шинины колонок</summary>
  public List<int> colWidths = new List<int>();

  /// <summary>Строк перед</summary>
  public int StrBefore
  {
    [DebuggerStepThrough] get => this.strBefore;
  }

  /// <summary>Строк после</summary>
  public int StrAfter
  {
    [DebuggerStepThrough] get => this.strAfter;
  }

  /// <summary>Могут ли примитивы перекрываться</summary>
  public bool CanOverlap
  {
    [DebuggerStepThrough] get => this.canOverlap;
  }

  /// <summary>?</summary>
  public bool UseColWidths
  {
    [DebuggerStepThrough] get => this.useColWidths;
  }

  /// <summary>Список вариантов</summary>
  public List<PrimitiveBase> Variants
  {
    [DebuggerStepThrough] get => this.variants;
  }

  /// <summary>шинины колонок</summary>
  public List<int> ColWidths
  {
    [DebuggerStepThrough] get => this.colWidths;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  public Area(GroupPrimitive owner)
    : base(owner)
  {
  }

  /// <summary>Конструктор</summary>
  public Area()
  {
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    int num1 = reader.ReadInt32();
    for (int index = 0; index < num1; ++index)
      this.Variants.Add(loader.ReadPrimitive((GroupPrimitive) this));
    if (loader.LoadingVersion >= 220 && !loader.CurrentPrimitiveIsLoaded)
    {
      this.strBefore = reader.ReadInt32();
      this.strAfter = reader.ReadInt32();
    }
    else
    {
      this.strBefore = 0;
      this.strAfter = 0;
    }
    this.canOverlap = loader.LoadingVersion >= 254 && !loader.CurrentPrimitiveIsLoaded && reader.ReadBoolean();
    if (loader.LoadingVersion >= 264 && !loader.CurrentPrimitiveIsLoaded)
    {
      this.useColWidths = reader.ReadBoolean();
      if (this.UseColWidths)
      {
        int num2 = reader.ReadInt32();
        for (int index = 0; index < num2; ++index)
          this.ColWidths.Add(reader.ReadInt32());
      }
    }
    for (int index1 = 0; index1 < this.ChildList.Count; ++index1)
    {
      for (int index2 = index1 + 1; index2 < this.ChildList.Count; ++index2)
      {
        if (this.ChildList[index1].Org.X > this.ChildList[index2].Org.X)
        {
          PrimitiveBase child = this.ChildList[index2];
          this.ChildList[index2] = this.ChildList[index1];
          this.ChildList[index1] = child;
        }
      }
    }
  }

  /// <summary>Восстановить ссылки на родительские элементы у дочерних элементов</summary>
  public override void RestoreOwnersRecurcive()
  {
    base.RestoreOwnersRecurcive();
    for (int index = 0; index < this.variants.Count; ++index)
    {
      this.variants[index].Owner = (GroupPrimitive) this;
      if (this.variants[index] is GroupPrimitive variant)
        variant.RestoreOwnersRecurcive();
    }
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    TableData newDocumentNode1 = (TableData) null;
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
    if (this.variants.Count > 0)
    {
      newDocumentNode1 = (TableData) new TableElement(true, (DocumentTreeNode) null, this.BoundsMm, true);
      this.SetNodeId((DocumentTreeNode) newDocumentNode1);
      parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode1, false, false);
      this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode1);
    }
    RectPrimitive rectPrimitive = (RectPrimitive) null;
    RectangleF boundsMm;
    for (int index = 0; index < this.ChildList.Count; ++index)
    {
      if (this.ChildList[index] is RectPrimitive child2)
      {
        if (newDocumentNode1 == null)
        {
          newDocumentNode1 = (TableData) new TableElement(false, (DocumentTreeNode) null, this.BoundsMm, true);
          this.SetNodeId((DocumentTreeNode) newDocumentNode1);
          parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode1, false, false);
          this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode1);
        }
        if (child2.relativeHei != 0 || child2.relativeWid != 0)
        {
          int num = newDocumentNode1.IsFixedStructureArea ? 1 : 0;
          newDocumentNode1.AssignIsFixedStructureArea(true, false, false);
        }
        else if (rectPrimitive != null)
        {
          boundsMm = child2.BoundsMm;
          double left = (double) boundsMm.Left;
          boundsMm = rectPrimitive.BoundsMm;
          double right = (double) boundsMm.Right;
          if (left - right > 0.0)
          {
            int num = newDocumentNode1.IsFixedStructureArea ? 1 : 0;
            newDocumentNode1.AssignIsFixedStructureArea(true, false, false);
          }
        }
        else
        {
          boundsMm = child2.BoundsMm;
          double left1 = (double) boundsMm.Left;
          boundsMm = this.BoundsMm;
          double left2 = (double) boundsMm.Left;
          if (left1 - left2 > 0.0)
          {
            int num = newDocumentNode1.IsFixedStructureArea ? 1 : 0;
            newDocumentNode1.AssignIsFixedStructureArea(true, false, false);
          }
        }
        boundsMm = child2.BoundsMm;
        if ((double) boundsMm.Y == 0.0)
        {
          boundsMm = child2.BoundsMm;
          double height1 = (double) boundsMm.Height;
          boundsMm = this.BoundsMm;
          double height2 = (double) boundsMm.Height;
          if (height1 == height2)
            goto label_20;
        }
        int num1 = newDocumentNode1.IsFixedStructureArea ? 1 : 0;
        newDocumentNode1.AssignIsFixedStructureArea(true, false, false);
label_20:
        documentTreeNode = (DocumentTreeNode) this.CreateCell(child2, (DocumentTreeNode) newDocumentNode1);
        rectPrimitive = child2;
      }
      else if (this.ChildList[index] is PolyLinePrimitive child1)
      {
        PageData page = this.GetPage(parentDocNode);
        child1.CreateNewDocumentNode((DocumentTreeNode) page);
      }
    }
    if (this.ChildList.Count > 0 && rectPrimitive != null)
    {
      boundsMm = this.BoundsMm;
      double right1 = (double) boundsMm.Right;
      boundsMm = rectPrimitive.BoundsMm;
      double right2 = (double) boundsMm.Right;
      if (right1 - right2 > 0.0 && newDocumentNode1 != null)
        newDocumentNode1.AssignIsFixedStructureArea(true, false, false);
    }
    for (int index = 0; index < this.Variants.Count; ++index)
    {
      if (newDocumentNode1 == null)
      {
        newDocumentNode1 = (TableData) new TableElement((DocumentTreeNode) null, this.BoundsMm, true);
        this.SetNodeId((DocumentTreeNode) newDocumentNode1);
        parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode1, false, false);
        this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode1);
      }
      DocumentTreeNode newDocumentNode2 = this.Variants[index].CreateNewDocumentNode((DocumentTreeNode) newDocumentNode1);
      newDocumentNode2.AssignCloneByTemplateWithParent(false);
      if (newDocumentNode2.Index > 0 && newDocumentNode2 is VisualNode visualNode)
        visualNode.SetVisible(false, false, false, false, false, false);
    }
    if (newDocumentNode1 == null)
    {
      newDocumentNode1 = (TableData) new TableElement(false, (DocumentTreeNode) null, this.BoundsMm, true);
      this.SetNodeId((DocumentTreeNode) newDocumentNode1);
      parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode1, false, false);
      this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode1);
    }
    if (this.NeedFrame)
      this.SetHorizontalLines((TableElement) newDocumentNode1);
    return (DocumentTreeNode) newDocumentNode1;
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public Page CreateAsPage(DocumentTreeNode parentDocNode)
  {
    RectangleF boundsMm = this.BoundsMm;
    Page asPage = new Page();
    this.SetNodeId((DocumentTreeNode) asPage);
    parentDocNode.AddChildNode((DocumentTreeNode) asPage, false, false);
    this.InitNewDocumentNode((DocumentTreeNode) asPage);
    for (int index = 0; index < this.ChildList.Count; ++index)
      this.ChildList[index].CreateNewDocumentNode((DocumentTreeNode) asPage)?.AssignCloneByTemplateWithParent(true);
    for (int index = 0; index < this.Variants.Count; ++index)
    {
      DocumentTreeNode newDocumentNode = this.Variants[index].CreateNewDocumentNode((DocumentTreeNode) asPage);
      newDocumentNode.AssignCloneByTemplateWithParent(false);
      if (newDocumentNode.Index > 0 && newDocumentNode is VisualNode visualNode)
        visualNode.SetVisible(false, false, false, false, false, false);
    }
    return asPage;
  }

  /// <summary>Найти страницу для элемента</summary>
  /// <param name="node"></param>
  /// <returns></returns>
  private PageData GetPage(DocumentTreeNode node)
  {
    for (DocumentTreeNode page = node; page != null; page = page.Parent)
    {
      if (page is PageData)
        return page as PageData;
    }
    return (PageData) null;
  }

  /// <summary>Заменить пользовательские примитивы</summary>
  /// <param name="loader">Загрузчик</param>
  public override void ReplaceUserPrimitives(BlankLoader loader)
  {
    for (int index = 0; index < this.Variants.Count; ++index)
    {
      if (this.Variants[index] is GroupPrimitive variant2)
        variant2.ReplaceUserPrimitives(loader);
      else if (this.Variants[index] is UserPrimitive variant1)
      {
        RectPrimitive fromUserPrimitive = loader.CreatePrimitiveFromUserPrimitive(variant1);
        if (fromUserPrimitive != null)
        {
          this.Variants[index] = (PrimitiveBase) fromUserPrimitive;
          fromUserPrimitive.Owner = (GroupPrimitive) this;
          if (this.Variants[index] is GroupPrimitive variant)
            variant.ReplaceUserPrimitives(loader);
        }
        else
        {
          int num = (int) MessageBox.Show("Can't Find Library Primitive");
        }
      }
    }
    base.ReplaceUserPrimitives(loader);
  }

  /// <summary>?</summary>
  /// <param name="style1">?</param>
  /// <param name="style2">?</param>
  /// <returns>?</returns>
  protected BorderStyles OverDrawStyle(BorderStyles style1, BorderStyles style2)
  {
    if (style1 == BorderStyles.SolidLine || style2 == BorderStyles.SolidLine)
      return BorderStyles.SolidLine;
    return style1 == BorderStyles.None || style1 == BorderStyles.Serif || style2 != BorderStyles.None && style2 != BorderStyles.Serif ? style2 : style1;
  }

  /// <summary>Создать ячейку таблицы</summary>
  /// <param name="rectPrimitive">Примитив</param>
  /// <returns>Ячейку таблицы</returns>
  protected RectangleElement CreateCell(
    RectPrimitive rectPrimitive,
    DocumentTreeNode cellParentNode)
  {
    RectangleElement newDocumentNode = (RectangleElement) rectPrimitive.CreateNewDocumentNode(cellParentNode);
    newDocumentNode.AssignMinHeight(newDocumentNode.Bounds.Height, false, false, false);
    newDocumentNode.AssignCloneByTemplateWithParent(true);
    return newDocumentNode;
  }

  /// <summary>Создать пустую ячейку</summary>
  /// <param name="cellBounds">Границы ячейки</param>
  /// <returns>Пустая ячейка</returns>
  protected RectangleElement CreateEmptyCell(RectangleF cellBounds)
  {
    TextBoxElement emptyCell = new TextBoxElement();
    emptyCell.AssignReadOnly(true);
    emptyCell.SetName(LocalizationHolder.rm.GetString("Document.Model_164"), false, false);
    emptyCell.AssignCloneByTemplateWithParent(true);
    emptyCell.AssignTransparent(true, false);
    return (RectangleElement) emptyCell;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    if (!(node is TableData ownerTable))
      return;
    ownerTable.AssignDrawGridToBottom(false, false);
    ownerTable.AssignMinHeight(this.BoundsMm.Height, false, false, false);
    if (this.StrBefore != 0)
      ownerTable.SetSkipCellsBefore((float) this.StrBefore, false, false, false);
    if (this.StrAfter != 0)
      ownerTable.SetSkipCellsAfter((float) this.StrAfter, false, false, false);
    if (!this.UseColWidths || this.ColWidths.Count <= 0)
      return;
    List<RowColParams> rowColParamsList = new List<RowColParams>(this.ColWidths.Count);
    for (int index = 0; index < this.ColWidths.Count; ++index)
      rowColParamsList.Add(new RowColParams(ownerTable, index, (string) null, PrimitiveBase.BlankUnitToMm(this.ColWidths[index])));
    ownerTable.SetGridColumnsParams(rowColParamsList, true, false);
    ownerTable.SetAttributeValue("BLN.UseColWidths", this.UseColWidths.ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
  }

  /// <summary>Установить стиль горизонтальных линий ячейки</summary>
  /// <param name="cell">Ячейка</param>
  protected void SetHorizontalLines(TableElement cell)
  {
    cell.SetBottomBorderLine(new BorderLine(BorderStyles.SolidLine, 0.2f), false);
    cell.SetTopBorderLine(new BorderLine(BorderStyles.SolidLine, 0.2f), false);
  }

  /// <summary>Найти примитив по идентификатору</summary>
  /// <param name="primId">Идентификатор примитива</param>
  /// <returns>Примитив</returns>
  public override PrimitiveBase FindById(string primId)
  {
    PrimitiveBase byId = base.FindById(primId);
    if (byId == null)
    {
      for (int index = 0; index < this.Variants.Count; ++index)
      {
        byId = this.Variants[index].FindById(primId);
        if (byId != null)
          break;
      }
    }
    return byId;
  }
}

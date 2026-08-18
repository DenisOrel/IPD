// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.BlankList
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

/// <summary>Страница бланка</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class BlankList(GroupPrimitive owner) : GroupPrimitive(owner)
{
  /// <summary>Может быть первой</summary>
  public bool canBeFirst;
  /// <summary>Содержит Рабочую область</summary>
  public bool hasWorkspace;
  /// <summary>Границы рабочей области</summary>
  public Rectangle workspaceRect;
  /// <summary>?</summary>
  public bool needShrinkToList;
  /// <summary>?</summary>
  public int indLeft;
  /// <summary>?</summary>
  public int indRight;
  /// <summary>?</summary>
  public int indTop;
  /// <summary>?</summary>
  public int indBottom;
  /// <summary>?</summary>
  public int evenOffs;
  /// <summary>number of workspace columns, if workspace exists</summary>
  public int wsParts;
  /// <summary>if true, fill variants by columns, else by rows</summary>
  public bool fillColsFirst;

  /// <summary>Может быть первой</summary>
  public bool CanBeFirst
  {
    [DebuggerStepThrough] get => this.canBeFirst;
  }

  /// <summary>Содержит Рабочую область</summary>
  public bool HasWorkspace
  {
    [DebuggerStepThrough] get => this.hasWorkspace;
  }

  /// <summary>Границы рабочей области</summary>
  public Rectangle WorkspaceRect
  {
    [DebuggerStepThrough] get => this.workspaceRect;
  }

  /// <summary>?</summary>
  public bool NeedShrinkToList
  {
    [DebuggerStepThrough] get => this.needShrinkToList;
  }

  /// <summary>?</summary>
  public int IndLeft
  {
    [DebuggerStepThrough] get => this.indLeft;
  }

  /// <summary>?</summary>
  public int IndRight
  {
    [DebuggerStepThrough] get => this.indRight;
  }

  /// <summary>?</summary>
  public int IndTop
  {
    [DebuggerStepThrough] get => this.indTop;
  }

  /// <summary>?</summary>
  public int IndBottom
  {
    [DebuggerStepThrough] get => this.indBottom;
  }

  /// <summary>?</summary>
  public int EvenOffs
  {
    [DebuggerStepThrough] get => this.evenOffs;
  }

  /// <summary>number of workspace columns, if workspace exists</summary>
  public int WSParts
  {
    [DebuggerStepThrough] get => this.wsParts;
  }

  /// <summary>if true, fill variants by columns, else by rows</summary>
  public bool FillColsFirst
  {
    [DebuggerStepThrough] get => this.fillColsFirst;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    this.canBeFirst = reader.ReadBoolean();
    this.hasWorkspace = reader.ReadBoolean();
    this.workspaceRect = loader.ReadRect();
    if (loader.LoadingVersion >= 118 && !loader.CurrentPrimitiveIsLoaded)
    {
      this.needShrinkToList = reader.ReadBoolean();
      this.indLeft = reader.ReadInt32();
      this.indTop = reader.ReadInt32();
      this.indRight = reader.ReadInt32();
      this.indBottom = reader.ReadInt32();
    }
    else
    {
      this.needShrinkToList = false;
      this.indLeft = 0;
      this.indTop = 0;
      this.indRight = 0;
      this.indBottom = 0;
    }
    this.evenOffs = loader.LoadingVersion < 250 || loader.CurrentPrimitiveIsLoaded ? 0 : reader.ReadInt32();
    if (loader.LoadingVersion >= 252 && !loader.CurrentPrimitiveIsLoaded)
    {
      this.wsParts = reader.ReadInt32();
      this.fillColsFirst = reader.ReadBoolean();
    }
    else
    {
      this.wsParts = 1;
      this.fillColsFirst = true;
    }
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    Page newDocumentNode = new Page();
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
    if (!(node is Page page1))
      return;
    node.AssignCloneByTemplateWithParent(false);
    if ((double) this.SizeMm.Height < (double) this.SizeMm.Width)
      page1.Landscape = true;
    page1.Size = this.SizeMm;
    bool flag;
    if (this.CanBeFirst)
    {
      Page page2 = page1;
      flag = this.CanBeFirst;
      string attributeValue = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      page2.SetAttributeValue("BLN.CanBeFirst", attributeValue, false, false, false);
    }
    if (this.NeedShrinkToList)
    {
      Page page3 = page1;
      flag = this.NeedShrinkToList;
      string attributeValue = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      page3.SetAttributeValue("BLN.NeedShrinkToList", attributeValue, false, false, false);
    }
    if (this.IndLeft != 0)
      page1.SetAttributeValue("BLN.IndLeft", PrimitiveBase.BlankUnitToMm(this.IndLeft).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.IndRight != 0)
      page1.SetAttributeValue("BLN.IndRight", PrimitiveBase.BlankUnitToMm(this.IndRight).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.IndTop != 0)
      page1.SetAttributeValue("BLN.IndTop", PrimitiveBase.BlankUnitToMm(this.IndTop).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.IndBottom != 0)
      page1.SetAttributeValue("BLN.IndBottom", PrimitiveBase.BlankUnitToMm(this.IndBottom).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.EvenOffs != 0)
      page1.SetAttributeValue("BLN.EvenOffs", PrimitiveBase.BlankUnitToMm(this.EvenOffs).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.WSParts != 1)
      page1.SetAttributeValue("BLN.WSParts", this.WSParts.ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (!this.FillColsFirst)
      return;
    Page page4 = page1;
    flag = this.FillColsFirst;
    string attributeValue1 = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    page4.SetAttributeValue("BLN.FillColsFirst", attributeValue1, false, false, false);
  }
}

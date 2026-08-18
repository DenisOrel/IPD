// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.GroupPrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Группа примитивов</summary>
[Serializable]
public class GroupPrimitive : RectPrimitive
{
  /// <summary>Дочерние примитивы</summary>
  public List<PrimitiveBase> childList = new List<PrimitiveBase>();
  /// <summary>for searching functions</summary>
  public int current;
  /// <summary>Тип искомых примитивов</summary>
  public PrimitiveType searchType;
  /// <summary>Границы поиска</summary>
  public Rectangle searchRect = Rectangle.Empty;
  /// <summary>Если примитив входит в рабочую область, он всегда должен начинаться с нового листа</summary>
  public bool placeOnPageTop;
  /// <summary>По возможности располагать весь примитив на одной странице</summary>
  public bool tryNotBreak;
  /// <summary>Если дочерние элементы уменьшились, нужно ли уменьшать и группу?</summary>
  public bool autoShrink;
  /// <summary>Если можно, не делить.
  /// Childs of this group are included into 'header',
  /// which is always placed when moving part of the
  /// owner to the next list</summary>
  public bool moveWhole;
  /// <summary>Childs of the group may reside on several lists</summary>
  public bool canSplitChilds;

  /// <summary>Дочерние примитивы</summary>
  public List<PrimitiveBase> ChildList
  {
    [DebuggerStepThrough] get => this.childList;
  }

  /// <summary>for searching functions</summary>
  public int Current
  {
    [DebuggerStepThrough] get => this.current;
  }

  /// <summary>Тип искомых примитивов</summary>
  public PrimitiveType SearchType
  {
    [DebuggerStepThrough] get => this.searchType;
  }

  /// <summary>Границы поиска</summary>
  public Rectangle SearchRect
  {
    [DebuggerStepThrough] get => this.searchRect;
  }

  /// <summary>Если примитив входит в рабочую область, он всегда должен начинаться с нового листа</summary>
  public bool PlaceOnPageTop
  {
    [DebuggerStepThrough] get => this.placeOnPageTop;
  }

  /// <summary>По возможности располагать весь примитив на одной странице</summary>
  public bool TryNotBreak
  {
    [DebuggerStepThrough] get => this.tryNotBreak;
  }

  /// <summary>Если дочерние элементы уменьшились, нужно ли уменьшать и группу?</summary>
  public bool AutoShrink
  {
    [DebuggerStepThrough] get => this.autoShrink;
  }

  /// <summary>Если можно, не делить.
  /// Childs of this group are included into 'header',
  /// which is always placed when moving part of the
  /// owner to the next list</summary>
  public bool MoveWhole
  {
    [DebuggerStepThrough] get => this.moveWhole;
  }

  /// <summary>Childs of the group may reside on several lists</summary>
  public bool CanSplitChilds
  {
    [DebuggerStepThrough] get => this.canSplitChilds;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  public GroupPrimitive(GroupPrimitive owner)
    : base(owner)
  {
  }

  /// <summary>Конструктор</summary>
  public GroupPrimitive()
  {
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    this.current = reader.ReadInt32();
    this.searchType = (PrimitiveType) reader.ReadByte();
    this.searchRect = loader.ReadRect();
    this.placeOnPageTop = reader.ReadBoolean();
    this.tryNotBreak = reader.ReadBoolean();
    this.autoShrink = reader.ReadBoolean();
    this.needFrame = reader.ReadBoolean();
    this.moveWhole = reader.ReadBoolean();
    int num = reader.ReadInt32();
    for (int index = 0; index < num; ++index)
      this.ChildList.Add(loader.ReadPrimitive(this));
    long position = reader.BaseStream.Position;
    this.canSplitChilds = reader.ReadInt32() == PrimitiveLoader.MagicSign ? reader.ReadBoolean() : throw new Exception("Document format error");
    reader.ReadInt32();
  }

  /// <summary>Восстановить ссылки на родительские элементы у дочерних элементов</summary>
  public virtual void RestoreOwnersRecurcive()
  {
    for (int index = 0; index < this.childList.Count; ++index)
    {
      this.childList[index].Owner = this;
      if (this.childList[index] is GroupPrimitive child)
        child.RestoreOwnersRecurcive();
    }
  }

  /// <summary>Вывести отчет о загруженных примитивах</summary>
  /// <returns>Строка с отчетом</returns>
  public override string Report()
  {
    string str = base.Report();
    for (int index = 0; index < this.ChildList.Count; ++index)
      str = str + Environment.NewLine + this.ChildList[index].Report();
    return str;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    if (!(node is TableElement) && !(this is Area))
    {
      for (int index = 0; index < this.ChildList.Count; ++index)
      {
        DocumentTreeNode newDocumentNode = this.ChildList[index].CreateNewDocumentNode(node);
        TablePrimitive child = this.ChildList[index] as TablePrimitive;
        if (newDocumentNode != null)
        {
          if (child == null || child.childList.Count > 0)
            node.AddChildNode(newDocumentNode, false, false);
          else
            node.InsertChildNode(0, newDocumentNode, false, true, false, false);
        }
      }
    }
    if (!(node is RectangleElement rectangleElement))
      return;
    if (this.MoveWhole)
      rectangleElement.SetTryNotBreak(true, false, false);
    if (this.placeOnPageTop)
      rectangleElement.SetFromNewPage(true, false, false);
    if (this.tryNotBreak)
      rectangleElement.SetTryNotBreak(true, false, false);
    if (this.CanSplitChilds)
      rectangleElement.SetAttributeValue("BLN.CanSplitChilds", this.CanSplitChilds.ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (!this.AutoShrink)
      rectangleElement.SetAttributeValue("BLN.AutoShrink", this.autoShrink.ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.Current != 0)
      rectangleElement.SetAttributeValue("BLN.Current", this.Current.ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.SearchType != PrimitiveType.ptUnknown)
      rectangleElement.SetAttributeValue("BLN.SearchType", this.SearchType.ToString(), false, false, false);
    if (this.SearchRect.IsEmpty)
      return;
    rectangleElement.SetAttributeValue("BLN.SearchRect", this.SearchRect.ToString(), false, false, false);
  }

  /// <summary>Заменить пользовательские примитивы</summary>
  /// <param name="loader">Загрузчик</param>
  public virtual void ReplaceUserPrimitives(BlankLoader loader)
  {
    for (int index = 0; index < this.ChildList.Count; ++index)
    {
      if (this.ChildList[index] is GroupPrimitive child2)
        child2.ReplaceUserPrimitives(loader);
      else if (this.ChildList[index] is UserPrimitive child1)
      {
        RectPrimitive fromUserPrimitive = loader.CreatePrimitiveFromUserPrimitive(child1);
        if (fromUserPrimitive != null)
        {
          this.ChildList[index] = (PrimitiveBase) fromUserPrimitive;
          fromUserPrimitive.Owner = this;
          if (this.ChildList[index] is GroupPrimitive child)
            child.ReplaceUserPrimitives(loader);
        }
        else
        {
          int num = (int) MessageBox.Show("Can't Find Library Primitive");
        }
      }
    }
  }

  /// <summary>Найти примитив по идентификатору</summary>
  /// <param name="primId">Идентификатор примитива</param>
  /// <returns>Примитив</returns>
  public override PrimitiveBase FindById(string primId)
  {
    PrimitiveBase byId = base.FindById(primId);
    if (byId == null)
    {
      for (int index = 0; index < this.ChildList.Count; ++index)
      {
        byId = this.ChildList[index].FindById(primId);
        if (byId != null)
          break;
      }
    }
    return byId;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchiveNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Класс, реализующий узел "Архив" из пространства навигации
/// </summary>
public class ArchiveNode : ObjectNode
{
  /// <summary>Тип выбранного архива</summary>
  public int ArcTypeID
  {
    [DebuggerStepThrough] get => this._objTypeID;
  }

  /// <summary>Идентификатор версии выбранного архива</summary>
  public long ArcID
  {
    [DebuggerStepThrough] get => this._objID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="arcTypeID"></param>
  /// <param name="arcID"></param>
  public ArchiveNode(int arcTypeID, long arcID)
    : base(arcTypeID, arcID)
  {
    this.options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="arcTypeID"></param>
  /// <param name="arcID"></param>
  /// <param name="conditions"></param>
  public ArchiveNode(int arcTypeID, long arcID, ConditionStructure[] conditions)
    : this(arcTypeID, arcID)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    if (this.Services != null && this.Services.GetService(typeof (ViewArchives)) != null)
      return base.CreateFolderSlots();
    DescriptorCollection specialDescriptors = this.GetSpecialDescriptors();
    List<PartSlot> folderSlots = base.CreateFolderSlots();
    folderSlots.Insert(0, new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(specialDescriptors, false)));
    return folderSlots;
  }

  protected override ITopBinding GetBinding(BindingType bindingType)
  {
    return (ITopBinding) new ArchiveBinding(this._objTypeID, this._objID, bindingType);
  }

  /// <summary>
  /// Создает и возвращает часть элемента, отвечающую за документы, находящиеся
  /// в данном архиве.
  /// </summary>
  /// <returns>Интерфейс части</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new DocumsPart(this._objID, this.Services));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <param name="dataFormat"></param>
  /// <returns></returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IDBObjectTypeSelectionID) ? (object) new DBBindedObjectType(ConstsHolder.DocTypeID) : base.GetData(nodeID, dataFormat);
  }
}

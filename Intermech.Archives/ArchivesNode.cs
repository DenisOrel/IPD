// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Класс, реализующий узел "Архивы" из пространства навигации
/// </summary>
public class ArchivesNode : CompositeNode, IContextAware
{
  /// <summary>Права доступа к списку объектов</summary>
  internal AccessRights _accessRights;
  /// <summary>Контекст узла</summary>
  internal IServiceProvider _services;

  /// <summary>Создать элемент пространства навигации "Архивы"</summary>
  public ArchivesNode()
    : this(AccessRights.Enabled)
  {
  }

  /// <summary>Создать элемент пространства навигации "Архивы"</summary>
  /// <param name="accessRights">Права доступа к списку архивов</param>
  public ArchivesNode(AccessRights accessRights)
  {
    this._accessRights = accessRights;
    this.options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Права доступа к списку объектов</summary>
  public AccessRights AccessRights
  {
    [DebuggerStepThrough] get => this._accessRights;
    set => this._accessRights = value;
  }

  /// <summary>
  /// Создает и возвращает части элемента, отвечающие за архивы, находящиеся
  /// на верхнем уровне дерева архивов.
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    DescriptorCollection descriptors = (DescriptorCollection) null;
    if (this.AccessRights == AccessRights.Enabled && (this._services == null || this._services.GetService(typeof (ViewArchives)) == null))
      descriptors = this.GetSpecialDescriptors();
    List<PartSlot> folderSlots = new List<PartSlot>();
    if (descriptors != null)
      folderSlots.Add(new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(descriptors, false)));
    folderSlots.Add(new PartSlot(Intermech.Navigator.Selections.Consts.ContentPartGuid, (INodePart) new ArchivesTopObjectsPart(ConstsHolder.ArcTypeID, this.Services)));
    return folderSlots;
  }

  protected override ITopBinding GetBinding(BindingType bindingType)
  {
    return (ITopBinding) new ArchivesBinding(bindingType);
  }

  /// <summary>
  /// Создает и возвращает часть элемента, отвечающую за документы, находящиеся
  /// в любом из существующих архивов.
  /// </summary>
  /// <returns>Интерфейс части</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.AccessRights != AccessRights.Enabled ? (List<PartSlot>) null : this.SlotsFromSinglePart((INodePart) new AllDocumsPart(this.Services));
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

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }
}

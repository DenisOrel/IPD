
// Type: Intermech.Navigator.DBObjects.ObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.ObjectGroups;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Базовая реализация элемента навигации, представляющего объект базы данных.
/// В качестве папок возвращает объекты, входящие в состав обрабатываемого
/// элементом объекта связью по умолчанию. Не-папок у этого элемента нет.
/// </summary>
public class ObjectNode : 
  CompositeNode,
  IContextAware,
  INodeNotifications,
  INodeIDCreator,
  IObjectTypeAndRelationFiltrationSupported
{
  /// <summary>
  /// Идентификатор типа объекта, состав которого будет разворачивать данный элемент
  /// </summary>
  protected int _objTypeID;
  /// <summary>Текущий пользователь и роль</summary>
  private static ICurrentUserAndRole _userRole;
  /// <summary>
  /// Идентификатор версии объекта, состав которого будет разворачивать данный элемент
  /// </summary>
  protected long _objID;
  /// <summary>Контейнер сервисов</summary>
  protected AdvancedServiceContainer _services = new AdvancedServiceContainer();

  protected ICurrentUserAndRole UserRole
  {
    [DebuggerStepThrough] get
    {
      ObjectNode._userRole = ObjectNode._userRole ?? ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      return ObjectNode._userRole;
    }
  }

  /// <summary>Создать узел</summary>
  /// <param name="objTypeID">Тип</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  public ObjectNode(int objTypeID, long objID)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this.options = NodeOptions.CanContainsComposition;
  }

  public override void Refresh()
  {
    this.folderSlots = (List<PartSlot>) null;
    this.nonFolderSlots = (List<PartSlot>) null;
    this.statusesInfoService = (INodeStatusesInfo) null;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    if (this.UserRole.Rule != null)
    {
      List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(this._objTypeID);
      objectTypeParentsId.Insert(0, this._objTypeID);
      ParentObjectType parentObjectType = objectTypeParentsId.Select<int, ParentObjectType>((Func<int, ParentObjectType>) (o => this.UserRole.Rule.ParentObjectTypes.FirstOrDefault<ParentObjectType>((Func<ParentObjectType, bool>) (oo => oo.ObjectTypeID == o)))).FirstOrDefault<ParentObjectType>((Func<ParentObjectType, bool>) (o => o != null));
      if (parentObjectType != null)
      {
        foreach (ChildRelationType childRelationType in parentObjectType.ChildRelationTypes)
        {
          if (childRelationType.Visible)
          {
            INodePart part;
            if (childRelationType.GetChildObjectTypesAndDescendants().Any<ChildObjectType>((Func<ChildObjectType, bool>) (o => o.Visible && o.Grouping)))
            {
              INodePart folderPart = this.CreateFolderPart(childRelationType.RelationTypeID);
              List<ObjectGroupNodePart> objectGroupNodePartList = new List<ObjectGroupNodePart>();
              foreach (ChildObjectType typesAndDescendant in childRelationType.GetChildObjectTypesAndDescendants())
              {
                if (typesAndDescendant.Visible && typesAndDescendant.Grouping)
                  objectGroupNodePartList.Add(new ObjectGroupNodePart(this._objTypeID, childRelationType.RelationTypeID, typesAndDescendant.ObjectTypeID, this._objID));
              }
              part = (INodePart) new ObjectAndObjectGroupNodePart(objectGroupNodePartList.ToArray(), folderPart);
            }
            else
              part = this.CreateFolderPart(childRelationType.RelationTypeID);
            folderSlots.Add(new PartSlot(MetaDataHelper.GetRelationTypeGuid(childRelationType.RelationTypeID), part));
          }
        }
      }
      else
        folderSlots.Add(this.CreateDefaultRelationPartSlot());
    }
    else
      folderSlots.Add(this.CreateDefaultRelationPartSlot());
    return folderSlots;
  }

  private PartSlot CreateDefaultRelationPartSlot()
  {
    Guid relationTypeGuid = MetaDataHelper.GetDefaultRelationTypeGuid(this._objTypeID);
    return new PartSlot(relationTypeGuid, this.CreateFolderPart(MetaDataHelper.GetRelationTypeID(relationTypeGuid)));
  }

  protected virtual INodePart CreateFolderPart(int relTypeId)
  {
    return (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relTypeId, this.Services);
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    List<int> visibleRelations = this.UserRole.Rule.GetObjectTypeVisibleRelations(this._objTypeID, true);
    if (visibleRelations.Count == 0 || RelationTypeHelper.IsAnyUnknownRelationTypeID((IEnumerable<int>) visibleRelations))
      return (List<PartSlot>) null;
    List<PartSlot> nonFolderSlots = new List<PartSlot>();
    for (int index = 0; index < visibleRelations.Count; ++index)
    {
      INodePart nonFolderPart = this.CreateNonFolderPart(visibleRelations[index]);
      if (nonFolderPart != null)
      {
        Guid relationTypeGuid = MetaDataHelper.GetRelationTypeGuid(visibleRelations[index]);
        nonFolderSlots.Add(new PartSlot(relationTypeGuid, nonFolderPart));
      }
    }
    return nonFolderSlots;
  }

  protected virtual INodePart CreateNonFolderPart(int relTypeId)
  {
    return (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relTypeId, this.Services);
  }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента. Используется только в том случае, если для
  /// данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    NodeColumnCollection defaultColumns = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).DefaultColumnPack[new NavigatorColumnsKey(4, this._objTypeID, (string) null)];
    if (defaultColumns != null)
      return defaultColumns;
    IViewState service = this.Services != null ? this.Services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.NodeInViews || (viewStateFlags & ViewStateFlags.InParametersCard) == ViewStateFlags.InParametersCard)
      return base.GetDefaultColumns(content);
    return (this.Options & NodeOptions.CanContainsComposition) == NodeOptions.CanContainsComposition ? Utils.CaptionAndStatesesColumns(NodeColumnSortOrder.Ascending) : Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending);
  }

  /// <summary>Контейнер сервисов</summary>
  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="AdditionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  public virtual ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    if (e.EventName == "ObjectTypeAndRelationFiltrationChanged" || e.EventName == "SortedRelationsChanged" && MetaDataHelper.HasObjectTypeSortingRelTypes(this._objTypeID) && ((DBRelationsEventArgs) e).RelationIDs.Contains(this._objID) || e.EventName == "ObjectTypesChanged" && e is DBObjectTypesEventArgs && (e as DBObjectTypesEventArgs).ObjectTypeIDs.Contains(this._objTypeID))
      return ProcessResult.RefreshNode;
    if ((e.EventName == "AttributeChanged" || e.EventName == "AttributeRemoved") && AdditionalInfo != null && AdditionalInfo is NodeColumnCollection && e is DBAttributesEventArgs && (AdditionalInfo as NodeColumnCollection).ColumnIDsExists((e as DBAttributesEventArgs).AttributeIDs))
      return ProcessResult.RefreshNodeAndColumns;
    if (e.EventName == "Attribute4RelTypeEvent" || e.EventName == "Attribute4ObjTypeEvent")
    {
      DBAttributes4TypeEventArgs attributes4TypeEventArgs = e as DBAttributes4TypeEventArgs;
      NodeColumnCollection columnCollection = AdditionalInfo as NodeColumnCollection;
      List<int> visibleRelations = this.UserRole.Rule.GetObjectTypeVisibleRelations(this._objTypeID, true);
      if (attributes4TypeEventArgs != null && columnCollection != null && columnCollection.Count > 0 && (e.EventName == "Attribute4RelTypeEvent" && visibleRelations.Count > 0 && visibleRelations.Contains(attributes4TypeEventArgs.CategoryID) || e.EventName == "Attribute4ObjTypeEvent" && this._objTypeID == attributes4TypeEventArgs.CategoryID))
        return !columnCollection.ColumnIDsExists(attributes4TypeEventArgs.ChangedIDs) && !columnCollection.ColumnIDsExists(attributes4TypeEventArgs.RemovedIDs) ? ProcessResult.None : ProcessResult.RefreshNodeAndColumns;
    }
    if (e is DBObjectsEventArgs)
    {
      DBObjectsEventArgs objectsEventArgs = (DBObjectsEventArgs) e;
      if (objectsEventArgs.ObjectIDs != null)
      {
        if (objectsEventArgs.ObjectIDs.Contains(this._objID))
        {
          if (e.EventName == "ObjectsCheckedOut")
          {
            this._objID = -Math.Abs(this._objID);
            this.Refresh();
            return ProcessResult.RefreshNode;
          }
          if (e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsChangesCancelled")
          {
            this._objID = Math.Abs(this._objID);
            this.Refresh();
            return ProcessResult.RefreshNode;
          }
        }
        else if (objectsEventArgs.ObjectIDs.Contains(-this._objID))
        {
          this.Refresh();
          return ProcessResult.RefreshNode;
        }
      }
    }
    if (e.EventName == "ObjectsChanged" && e is DBObjectsEventArgs)
    {
      DBObjectsEventArgs objectsEventArgs = (DBObjectsEventArgs) e;
      if (objectsEventArgs.ObjectIDs != null && objectsEventArgs.ObjectIDs.Contains(this._objID))
        return ProcessResult.RefreshNode;
    }
    return ProcessResult.None;
  }

  /// <summary>
  /// Создать частично заполненное описание узла по идентификатору связи
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <returns>Частично заполненное описание узла</returns>
  public INodeID Create(long prjLinkID)
  {
    Guid guid = Guid.Empty;
    Guid relGuid = Guid.Empty;
    long projID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(prjLinkID);
      guid = MetaDataHelper.GetRelationTypeGuid(relation.RelationType);
      relGuid = relation.GUID;
      projID = relation.ProjID;
    }
    int uniqueId = PartGuidMapper.GetUniqueId(guid);
    NodeID nodeId = new NodeID(new CreateObjectNodeParams(-1, 0L, 0L, 0L, prjLinkID, -1, string.Empty, MetaDataHelper.GetRelationTypeID(guid), 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, projID, relGuid, 0L));
    nodeId.Cookie = (object) new PartCookie(uniqueId | 1073741824 /*0x40000000*/);
    return (INodeID) nodeId;
  }
}

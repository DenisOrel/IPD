
// Type: Intermech.Client.Core.FormDesigner.Controls.ObjectsListCompositionApplicabilityNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Узел для состава/применяемости.</summary>
public class ObjectsListCompositionApplicabilityNode : ObjectNode
{
  private Guid _nodeGuid = ObjectsListConsts.CompositionNodeGuid;
  private RelatedObjectsRole _relatedRole;
  private int _relTypeID = -1;
  private int _objsTypeID = -1;
  private long _objectID;
  private long _selectionID;
  private NodeColumnCollection _defaultColumns;

  /// <summary>Конструктор.</summary>
  public ObjectsListCompositionApplicabilityNode()
    : base(-1, -1L)
  {
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots() => new List<PartSlot>();

  /// <summary>Формирование слотов-папок.</summary>
  /// <returns>Коллекция слотов-папок</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    if (!this.LoadData())
      return (List<PartSlot>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ConditionStructure[] conditions = (ConditionStructure[]) null;
      if (this._selectionID != 0L && ServicesManager.ServiceContainer.GetService(typeof (ISelectionsService)) is ISelectionsService service)
        conditions = service.GetConditionStructures((object) sessionKeeper.Session, this._selectionID, this._objectID);
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this._objectID);
      List<PartSlot> nonFolderSlots = (List<PartSlot>) null;
      if (!objectInfo.Empty)
      {
        if (this._relTypeID != -1 || this._objsTypeID > -1)
        {
          if (this._relatedRole == RelatedObjectsRole.Composition)
          {
            List<int> intList;
            if (this._relTypeID != -1)
              intList = new List<int>() { this._relTypeID };
            else
              intList = this.GetApplicabilitiesList(sessionKeeper.Session, objectInfo.ObjectTypeID, this._objsTypeID);
            nonFolderSlots = new List<PartSlot>(intList.Count);
            foreach (int relTypeID in intList)
            {
              Guid relationTypeGuid = MetaDataHelper.GetRelationTypeGuid(relTypeID);
              nonFolderSlots.Add(new PartSlot(relationTypeGuid, (INodePart) new RelatedObjectsPart(objectInfo.ObjectTypeID, this._objectID, this._relatedRole, relTypeID, this._objsTypeID, conditions, this.Services)));
            }
          }
          else
          {
            IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._objectID, false);
            if (objectActualCopy != null)
              nonFolderSlots = new List<PartSlot>()
              {
                new PartSlot(this._nodeGuid, (INodePart) new RelatedObjectsPart(objectInfo.ObjectTypeID, objectActualCopy.ObjectID, this._relatedRole, this._relTypeID, this._objsTypeID, conditions, this.Services))
              };
          }
        }
        else if (this._relatedRole == RelatedObjectsRole.Composition)
        {
          if (this.UserRole != null)
          {
            List<Guid> visibleRelationsGuids = this.UserRole.Rule.GetObjectTypeVisibleRelationsGuids(objectInfo.ObjectTypeID, true);
            if (visibleRelationsGuids != null && visibleRelationsGuids.Count > 0)
            {
              nonFolderSlots = new List<PartSlot>(visibleRelationsGuids.Count);
              foreach (Guid guid in visibleRelationsGuids)
              {
                int relationTypeId = MetaDataHelper.GetRelationTypeID(guid);
                nonFolderSlots.Add(new PartSlot(guid, (INodePart) new RelatedObjectsPart(objectInfo.ObjectTypeID, this._objectID, this._relatedRole, relationTypeId, this._objsTypeID, conditions, this.Services)));
              }
            }
          }
        }
        else
        {
          IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
          if (applicabilityCollection != null)
          {
            IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._objectID, false);
            if (objectActualCopy != null)
            {
              DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, objectInfo.ObjectTypeID, -1);
              List<int> intList = new List<int>(applicabilitiesList.Rows.Count);
              nonFolderSlots = new List<PartSlot>(applicabilitiesList.Rows.Count);
              foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
              {
                int result = -1;
                if (int.TryParse(Convert.ToString(row["F_RELATION_TYPE"]), out result) && result != -1 && !intList.Contains(result))
                {
                  Guid relationTypeGuid = MetaDataHelper.GetRelationTypeGuid(result);
                  nonFolderSlots.Add(new PartSlot(relationTypeGuid, (INodePart) new RelatedObjectsPart(objectInfo.ObjectTypeID, objectActualCopy.ObjectID, this._relatedRole, result, this._objsTypeID, conditions, this.Services)));
                  intList.Add(result);
                }
              }
            }
          }
        }
      }
      return nonFolderSlots;
    }
  }

  /// <summary>
  /// Вернуть список колонок по умолчанию для корневого узла.
  /// </summary>
  /// <param name="content">Содержание</param>
  /// <returns>Список по умолчанию для корневого узла</returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    this.LoadData();
    return this._defaultColumns == null ? (this._objTypeID == -1 ? (INode) new ObjectTypesNode() : (INode) new ObjectTypeNode(this._objTypeID, AccessRights.Enabled)).GetDefaultColumns(ContentType.NonFolders) : this._defaultColumns;
  }

  /// <summary>
  /// Вернуть список поддерживаемых колонок для корневого узла.
  /// </summary>
  /// <param name="content">Содержание</param>
  /// <param name="ColumnSetName">Имя набора колонок</param>
  /// <returns>Список поддерживаемых колонок для корневого узла</returns>
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    this.LoadData();
    return (this._objTypeID == -1 ? (INode) new ObjectTypesNode() : (INode) new ObjectTypeNode(this._objTypeID, AccessRights.Enabled)).GetSupportedColumns(ContentType.NonFolders, string.Empty);
  }

  /// <summary>
  /// Обнуление данных, если необходимо обновить информацию в контроле.
  /// </summary>
  public override void Refresh()
  {
    this.folderSlots = (List<PartSlot>) null;
    this.nonFolderSlots = (List<PartSlot>) null;
  }

  /// <summary>
  /// Получить список идентификаторов связей, с помощью которых дочерний тип объектов и его подтипы могут входить в объекты родительского типа.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="baseObjTypeID">Тип родительского объекта</param>
  /// <param name="childObjTypeID">Тип дочернего объекта</param>
  /// <returns>Список идентификаторов связей</returns>
  private List<int> GetApplicabilitiesList(
    IUserSession session,
    int baseObjTypeID,
    int childObjTypeID)
  {
    List<int> applicabilitiesList = new List<int>();
    if (session != null && baseObjTypeID != -1 && childObjTypeID != -1)
    {
      IDBRelationsApplicabilityCollection applicabilityCollection = session.GetRelationsApplicabilityCollection();
      if (applicabilityCollection != null)
      {
        foreach (int objectType in MetaDataHelper.GetObjectTypeChildrenIDRecursive(childObjTypeID))
        {
          foreach (DataRow row in (InternalDataCollectionBase) applicabilityCollection.GetApplicabilitiesList(-1, objectType, baseObjTypeID).Rows)
          {
            int result = -1;
            if (int.TryParse(Convert.ToString(row["F_RELATION_TYPE"]), out result) && result != -1 && !applicabilitiesList.Contains(result))
              applicabilitiesList.Add(result);
          }
        }
      }
    }
    return applicabilitiesList;
  }

  /// <summary>Загрузка данных.</summary>
  /// <returns>Результат загрузки</returns>
  private bool LoadData()
  {
    bool flag = false;
    if (this._services != null && this._services.GetService(typeof (ObjectsListService)) is ObjectsListService service)
    {
      this._relTypeID = service.RelationTypeID;
      this._objsTypeID = service.ObjectsTypeID;
      this._objectID = service.ObjectID;
      this._selectionID = service.SelectionID;
      this._defaultColumns = service.Columns;
      this._relatedRole = service.RelatedRole;
      this._nodeGuid = this._relatedRole == RelatedObjectsRole.Composition ? ObjectsListConsts.CompositionNodeGuid : ObjectsListConsts.ApplicabilityNodeGuid;
      flag = true;
    }
    return flag;
  }
}

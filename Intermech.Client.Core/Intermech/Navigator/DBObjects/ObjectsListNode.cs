
// Type: Intermech.Navigator.DBObjects.ObjectsListNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Узел, содержащий в своём составе объекты из указанного списка
/// </summary>
public class ObjectsListNode : CompositeNode, IContextAware
{
  /// <summary>Список идентификаторов объектов</summary>
  protected IList objectIDs;
  /// <summary>Тип указанных объектов</summary>
  protected int objectTypeID = -1;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  /// <summary>Тип указанных объектов</summary>
  protected virtual int ObjectTypeID => this.objectTypeID;

  /// <summary>Создать экземпляр узла</summary>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  public ObjectsListNode(IList objectIDs)
  {
    this.objectIDs = objectIDs;
    this.options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Создать экземпляр узла</summary>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  /// <param name="objectTypeID">Тип указанных объектов</param>
  public ObjectsListNode(IList objectIDs, int objectTypeID)
  {
    this.objectIDs = objectIDs;
    this.objectTypeID = objectTypeID;
    if (this.objectTypeID == 0)
      this.objectTypeID = -1;
    this.options |= NodeOptions.CanContainsObjectsList;
  }

  public bool LocalTypesMode { get; set; }

  public bool ShowAllModifications { get; set; }

  public bool ShowNotOwnedWorkCopies { get; set; }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateFolderSlots() => this.CreateSlots();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateNonFolderSlots() => this.CreateSlots();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="content"></param>
  /// <param name="ColumnSetName"></param>
  /// <returns></returns>
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    NodeColumnCollection supportedColumns = base.GetSupportedColumns(content, ColumnSetName);
    if (supportedColumns == null || supportedColumns.Count == 0)
      supportedColumns = Utils.DefaultSupportedColumnsObjects();
    return supportedColumns;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    NodeColumnCollection columns = base.GetDefaultColumns(content);
    if (columns == null || columns.Count == 0)
    {
      columns = columns ?? new NodeColumnCollection();
      Helper.AddObligatoryColumns(columns, true, false);
    }
    return columns;
  }

  protected virtual ObjectsListPart GetObjectsListPart(
    IList objectVersionIds,
    IServiceProvider serviceProvider,
    int objectTypeID)
  {
    return new ObjectsListPart(objectVersionIds, serviceProvider, objectTypeID);
  }

  private List<PartSlot> CreateSlots()
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(this.ObjectTypeID) && this.objectIDs != null && this.objectIDs.Count > 0)
    {
      ObjectsSelectionOptions selectionOptions = ObjectsSelectionOptions.None;
      if (this.Services != null && this.Services.GetService(typeof (ObjectsSelectionOptionsHolder)) is ObjectsSelectionOptionsHolder service)
        selectionOptions = service.Options;
      if (selectionOptions.HasFlag((Enum) ObjectsSelectionOptions.LocalTypesMode) || this.LocalTypesMode)
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
        // ISSUE: explicit reference operation
        (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
        {
          new ConditionStructure()
          {
            Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
            RelationalOperator = RelationalOperators.In,
            Value = (object) this.objectIDs,
            SQL = string.Empty
          }
        };
        dbRecordSetParams.Columns = new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
        };
        dbRecordSetParams.RecordCount = -1;
        DBRecordSetParams paramSet = dbRecordSetParams with
        {
          Tags = new HybridDictionary()
        };
        if (selectionOptions.HasFlag((Enum) ObjectsSelectionOptions.ShowNotOwnedWorkCopies) || this.ShowNotOwnedWorkCopies)
          paramSet.Tags[(object) "ShowNotOwnedWorkCopies"] = (object) true;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
          if (selectionOptions.HasFlag((Enum) ObjectsSelectionOptions.ShowAllModifications) || this.ShowAllModifications)
            objectCollection.ShowAllModifications = true;
          if (selectionOptions.HasFlag((Enum) ObjectsSelectionOptions.TrashMode))
            objectCollection.TrashMode = true;
          objectCollection.LocalTypesMode = true;
          DataTable dataTable = objectCollection.Select(paramSet);
          Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
            int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
            List<long> longList = (List<long>) null;
            if (!dictionary.TryGetValue(int32Value, out longList))
            {
              longList = new List<long>();
              dictionary.Add(int32Value, longList);
            }
            if (!longList.Contains(int64Value))
              longList.Add(int64Value);
          }
          List<PartSlot> slots = new List<PartSlot>();
          foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary)
          {
            if (keyValuePair.Value.Count > 0)
            {
              PartSlot partSlot = new PartSlot(MetaDataHelper.GetObjectTypeGuid(keyValuePair.Key), (INodePart) this.GetObjectsListPart((IList) keyValuePair.Value, this.Services, keyValuePair.Key));
              slots.Add(partSlot);
            }
          }
          return slots;
        }
      }
    }
    return this.SlotsFromSinglePart((INodePart) this.GetObjectsListPart(this.objectIDs, this.Services, this.ObjectTypeID));
  }
}

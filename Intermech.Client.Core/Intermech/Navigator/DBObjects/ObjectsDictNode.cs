
// Type: Intermech.Navigator.DBObjects.ObjectsDictNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Узел, содержащий в своём составе объекты из указанных типизированных коллекций
/// </summary>
public class ObjectsDictNode : CompositeNode, IContextAware
{
  /// <summary>Признак раскрытия состава дочерних элементов</summary>
  protected bool _expandNode = true;
  /// <summary>Типизированные коллекции версий объектов</summary>
  protected Dictionary<int, List<long>> _objectIDs;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  /// <summary>Создать экземпляр узла</summary>
  /// <param name="objectIDs">Типизированные коллекции версий объектов</param>
  /// <param name="expandNode">Признак раскрытия состава дочерних элементов</param>
  public ObjectsDictNode(Dictionary<int, List<long>> objectIDs, bool expandNode)
  {
    this._objectIDs = objectIDs;
    this._expandNode = expandNode;
    this.options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  protected List<PartSlot> CreateNonFolderSlots(IConditionsProvider conditionProvider)
  {
    if (this._objectIDs == null || this._objectIDs.Count == 0)
      return (List<PartSlot>) null;
    List<PartSlot> nonFolderSlots = new List<PartSlot>(this._objectIDs.Count);
    foreach (KeyValuePair<int, List<long>> objectId in this._objectIDs)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objectId.Key);
      if (objectId.Value.Count > 0)
        nonFolderSlots.Add(new PartSlot(objectType != null ? objectType.Guid : Intermech.Consts.CategoryObjectVersionGUID, this.GetPart(conditionProvider, (IList) objectId.Value, objectId.Key)));
    }
    return nonFolderSlots;
  }

  protected virtual INodePart GetPart(
    IConditionsProvider conditionProvider,
    IList objectIDs,
    int objectTypeID)
  {
    return (INodePart) new ObjectsListPart(objectIDs, conditionProvider, this.Services, objectTypeID, this._expandNode);
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.CreateNonFolderSlots((IConditionsProvider) null);
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-не-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.CreateNonFolderSlots((IConditionsProvider) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public override INodeQuery GetQuery(ContentType content) => base.GetQuery(content);

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
}

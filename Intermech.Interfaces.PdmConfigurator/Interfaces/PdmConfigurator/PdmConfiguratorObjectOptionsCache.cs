// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmConfiguratorObjectOptionsCache
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Кэш опций конфигуратора составов IPS, назначенных информационным объектам системы
/// </summary>
public static class PdmConfiguratorObjectOptionsCache
{
  /// <summary>Объект для синхронизации</summary>
  private static object syncRoot = new object();
  /// <summary>
  /// Промежуток времени, в течение которого содержимое опций конфигуратора составов,
  /// назначенных информационному объекту, считается актуальным
  /// </summary>
  public static TimeSpan Timeout = new TimeSpan(0, 1, 0);
  /// <summary>
  /// Кэш опций конфигуратора составов IPS, назначенных информационным объектам
  /// </summary>
  private static Dictionary<long, ObjectOptionsHolder> _items = new Dictionary<long, ObjectOptionsHolder>();

  /// <summary>
  /// Кэш опций конфигуратора составов IPS, назначенных информационным объектам
  /// </summary>
  public static Dictionary<long, ObjectOptionsHolder> Items
  {
    [DebuggerStepThrough] get => PdmConfiguratorObjectOptionsCache._items;
  }

  /// <summary>Удалить из кэша всю информацию</summary>
  public static void Reset()
  {
    lock (PdmConfiguratorObjectOptionsCache.syncRoot)
      PdmConfiguratorObjectOptionsCache._items.Clear();
  }

  /// <summary>Удалить из кэша записи с истёкшём сроком действия</summary>
  public static void ResetExpired()
  {
    lock (PdmConfiguratorObjectOptionsCache.syncRoot)
    {
      if (PdmConfiguratorObjectOptionsCache._items.Count == 0)
        return;
      List<long> longList = new List<long>();
      foreach (KeyValuePair<long, ObjectOptionsHolder> keyValuePair in PdmConfiguratorObjectOptionsCache._items)
      {
        if (DateTime.UtcNow - keyValuePair.Value.ModifiedAt > PdmConfiguratorObjectOptionsCache.Timeout)
          longList.Add(keyValuePair.Key);
      }
      for (int index = 0; index < longList.Count; ++index)
        PdmConfiguratorObjectOptionsCache._items.Remove(longList[index]);
    }
  }

  /// <summary>
  /// Удалить из кэша всю информацию, которая касается указанной опции
  /// </summary>
  /// <param name="option">Идентификатор версии объекта опции</param>
  public static void ResetOption(long option)
  {
    lock (PdmConfiguratorObjectOptionsCache.syncRoot)
    {
      if (PdmConfiguratorObjectOptionsCache._items.Count == 0)
        return;
      List<long> longList = new List<long>();
      foreach (KeyValuePair<long, ObjectOptionsHolder> keyValuePair in PdmConfiguratorObjectOptionsCache._items)
      {
        if (keyValuePair.Value.Options.IndexOf(option) >= 0)
          longList.Add(keyValuePair.Key);
      }
      for (int index = 0; index < longList.Count; ++index)
        PdmConfiguratorObjectOptionsCache._items.Remove(longList[index]);
    }
  }

  /// <summary>
  /// Получить из кэша опции, назначенные указанному объекту
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <returns>Опции, назначенные указанному объекту, или null</returns>
  public static ObjectOptionsHolder GetObjectOptions(long objectID)
  {
    PdmConfiguratorObjectOptionsCache.ResetExpired();
    lock (PdmConfiguratorObjectOptionsCache.syncRoot)
    {
      if (objectID != 0L)
      {
        if (PdmConfiguratorObjectOptionsCache._items.ContainsKey(objectID))
          return PdmConfiguratorObjectOptionsCache._items[objectID];
      }
    }
    return (ObjectOptionsHolder) null;
  }

  /// <summary>Записать в кэш опции, назначеннные указанному объекту</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="value">Опции, назначенные указанному объекту</param>
  /// <returns>Опции, назначенные указанному объекту, или null</returns>
  public static bool SetObjectOptions(long objectID, ObjectOptionsHolder value)
  {
    PdmConfiguratorObjectOptionsCache.ResetExpired();
    if (objectID == 0L)
      return false;
    if (value == null)
    {
      lock (PdmConfiguratorObjectOptionsCache.syncRoot)
      {
        if (PdmConfiguratorObjectOptionsCache._items.ContainsKey(objectID))
        {
          PdmConfiguratorObjectOptionsCache._items.Remove(objectID);
          return true;
        }
      }
      return false;
    }
    lock (PdmConfiguratorObjectOptionsCache.syncRoot)
      PdmConfiguratorObjectOptionsCache._items[objectID] = value;
    return true;
  }

  /// <summary>
  /// Получить из кэша опции, назначенные указанному объекту. Если в кэше информации нет,
  /// попробовать найти её в базе данных
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <returns>Опции, назначенные указанному объекту, или null</returns>
  public static ObjectOptionsHolder GetOrLoadObjectOptions(IUserSession session, long objectID)
  {
    PdmConfiguratorObjectOptionsCache.ResetExpired();
    lock (PdmConfiguratorObjectOptionsCache.syncRoot)
    {
      if (objectID != 0L)
      {
        if (PdmConfiguratorObjectOptionsCache._items.ContainsKey(objectID))
          return PdmConfiguratorObjectOptionsCache._items[objectID];
      }
    }
    if (session == null)
      return (ObjectOptionsHolder) null;
    ObjectOptionsHolder loadObjectOptions = new ObjectOptionsHolder((object) session.GetObject(objectID, false));
    if (loadObjectOptions.ObjectID == 0L)
      return (ObjectOptionsHolder) null;
    PdmConfiguratorObjectOptionsCache.SetObjectOptions(objectID, loadObjectOptions);
    return loadObjectOptions;
  }

  /// <summary>
  /// Загрузить в кэш опции информационных объектов, входящих в состав указанного родительского объекта
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="relType">Идентификатор типа связи, по которому надо получать состав</param>
  /// <param name="filtrationOwnerID">Ключ настроек фильтрации составов</param>
  /// <returns>Опции информационных объектов, входящих в состав указанного родительского объекта</returns>
  public static List<ObjectOptionsHolder> CacheLoadObjectsOptions(
    IUserSession session,
    long projID,
    int relType,
    string filtrationOwnerID)
  {
    List<ObjectOptionsHolder> objectOptionsHolderList = new List<ObjectOptionsHolder>();
    if (session == null || projID == 0L || relType == -1 || !(session.GetCustomService(typeof (ICompositionLoadService)) is ICompositionLoadService customService))
      return objectOptionsHolderList;
    List<TypedObjectInfo> objects = customService.LoadCompositionTypedObjects((object) session.SessionGUID, projID, relType, filtrationOwnerID);
    return objects != null && objects.Count > 0 ? PdmConfiguratorObjectOptionsCache.CacheLoadObjectsOptions(session, (IList<TypedObjectInfo>) objects) : objectOptionsHolderList;
  }

  /// <summary>
  /// Загрузить в кэш опции указанных информационных объектов
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="objects">Список идентификаторов версий объектов и их типов данных</param>
  /// <returns>Опции указанных информационных объектов</returns>
  public static List<ObjectOptionsHolder> CacheLoadObjectsOptions(
    IUserSession session,
    IList<TypedObjectInfo> objects)
  {
    List<ObjectOptionsHolder> objectOptionsHolderList = new List<ObjectOptionsHolder>();
    if (session == null || objects == null || objects.Count == 0)
      return objectOptionsHolderList;
    List<int> intList = new List<int>();
    List<TypedObjectInfo> typedObjectInfoList = new List<TypedObjectInfo>();
    List<long> longList = new List<long>();
    for (int index = 0; index < objects.Count; ++index)
    {
      if (typedObjectInfoList.IndexOf(objects[index]) < 0)
      {
        typedObjectInfoList.Add(objects[index]);
        longList.Add(objects[index].F_OBJECT_ID);
        if (intList.IndexOf(objects[index].F_OBJECT_TYPE) < 0)
          intList.Add(objects[index].F_OBJECT_TYPE);
      }
    }
    if (typedObjectInfoList.Count == 0 || intList.Count == 0)
      return objectOptionsHolderList;
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[ObjectOptionsHolder.GetSelectColumns().Count];
    ObjectOptionsHolder.GetSelectColumns().CopyTo(columnDescriptorArray);
    for (int index1 = 0; index1 < intList.Count && longList.Count != 0; ++index1)
    {
      DataTable dataTable = (DataTable) null;
      object[] objArray = new object[0];
      SortOrders[] sortOrdersArray = new SortOrders[0];
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, longList.Count == 1 ? RelationalOperators.Equal : RelationalOperators.In, longList.Count == 1 ? (object) longList[0] : (object) longList.ToArray(), (object) null, LogicalOperators.NONE, 0, true, AttributeSourceTypes.Object, ColumnContents.Text)
      }, columnDescriptorArray);
      IDBObjectCollection objectCollection = session.GetObjectCollection(intList[index1]);
      try
      {
        dataTable = objectCollection.Select(paramSet);
      }
      catch
      {
      }
      if (dataTable != null)
      {
        try
        {
          dataTable.ExtendedProperties[(object) "IUserSession"] = (object) session;
          lock (PdmConfiguratorObjectOptionsCache.syncRoot)
          {
            for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
            {
              ObjectOptionsHolder objectOptionsHolder = new ObjectOptionsHolder((object) dataTable.Rows[index2]);
              PdmConfiguratorObjectOptionsCache.SetObjectOptions(objectOptionsHolder.ObjectID, objectOptionsHolder);
              longList.Remove(objectOptionsHolder.ObjectID);
              objectOptionsHolderList.Add(objectOptionsHolder);
            }
          }
        }
        finally
        {
          dataTable.ExtendedProperties[(object) "IUserSession"] = (object) null;
          dataTable.Dispose();
        }
      }
    }
    return objectOptionsHolderList;
  }
}


// Type: Intermech.CacheServices.ObjectTypeHierarchy
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Cache;
using Intermech.Cache.Performance;
using Intermech.Cache.Policies;
using Intermech.Cache.Storages;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Data;


namespace Intermech.CacheServices;

internal class ObjectTypeHierarchy : IObjectTypeHierarchy, ICacheService
{
  private ICacheManager cacheManager;
  private IPerformanceCounter fillTime;
  private static readonly ObjectTypeHierarchy.Key hierarchyTableKey = new ObjectTypeHierarchy.Key(ObjectTypeHierarchy.KeyType.HierarchyTable);
  private static readonly int[] empty = new int[0];

  public ObjectTypeHierarchy()
  {
    this.cacheManager = (ICacheManager) new CacheManager((IStorage) new InMemoryStorage(192L /*0xC0*/), (IReplacementPolicy) new Lru());
    this.fillTime = (IPerformanceCounter) new NumberOfItems(string.Empty, LocalizationHolder.rm.GetString("Client.Core_13"), LocalizationHolder.rm.GetString("Client.Core_14"), LocalizationHolder.rm.GetString("Client.Core_15"));
    this.PerformanceCounters.Add(this.fillTime);
  }

  public PerformanceCounterCollection PerformanceCounters => this.cacheManager.PerformanceCounters;

  /// <summary>
  /// Проверить, доступен ли по правам доступа и предметным областям указанный тип объектов
  /// </summary>
  /// <param name="objTypeID">Проверяемый тип объектов</param>
  /// <returns>true - объект доступен</returns>
  public bool EnabledObjectType(int objTypeID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(-2, true).Select(string.Empty);
      if (dataTable == null || dataTable.Rows.Count == 0)
        return false;
      DataRow[] dataRowArray = dataTable.Select($"{"F_OBJECT_TYPE"}={objTypeID}");
      return dataRowArray != null && dataRowArray.Length != 0;
    }
  }

  /// <summary>
  /// Возвращает идентификатор родительского типа объектов. Если результат равен -1, то
  /// это корневой тип объектов.
  /// </summary>
  /// <param name="childTypeID">Идентификатор дочернего типа объектов</param>
  /// <returns>Идентификатор родительского типа объектов</returns>
  public int GetParentType(int childTypeID)
  {
    ObjectTypeHierarchy.Key key = new ObjectTypeHierarchy.Key(ObjectTypeHierarchy.KeyType.Parent, (object) childTypeID);
    object data = this.cacheManager[(object) key];
    if (data == null)
    {
      int tickCount = Environment.TickCount;
      data = (object) -1;
      DataTable hierarchyTable = this.GetHierarchyTable();
      int columnIndex1 = hierarchyTable.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex2 = hierarchyTable.Columns.IndexOf("F_PARENT_ID");
      for (int index = 0; index < hierarchyTable.Rows.Count; ++index)
      {
        DataRow row = hierarchyTable.Rows[index];
        if (Convert.ToInt32(row[columnIndex1]) == childTypeID)
        {
          data = (object) Convert.ToInt32(row[columnIndex2]);
          break;
        }
      }
      this.cacheManager.Add((object) key, data);
      this.fillTime.IncrementBy((long) (Environment.TickCount - tickCount));
    }
    return (int) data;
  }

  /// <summary>
  /// Возвращает идентификаторы всех родительских типов объектов для указанного типа объектов.
  /// </summary>
  /// <param name="childTypeID">Идентификатор дочернего типа объектов</param>
  /// <returns>Массив идентификаторов родительских типов объектов</returns>
  public int[] GetParentTypes(int childTypeID)
  {
    ObjectTypeHierarchy.Key key = new ObjectTypeHierarchy.Key(ObjectTypeHierarchy.KeyType.ParentsArray, (object) childTypeID);
    object array = this.cacheManager[(object) key];
    if (array == null)
    {
      int tickCount = Environment.TickCount;
      DataTable hierarchyTable = this.GetHierarchyTable();
      int columnIndex1 = hierarchyTable.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex2 = hierarchyTable.Columns.IndexOf("F_PARENT_ID");
      ArrayList arrayList = new ArrayList();
      while (childTypeID != -1)
      {
        bool flag = true;
        for (int index = 0; index < hierarchyTable.Rows.Count; ++index)
        {
          DataRow row = hierarchyTable.Rows[index];
          if (Convert.ToInt32(row[columnIndex1]) == childTypeID)
          {
            int int32 = Convert.ToInt32(row[columnIndex2]);
            arrayList.Add((object) int32);
            childTypeID = int32;
            flag = false;
            break;
          }
        }
        if (flag)
          break;
      }
      array = (object) (int[]) arrayList.ToArray(typeof (int));
      this.cacheManager.Add((object) key, array);
      this.fillTime.IncrementBy((long) (Environment.TickCount - tickCount));
    }
    return (int[]) array;
  }

  private DataTable GetHierarchyTable()
  {
    DataTable typesHierarchy = (DataTable) this.cacheManager[(object) ObjectTypeHierarchy.hierarchyTableKey];
    if (typesHierarchy == null)
    {
      lock (this)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          typesHierarchy = sessionKeeper.Session.GetObjectTypeCollection(-1, true).GetTypesHierarchy();
        this.cacheManager.Add((object) ObjectTypeHierarchy.hierarchyTableKey, (object) typesHierarchy);
      }
    }
    return typesHierarchy;
  }

  private enum KeyType
  {
    HierarchyTable,
    Parent,
    ParentsArray,
  }

  private class Key
  {
    public ObjectTypeHierarchy.KeyType KeyType;
    public object Data;

    public Key(ObjectTypeHierarchy.KeyType keyType, object data)
    {
      this.KeyType = keyType;
      this.Data = data;
    }

    public Key(ObjectTypeHierarchy.KeyType keyType)
      : this(keyType, (object) 0)
    {
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObjectTypeHierarchy.Key key))
        return base.Equals(obj);
      return this.KeyType == key.KeyType && this.Data.Equals(key.Data);
    }

    public override int GetHashCode() => this.KeyType.GetHashCode() << 24 ^ this.Data.GetHashCode();
  }
}

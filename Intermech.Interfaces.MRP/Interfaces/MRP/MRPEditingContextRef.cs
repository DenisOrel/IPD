// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPEditingContextRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, ссылающийся на контейнер версий объектов, которые требуется добавить в какой-либо контекст редактирования
/// </summary>
/// <summary>
/// Создать контейнер версий объектов для добавления в контекст редактирования
/// </summary>
/// <param name="services">Контейнер сервисов (контекст)</param>
public class MRPEditingContextRef(IServiceProvider services) : 
  MRPContext(services),
  IMRPEditingContextRef,
  IMRPContext
{
  /// <summary>Список идентификаторов версий объектов</summary>
  private List<long> items = new List<long>();
  /// <summary>Список идентификаторов объектов</summary>
  private List<long> fIDs = new List<long>();
  /// <summary>Список типов объектов</summary>
  private List<int> typeIDs = new List<int>();

  /// <summary>Список версий объектов в контейнере (копия списка)</summary>
  public List<long> Items
  {
    get
    {
      lock (this.items)
        return new List<long>((IEnumerable<long>) this.items);
    }
  }

  /// <summary>Список объектов в контейнере (копия списка)</summary>
  public List<long> ItemsF_ID
  {
    get
    {
      lock (this.fIDs)
        return new List<long>((IEnumerable<long>) this.fIDs);
    }
  }

  /// <summary>Список версий объектов в контейнере (копия списка)</summary>
  public List<int> ItemTypes
  {
    get
    {
      lock (this.typeIDs)
        return new List<int>((IEnumerable<int>) this.typeIDs);
    }
  }

  /// <summary>Добавить версию объекта в контейнер</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="fID">Идентификатор объекта</param>
  /// <param name="typeID">Идентификатор типа объекта</param>
  public void Add(long objectID, long fID, int typeID)
  {
    if (objectID == 0L)
      throw new ArgumentException();
    lock (this.items)
    {
      if (this.items.IndexOf(objectID) >= 0)
        return;
      this.items.Add(objectID);
      this.fIDs.Add(fID);
      this.typeIDs.Add(typeID);
    }
  }

  /// <summary>Добавить версии объектов в контейнер</summary>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="fIDs">Список идентификаторов объектов</param>
  /// <param name="typeIDs">Список типов объектов</param>
  public void Add(IList<long> objectIDs, IList<long> fIDs, IList<int> typeIDs)
  {
    if (objectIDs == null)
      throw new ArgumentNullException(nameof (objectIDs));
    if (fIDs == null)
      throw new ArgumentNullException(nameof (fIDs));
    if (typeIDs == null)
      throw new ArgumentNullException(nameof (typeIDs));
    if (objectIDs.Count == 0)
      return;
    if (objectIDs.Count != fIDs.Count || objectIDs.Count != typeIDs.Count)
      throw new ArgumentException();
    lock (this.items)
    {
      for (int index = 0; index < objectIDs.Count; ++index)
      {
        long objectId = objectIDs[index];
        if (objectId == 0L)
          throw new ArgumentException();
        if (this.items.IndexOf(objectId) < 0)
        {
          this.items.Add(objectId);
          this.fIDs.Add(fIDs[index]);
          this.typeIDs.Add(typeIDs[index]);
        }
      }
    }
  }

  /// <summary>Проверить наличие версии объекта в контейнере</summary>
  /// <param name="objectID">Искомая версия объекта</param>
  /// <returns>true - версия найдена в контейнере</returns>
  public bool Exists(long objectID)
  {
    lock (this.items)
      return this.items.IndexOf(objectID) >= 0;
  }
}

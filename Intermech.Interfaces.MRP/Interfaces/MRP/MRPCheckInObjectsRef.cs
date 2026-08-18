// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCheckInObjectsRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, ссылающийся на контейнер версий объектов, для которых требуется завершить изменения
/// </summary>
/// <summary>
/// Создать контейнер версий объектов, для которых требуется завершить изменения
/// </summary>
/// <param name="services">Контейнер сервисов (контекст)</param>
public class MRPCheckInObjectsRef(IServiceProvider services) : 
  MRPContext(services),
  IMRPCheckInObjectsRef,
  IMRPContext
{
  /// <summary>
  /// Список описаний версий объектов в контейнере (копия списка)
  /// </summary>
  private List<IMRPObjectRef> items = new List<IMRPObjectRef>();

  /// <summary>Список версий объектов в контейнере (копия списка)</summary>
  public List<IMRPObjectRef> Items
  {
    get
    {
      lock (this.items)
        return new List<IMRPObjectRef>((IEnumerable<IMRPObjectRef>) this.items);
    }
  }

  /// <summary>Добавить описание версии объекта в контейнер</summary>
  /// <param name="objRef">Описание версии объекта</param>
  public void Add(IMRPObjectRef objRef)
  {
    if (objRef == null)
      throw new ArgumentNullException(nameof (objRef));
    if (objRef.ObjectID == 0L)
      throw new ArgumentException();
    lock (this.items)
    {
      if (this.Exists(objRef.ObjectID))
        return;
      this.items.Add(objRef);
    }
  }

  /// <summary>Добавить описания версий объектов в контейнер</summary>
  /// <param name="objRefs">Список описаний версий объектов</param>
  public void Add(IList<IMRPObjectRef> objRefs)
  {
    if (objRefs == null)
      throw new ArgumentNullException(nameof (objRefs));
    if (objRefs.Count == 0)
      return;
    lock (this.items)
    {
      for (int index = 0; index < objRefs.Count; ++index)
      {
        IMRPObjectRef objRef = objRefs[index];
        if (objRef == null)
          throw new ArgumentNullException("objRefs[i]");
        if (objRef.ObjectID == 0L)
          throw new ArgumentException();
        if (!this.Exists(objRef.ObjectID))
          this.items.Add(objRef);
      }
    }
  }

  /// <summary>Проверить наличие версии объекта в контейнере</summary>
  /// <param name="objectID">Искомая версия объекта (знак не имеет значения)</param>
  /// <returns>true - версия найдена в контейнере</returns>
  public bool Exists(long objectID)
  {
    lock (this.items)
      return this.items.Exists((Predicate<IMRPObjectRef>) (obj => Math.Abs(obj.ObjectID) == Math.Abs(objectID)));
  }
}

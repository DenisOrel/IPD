// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPNavigatorEventsRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, ссылающийся на коллекции, позволяющие сформировать уведомления для Навигатора
/// </summary>
[Serializable]
/// <summary>
/// Создать контейнер версий объектов для добавления в контекст редактирования
/// </summary>
/// <param name="services">Контейнер сервисов (контекст)</param>
public class MRPNavigatorEventsRef(IServiceProvider services) : 
  MRPContext(services),
  IMRPNavigatorEventsRef,
  IMRPContext
{
  /// <summary>
  /// Если значение установлено в значение False, использовать содержимое полей класса запрещено
  /// </summary>
  public volatile bool Enabled = true;
  /// <summary>Список идентификаторов созданных объектов</summary>
  public List<long> ObjCreatedIDs = new List<long>();
  /// <summary>Список идентификаторов типов созданных объектов</summary>
  public List<int> ObjCreatedTypeIDs = new List<int>();
  /// <summary>Список идентификаторов изменённых объектов</summary>
  public List<long> ObjChangedIDs = new List<long>();
  /// <summary>Список идентификаторов типов изменённых объектов</summary>
  public List<int> ObjChangedTypeIDs = new List<int>();
  /// <summary>Список идентификаторов удалённых объектов</summary>
  public List<long> ObjDeletedIDs = new List<long>();
  /// <summary>Список идентификаторов типов удалённых объектов</summary>
  public List<int> ObjDeletedTypeIDs = new List<int>();
  /// <summary>Список идентификаторов созданных связей</summary>
  public List<long> RelCreatedIDs = new List<long>();
  /// <summary>Список идентификаторов типов созданных связей</summary>
  public List<int> RelCreatedTypeIDs = new List<int>();
  /// <summary>
  /// Список идентификаторов версий родительских объектов для созданных связей
  /// </summary>
  public List<long> RelCreatedProjIDs = new List<long>();
  /// <summary>
  /// Список идентификаторов типов версий родительских объектов для созданных связей
  /// </summary>
  public List<int> RelCreatedProjTypeIDs = new List<int>();
  /// <summary>Список идентификаторов удалённых связей</summary>
  public List<long> RelDeletedIDs = new List<long>();
  /// <summary>Список идентификаторов типов удалённых связей</summary>
  public List<int> RelDeletedTypeIDs = new List<int>();

  /// <summary>Добавить в контейнер информацию о созданном объекте</summary>
  /// <param name="objID">Идентификтор версии объекта</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  public void AddCreatedObject(long objID, int objTypeID)
  {
    if (objID == 0L)
      throw new ArgumentException();
    lock (this.ObjCreatedIDs)
    {
      if (this.ObjCreatedIDs.IndexOf(objID) >= 0)
        return;
      this.ObjCreatedIDs.Add(objID);
      this.ObjCreatedTypeIDs.Add(objTypeID);
    }
  }

  /// <summary>Добавить в контейнер информацию об изменённом объекте</summary>
  /// <param name="objID">Идентификтор версии объекта</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  public void AddChangedObject(long objID, int objTypeID)
  {
    if (objID == 0L)
      throw new ArgumentException();
    lock (this.ObjChangedIDs)
    {
      if (this.ObjChangedIDs.IndexOf(objID) >= 0)
        return;
      this.ObjChangedIDs.Add(objID);
      this.ObjChangedTypeIDs.Add(objTypeID);
    }
  }

  /// <summary>Добавить в контейнер информацию об удалённом объекте</summary>
  /// <param name="objID">Идентификтор версии объекта</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  public void AddDeletedObject(long objID, int objTypeID)
  {
    if (objID == 0L)
      throw new ArgumentException();
    lock (this.ObjDeletedIDs)
    {
      if (this.ObjDeletedIDs.IndexOf(objID) >= 0)
        return;
      this.ObjDeletedIDs.Add(objID);
      this.ObjDeletedTypeIDs.Add(objTypeID);
    }
  }

  /// <summary>Добавить в контейнер информацию о созданной связи</summary>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="projTypeID">Идентификатор типа родительского объекта</param>
  public void AddCreatedRelation(long relID, int relTypeID, long projID, int projTypeID)
  {
    if (relID == 0L)
      throw new ArgumentException();
    lock (this.RelCreatedIDs)
    {
      if (this.RelCreatedIDs.IndexOf(relID) >= 0)
        return;
      this.RelCreatedIDs.Add(relID);
      this.RelCreatedTypeIDs.Add(relTypeID);
      this.RelCreatedProjIDs.Add(projID);
      this.RelCreatedProjTypeIDs.Add(projTypeID);
    }
  }

  /// <summary>Добавить в контейнер информацию об удалённой связи</summary>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  public void AddDeletedRelation(long relID, int relTypeID)
  {
    if (relID == 0L)
      throw new ArgumentException();
    lock (this.RelDeletedIDs)
    {
      if (this.RelDeletedIDs.IndexOf(relID) >= 0)
        return;
      this.RelDeletedIDs.Add(relID);
      this.RelDeletedTypeIDs.Add(relTypeID);
    }
  }
}

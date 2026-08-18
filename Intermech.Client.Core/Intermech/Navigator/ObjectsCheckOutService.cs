
// Type: Intermech.Navigator.ObjectsCheckOutService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Сервис "Навигатора", позволяющий брать на изменение группы объектов
/// </summary>
public sealed class ObjectsCheckOutService : IObjectsCheckOutService
{
  /// <summary>
  /// Событие, которое вызывается после получения редактируемых копий объектов, для дальнейшего анализа и обработки
  /// </summary>
  public event Intermech.Interfaces.Client.ObjectsCheckOutEventHandler ObjectsCheckOutEventHandler;

  /// <summary>
  /// Сервис "Навигатора", позволяющий брать на изменение группы объектов
  /// </summary>
  public IList<long> CheckOut(IUserSession session, IList<long> versions, bool throwException)
  {
    if (session == null)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1553"));
      return (IList<long>) null;
    }
    if (versions == null || versions.Count == 0)
      return (IList<long>) null;
    List<long> versions1 = new List<long>(versions.Count);
    for (int index = 0; index < versions.Count; ++index)
    {
      long version = versions[index];
      if (version != 0L && versions1.IndexOf(version) < 0)
        versions1.Add(version);
    }
    if (versions1.Count == 0)
      return (IList<long>) null;
    try
    {
      IObjectsCheckOutServerService customService = session.GetCustomService(typeof (IObjectsCheckOutServerService)) as IObjectsCheckOutServerService;
      List<ObjectCheckOutVersionDescription> collection = customService.LoadDescriptions((object) session.SessionGUID, (IList<long>) versions1, throwException);
      if (collection == null || collection.Count != versions1.Count)
      {
        if (throwException)
          throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1554"));
        return (IList<long>) null;
      }
      ObjectCheckedOutVersionsHolder result = customService.CheckOut((object) session.SessionGUID, (IList<long>) versions1, throwException);
      List<ObjectCheckOutVersionDescription> sourceVersions = new List<ObjectCheckOutVersionDescription>(collection.Count + result.PairVersionSources.Count);
      sourceVersions.AddRange((IEnumerable<ObjectCheckOutVersionDescription>) collection);
      sourceVersions.AddRange((IEnumerable<ObjectCheckOutVersionDescription>) result.PairVersionSources);
      List<ObjectCheckOutVersionDescription> resultVersions = new List<ObjectCheckOutVersionDescription>(result.Objects.Count + result.PairVersionTargets.Count);
      resultVersions.AddRange((IEnumerable<ObjectCheckOutVersionDescription>) result.Objects);
      resultVersions.AddRange((IEnumerable<ObjectCheckOutVersionDescription>) result.PairVersionTargets);
      ObjectsCheckOutEventArgs e = new ObjectsCheckOutEventArgs(sourceVersions, resultVersions);
      this.FireObjectsCheckOutEvent(e, session, result, throwException);
      if (e.Handled && !e.Rollback)
      {
        List<long> longList = new List<long>();
        for (int index = 0; index < result.Objects.Count; ++index)
          longList.Add(result.Objects[index].F_OBJECT_ID);
        return (IList<long>) longList;
      }
      if (result.Objects != null)
      {
        for (int index = result.Objects.Count - 1; index >= 0; --index)
        {
          if (result.Objects[index].Mode == ObjectCheckedOutVersionMode.NewVersion)
          {
            this.RemoveDBObject(session, result.Objects[index].F_OBJECT_ID);
            if (result.Objects[index].F_OBJECT_ID < 0L)
              this.RemoveDBObject(session, Math.Abs(result.Objects[index].F_OBJECT_ID));
          }
        }
      }
      if (throwException)
        throw new KernelException("Нельзя выпустить версию объекта без включения его в извещение");
    }
    catch
    {
      if (throwException)
        throw;
    }
    return (IList<long>) null;
  }

  private void RemoveDBObject(IUserSession session, long objectId)
  {
    session.GetObject(objectId, false)?.Delete(0L);
  }

  /// <summary>Получить список версий объектов для редактирования</summary>
  /// <param name="session">Сессия, в рамках которой выполняется работа</param>
  /// <param name="versions">Список описаний версий объектов, которые требуется взять на изменение</param>
  /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
  /// <returns>Список версий объектов для редактирования. Если возникла ошибка, будет возвращено значение null</returns>
  public IList<long> CheckOut(IUserSession session, IList<IDBObject> versions, bool throwException)
  {
    if (session == null)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1553"));
      return (IList<long>) null;
    }
    if (versions == null || versions.Count == 0)
      return (IList<long>) null;
    List<long> versions1 = new List<long>(versions.Count);
    for (int index = 0; index < versions.Count; ++index)
    {
      if (versions[index] != null)
      {
        long objectId = versions[index].ObjectID;
        if (objectId != 0L && versions1.IndexOf(objectId) < 0)
          versions1.Add(objectId);
      }
    }
    return this.CheckOut(session, (IList<long>) versions1, throwException);
  }

  /// <summary>Сгенерировать событие "ObjectsCheckOutEventHandler"</summary>
  /// <param name="e">Аргументы события</param>
  /// <param name="session">Сессия</param>
  /// <param name="result">Результаты запроса к серверной службе</param>
  /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
  internal void FireObjectsCheckOutEvent(
    ObjectsCheckOutEventArgs e,
    IUserSession session,
    ObjectCheckedOutVersionsHolder result,
    bool throwException)
  {
    if (e == null)
      return;
    if (this.ObjectsCheckOutEventHandler == null)
    {
      e.Handled = true;
    }
    else
    {
      foreach (Intermech.Interfaces.Client.ObjectsCheckOutEventHandler invocation in this.ObjectsCheckOutEventHandler.GetInvocationList())
      {
        e.Handled = false;
        e.Rollback = false;
        try
        {
          invocation((object) this, e);
          if (e.Rollback)
          {
            this.Rollback(session, result, throwException);
            break;
          }
        }
        catch
        {
          this.Rollback(session, result, throwException);
          if (!throwException)
            return;
          throw;
        }
      }
      if (session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService && customService.InCreationLogMode)
        customService.CommitCreationLog();
      e.Handled = true;
    }
  }

  /// <summary>
  /// Выполнить откат произведённых изменений по списку-результату метода CheckOut
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="rollback">Список редактируемых версий объектов, полученных в методе CheckOut</param>
  /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
  internal void Rollback(
    IUserSession session,
    ObjectCheckedOutVersionsHolder rollback,
    bool throwException)
  {
    if (session == null || rollback == null || !(session.GetCustomService(typeof (IObjectsCheckOutServerService)) is IObjectsCheckOutServerService customService1))
      return;
    customService1.Rollback((object) session.SessionGUID, rollback, throwException);
    if (!(session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService2) || !customService2.InCreationLogMode)
      return;
    customService2.RollBackCreationLog();
  }

  /// <summary>
  /// Генерирует событие для дальнейшей обработки редактируемых копий объектов. Используется в тех случаях, когда редактируемые объекты создаются другим сервисов IPS.
  /// </summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  public void FireObjectsCheckOutEvent(object sender, ObjectsCheckOutEventArgs e)
  {
    if (e == null)
      throw new ArgumentNullException(nameof (e), LocalizationHolder.rm.GetString("Client.Core_1555"));
    if (this.ObjectsCheckOutEventHandler == null)
      return;
    this.ObjectsCheckOutEventHandler(sender, e);
  }
}

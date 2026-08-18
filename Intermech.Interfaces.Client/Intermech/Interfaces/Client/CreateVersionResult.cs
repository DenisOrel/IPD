// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CreateVersionResult
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Описывает результат создания версии объекта с учетом выпуска версий парных объектов.
/// ВНИМАНИЕ! При получении объекта этого класса на сервере остается включенным режим
/// регистрации создаваемых объектов и связей. Необходимо подтвердить или откатить
/// операцию создания версии с помощью методов Commit/Rollback у этого класса.
/// </summary>
public sealed class CreateVersionResult
{
  private const int PairVersionsHeuristic = 16 /*0x10*/;
  private long objectId;
  private IDBObject newObjectVersion;
  private List<ObjectCheckOutVersionDescription> sourceVersions;
  private List<ObjectCheckOutVersionDescription> targetVersions;

  /// <summary>Создает объект.</summary>
  /// <param name="objectId">Идентификатор исходной версии объекта</param>
  /// <param name="newObjectVersion">Новая версия объекта</param>
  /// <exception cref="T:System.ArgumentException">Идентификатор исходной версии объекта не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на новую версию объекта не может быть null</exception>
  public CreateVersionResult(long objectId, IDBObject newObjectVersion)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (newObjectVersion == null)
      throw new ArgumentNullException();
    this.objectId = objectId;
    this.newObjectVersion = newObjectVersion;
    this.sourceVersions = new List<ObjectCheckOutVersionDescription>(16 /*0x10*/);
    this.targetVersions = new List<ObjectCheckOutVersionDescription>(16 /*0x10*/);
  }

  /// <summary>
  /// Подтверждает создание версии исходного объекта и версий парных ему объектов.
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на пользовательскую сессию не может быть null</exception>
  public void Commit(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException();
    ServiceUtils.GetService<IDBTransactions>((object) session, true).CommitCreationLog();
  }

  /// <summary>
  /// Отменяет создание версии исходного объекта и версий парных ему объектов. Все созданные версии объектов
  /// будут удалены.
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на пользовательскую сессию не может быть null</exception>
  public void Rollback(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException();
    ServiceUtils.GetService<IDBTransactions>((object) session, true).RollBackCreationLog();
  }

  /// <summary>Возвращает идентификатор исходной версии объекта.</summary>
  public long ObjectId => this.objectId;

  /// <summary>Возвращает новую версию исходной версии объекта.</summary>
  public IDBObject NewObjectVersion => this.newObjectVersion;

  /// <summary>
  /// Возвращает список дескрипторов исходных версий объектов.
  /// </summary>
  public List<ObjectCheckOutVersionDescription> SourceVersions => this.sourceVersions;

  /// <summary>
  /// Возвращает список дескрипторов созданных версий объектов. Этот список попарно соответствует
  /// содержимому SourceVersions.
  /// </summary>
  public List<ObjectCheckOutVersionDescription> TargetVersions => this.targetVersions;
}

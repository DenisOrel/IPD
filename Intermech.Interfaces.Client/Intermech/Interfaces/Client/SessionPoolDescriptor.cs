// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SessionPoolDescriptor
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Описывает открытую пользовательскую сессию и ресурсы, связанные с ней.
/// </summary>
internal sealed class SessionPoolDescriptor : IUserSessionDescriptor
{
  private readonly IUserSession session;
  private int usageCount;
  private DateTime lastAccessTimeUtc;
  private SessionPoolThreadKey threadKey;
  private SessionPoolThreadKey ownerThreadKey;
  private UserSessionReleaseMode releaseMode;

  /// <summary>Создает дескриптор пользовательской сессии.</summary>
  /// <param name="session">Пользовательская сессия</param>
  public SessionPoolDescriptor(IUserSession session)
  {
    this.session = session != null ? session : throw new ArgumentNullException(nameof (session));
    this.usageCount = 0;
    this.lastAccessTimeUtc = DateTime.UtcNow;
    this.releaseMode = UserSessionReleaseMode.Normal;
  }

  /// <summary>
  /// Сигнализирует, что у пользовательской сессии появился еще один клиент.
  /// </summary>
  public void BeginUsage()
  {
    ++this.usageCount;
    this.lastAccessTimeUtc = DateTime.UtcNow;
  }

  /// <summary>
  /// Сигнализирует, что у пользовательской сессии стало на одного клиента меньше.
  /// </summary>
  public void EndUsage()
  {
    if (this.usageCount <= 0)
      return;
    --this.usageCount;
  }

  /// <summary>
  /// Возвращает количество активных клиентов у выделенной пользовательской сессии.
  /// </summary>
  public int UsageCount
  {
    [DebuggerStepThrough] get => this.usageCount;
  }

  /// <summary>
  /// Возвращает время последнего обращения к пользовательской сессии.
  /// </summary>
  public DateTime LastAccessTimeUtc
  {
    [DebuggerStepThrough] get => this.lastAccessTimeUtc;
  }

  /// <summary>
  /// Возвращает пользовательскую сессию, описываемую дескриптором.
  /// </summary>
  public IUserSession Session
  {
    [DebuggerStepThrough] get => this.session;
  }

  /// <summary>
  /// Задает или возвращает ключ потока, которому выделена сессия.
  /// </summary>
  public SessionPoolThreadKey ThreadKey
  {
    [DebuggerStepThrough] get => this.threadKey;
    [DebuggerStepThrough] set => this.threadKey = value;
  }

  /// <summary>
  /// Возвращает или задает ключ потока, за которым закреплена сессия.
  /// Из пула такая сессия будет выдаваться только по запросу от этого потока.
  /// Значение свойства может быть не задано и равно null.
  /// </summary>
  public SessionPoolThreadKey OwnerThreadKey
  {
    [DebuggerStepThrough] get => this.ownerThreadKey;
    [DebuggerStepThrough] set => this.ownerThreadKey = value;
  }

  /// <summary>
  /// Возвращает признак переиспользования сессии при вложенном создании SessionKeeper.
  /// Если создание SessionKeeper не вложено в область действия другого SessionKeeper,
  /// то значение свойства будет равно true, во всех остальных случаях - false.
  /// </summary>
  public bool IsTopmost
  {
    [DebuggerStepThrough] get => this.usageCount == 1;
  }

  /// <summary>Возвращает текущий режим освобождения сессии.</summary>
  public UserSessionReleaseMode ReleaseMode
  {
    [DebuggerStepThrough] get => this.releaseMode;
  }

  /// <summary>Изменяет режим освобождения сессии.</summary>
  /// <param name="newReleaseMode">Режим освобождения сессии</param>
  /// <returns>Признак успешного/неуспешного изменения режима</returns>
  public bool TrySetReleaseMode(UserSessionReleaseMode newReleaseMode)
  {
    if (this.releaseMode != newReleaseMode)
      this.releaseMode = newReleaseMode;
    return true;
  }
}

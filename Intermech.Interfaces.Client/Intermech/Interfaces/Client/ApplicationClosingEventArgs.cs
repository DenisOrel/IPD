// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ApplicationClosingEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события, возникающего перед завершением работы приложения
/// </summary>
[DebuggerDisplay("EventName: {EventName}; Cancel: {Cancel}")]
[Serializable]
public class ApplicationClosingEventArgs : NotificationEventArgs, IDataMergingSupport
{
  /// <summary>
  /// Если равно true, приложение не должно завершать свою работу
  /// </summary>
  private bool _cancel;

  /// <summary>Создать событие с указанным именем</summary>
  /// <param name="eventName">Имя события</param>
  /// <param name="cancel">Если равно true, приложение не должно завершать свою работу</param>
  public ApplicationClosingEventArgs(string eventName, bool cancel)
    : base(eventName)
  {
    this._cancel = cancel;
  }

  /// <summary>
  /// Если равно true, приложение не должно завершать свою работу
  /// </summary>
  public bool Cancel
  {
    get => this._cancel;
    set => this._cancel = value;
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public bool MergeWith(object obj)
  {
    return obj is ApplicationClosingEventArgs closingEventArgs && closingEventArgs._cancel == this._cancel;
  }
}

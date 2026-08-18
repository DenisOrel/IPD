// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerConnectionException
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Исключение, возникающее при попытке подключения к серверу приложений.
/// </summary>
public class IMServerConnectionException : Exception
{
  private bool tryLater;

  /// <summary>Создает объект.</summary>
  /// <param name="message">Сообщение об ошибке</param>
  /// <param name="tryLater">Признак, что продолжать попытки подключения к серверу приложений</param>
  public IMServerConnectionException(string message, bool tryLater)
    : base(message)
  {
    this.tryLater = tryLater;
  }

  /// <summary>Создает объект.</summary>
  /// <param name="message">Сообщение об ошибке</param>
  /// <param name="tryLater">Признак, что продолжать попытки подключения к серверу приложений</param>
  /// <param name="innerException">Вложенное исключение</param>
  public IMServerConnectionException(string message, bool tryLater, Exception innerException)
    : base(message, innerException)
  {
    this.tryLater = tryLater;
  }

  /// <summary>
  /// Возвращает признак, указывающий, стоит ли продолжать попытки подключения к серверу приложений.
  /// </summary>
  public bool TryLater
  {
    [DebuggerStepThrough] get => this.tryLater;
  }
}

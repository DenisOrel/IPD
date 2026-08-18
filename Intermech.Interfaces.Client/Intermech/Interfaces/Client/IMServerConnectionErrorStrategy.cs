// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerConnectionErrorStrategy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для алгоритмов обработки ошибок подключения к серверу приложений.
/// </summary>
public class IMServerConnectionErrorStrategy
{
  /// <summary>
  /// Возвращает описание ошибки подключения к серверу приложений,
  /// пригодное для вывода в журналы приложения или для показа пользователю.
  /// </summary>
  /// <param name="exception">Исключение, возникшее при попытке подключения к серверу приложений</param>
  /// <returns>Описание ошибки подключения</returns>
  public virtual IMServerConnectionErrorInfo FormatConnectionException(
    IMServerConnectionException exception)
  {
    Exception informativeException = this.GetMostInformativeException(exception);
    return informativeException == exception ? new IMServerConnectionErrorInfo(exception.GetType().ToString(), exception.Message) : new IMServerConnectionErrorInfo(exception.GetType().ToString(), $"{exception.Message} {informativeException.Message}");
  }

  /// <summary>
  /// Находит и возвращает наиболее информативное исключение для показа пользователю
  /// </summary>
  /// <param name="exception">Исходное исключение</param>
  /// <returns>Наиболее информативное исключение</returns>
  protected virtual Exception GetMostInformativeException(IMServerConnectionException exception)
  {
    return exception.InnerException != null ? exception.InnerException : (Exception) exception;
  }

  /// <summary>Обрабатывает ошибку подключения к серверу приложений.</summary>
  /// <param name="exception">Исключение, возникшее при попытке подключения к серверу приложений</param>
  /// <returns>Способ переподключения к серверу приложений</returns>
  public virtual IMServerReconnectType HandleConnectionException(
    IMServerConnectionException exception)
  {
    return IMServerReconnectType.AbortConnection;
  }
}

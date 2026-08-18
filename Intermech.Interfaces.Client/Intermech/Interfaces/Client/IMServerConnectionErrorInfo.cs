// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerConnectionErrorInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Информация об ошибке подключения к <see cref="T:Intermech.Interfaces.IMServer" /> или о сбое создания сессии сервера приложений типа <see cref="T:Intermech.Interfaces.IUserSession" />
/// </summary>
/// <remarks>Реализация является immutable и thread safe.</remarks>
public sealed class IMServerConnectionErrorInfo
{
  /// <summary>Создает объект</summary>
  /// <param name="exceptionType">Тип возникшей исключительной ситуации</param>
  /// <param name="exceptionText">Текст исключительной ситуации</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="exceptionType" /> не должен быть null; параметр <paramref name="exceptionText" /> не должен быть null</exception>
  public IMServerConnectionErrorInfo(string exceptionType, string exceptionText)
  {
    if (exceptionType == null)
      throw new ArgumentNullException(nameof (exceptionType));
    if (exceptionText == null)
      throw new ArgumentNullException(nameof (exceptionText));
    this.ExceptionType = exceptionType;
    this.ExceptionText = exceptionText;
  }

  /// <summary>Тип возникшей исключительной ситуации</summary>
  public string ExceptionType { get; }

  /// <summary>Текст исключительной ситуации</summary>
  public string ExceptionText { get; }
}

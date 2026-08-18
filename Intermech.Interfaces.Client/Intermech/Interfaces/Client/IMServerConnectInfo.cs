// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerConnectInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Информация об адресе и параметрах подключения к серверу приложений.
/// </summary>
internal class IMServerConnectInfo : IEquatable<IMServerConnectInfo>
{
  private string serverUrl;

  /// <summary>Создает объект.</summary>
  /// <param name="serverUrl">url для подключения к серверу приложений</param>
  internal IMServerConnectInfo(string serverUrl) => this.serverUrl = serverUrl;

  /// <summary>Возвращает url для подключения к серверу приложений.</summary>
  public string Url
  {
    [DebuggerStepThrough] get => this.serverUrl;
  }

  /// <summary>Проверяет равенство текущего и указанного объектов.</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>Признак равенства объектов</returns>
  public bool Equals(IMServerConnectInfo other)
  {
    return other != null && other.serverUrl == this.serverUrl;
  }

  /// <summary>Проверяет равенство текущего и указанного объектов.</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>Признак равенства объектов</returns>
  public override bool Equals(object obj)
  {
    return obj is IMServerConnectInfo other ? this.Equals(other) : base.Equals(obj);
  }

  /// <summary>Вычисляет хэш-код текущего объекта.</summary>
  /// <returns>Хэш-код текущего объекта</returns>
  public override int GetHashCode() => this.serverUrl.GetHashCode();
}

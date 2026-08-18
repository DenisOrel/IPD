// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IClientPluginsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис, позволяющий клиентским плагинам передавать какую-о информацию на сторону сервера
/// </summary>
public interface IClientPluginsService
{
  /// <summary>Объект для синхронизации</summary>
  object SyncRoot { get; }

  /// <summary>
  /// Зарегистрировать указанный класс для обмена информацией
  /// </summary>
  /// <param name="PluginGuid">Guid плагина</param>
  /// <param name="ClientPluginsDataTransfer">Интерфейс для обмена данными</param>
  void RegisterClientPlugin(
    Guid PluginGuid,
    IClientPluginsDataTransfer ClientPluginsDataTransfer);

  /// <summary>
  /// Разрегистрировать указанный класс для обмена информацией
  /// </summary>
  /// <param name="PluginGuid">Guid плагина</param>
  void UnregisterClientPlugin(Guid PluginGuid);

  /// <summary>
  /// Собрать у зарегистрированных плагинов информацию в указанный словарик
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений [Ключ] = [Значение]</param>
  void GetClientPluginsData(ref HybridDictionary PluginsData);

  /// <summary>
  /// Раздать зарегистрированным плагинам информацию из указанного словарика
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений [Ключ] = [Значение]</param>
  void PutClientPluginsData(ref HybridDictionary PluginsData);
}

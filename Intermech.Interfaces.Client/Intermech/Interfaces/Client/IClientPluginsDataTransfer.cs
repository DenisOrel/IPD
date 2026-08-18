// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IClientPluginsDataTransfer
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Механизм, позволяющий клиентским плагинам читать и записывать данные в коллекцию для передачи на серверную сторону
/// </summary>
public interface IClientPluginsDataTransfer
{
  /// <summary>
  /// Временный Guid плагина, владеющего данным экземпляром классом
  /// </summary>
  Guid PluginGuid { get; }

  /// <summary>
  /// Метод вызывается ядром клиентской части для сбора информации у плагинов.
  /// Плагины, подписавшиеся в коллекции IClientPluginsService, должны записать в словарик
  /// PluginsData свою информацию в виде сериализуемых пар значений [Ключ] = [Значение].
  /// Указанная информация будет передана на серверную сторону.
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений для передачи
  /// дополнительной информации на серверную сторону</param>
  void GetPluginData(HybridDictionary PluginsData);

  /// <summary>
  /// Метод вызывается ядром клиентской части для раздачи информации плагинам.
  /// Плагины, подписавшиеся в коллекции IClientPluginsService, должны считать из словарика
  /// PluginsData свою информацию в виде сериализуемых пар значений [Ключ] = [Значение].
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений для чтения</param>
  void PutPluginData(HybridDictionary PluginsData);
}

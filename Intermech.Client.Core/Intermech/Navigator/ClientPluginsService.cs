
// Type: Intermech.Navigator.ClientPluginsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator;

/// <summary>
/// Сервис, позволяющий клиентским плагинам передавать какую-о информацию на сторону сервера
/// </summary>
public class ClientPluginsService : IClientPluginsService
{
  /// <summary>
  /// Коллекция зарегистрированных плагинов.
  /// Пары значений [(Guid) плагин] = [(IClientPluginsDataTransfer) класс по передаче данных]
  /// </summary>
  private HybridDictionary FDataTransfer = new HybridDictionary(0, true);

  /// <summary>Объект для синхронизации</summary>
  public object SyncRoot => this.FDataTransfer.SyncRoot;

  /// <summary>
  /// Зарегистрировать указанный класс для обмена информацией
  /// </summary>
  /// <param name="PluginGuid">Guid плагина</param>
  /// <param name="ClientPluginsDataTransfer">Интерфейс для обмена данными</param>
  public void RegisterClientPlugin(
    Guid PluginGuid,
    IClientPluginsDataTransfer ClientPluginsDataTransfer)
  {
    if (PluginGuid == Guid.Empty || ClientPluginsDataTransfer == null)
      return;
    this.FDataTransfer[(object) PluginGuid] = (object) ClientPluginsDataTransfer;
  }

  /// <summary>
  /// Разрегистрировать указанный класс для обмена информацией
  /// </summary>
  /// <param name="PluginGuid">Guid плагина</param>
  public void UnregisterClientPlugin(Guid PluginGuid)
  {
    this.FDataTransfer.Remove((object) PluginGuid);
  }

  /// <summary>
  /// Собрать у зарегистрированных плагинов информацию в указанный словарик
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений [Ключ] = [Значение]</param>
  public void GetClientPluginsData(ref HybridDictionary PluginsData)
  {
    if (this.FDataTransfer.Count <= 0)
      return;
    bool flag = PluginsData == null;
    if (PluginsData == null)
      PluginsData = new HybridDictionary(0, true);
    IDictionaryEnumerator enumerator = this.FDataTransfer.GetEnumerator();
    if (enumerator != null)
    {
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        if (enumerator.Value is IClientPluginsDataTransfer pluginsDataTransfer)
          pluginsDataTransfer.GetPluginData(PluginsData);
      }
    }
    if (!flag || PluginsData.Count > 0)
      return;
    PluginsData = (HybridDictionary) null;
  }

  /// <summary>
  /// Раздать зарегистрированным плагинам информацию из указанного словарика
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений [Ключ] = [Значение]</param>
  public void PutClientPluginsData(ref HybridDictionary PluginsData)
  {
    if (this.FDataTransfer.Count <= 0)
      return;
    lock (this.SyncRoot)
    {
      if (PluginsData == null)
        PluginsData = new HybridDictionary(0, true);
      IDictionaryEnumerator enumerator = this.FDataTransfer.GetEnumerator();
      if (enumerator == null)
        return;
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        if (enumerator.Value is IClientPluginsDataTransfer pluginsDataTransfer)
          pluginsDataTransfer.PutPluginData(PluginsData);
      }
    }
  }
}

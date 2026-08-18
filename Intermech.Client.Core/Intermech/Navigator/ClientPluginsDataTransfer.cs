
// Type: Intermech.Navigator.ClientPluginsDataTransfer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Specialized;
using System.Diagnostics;


namespace Intermech.Navigator;

/// <summary>
/// Механизм, позволяющий клиентским плагинам читать и записывать данные в коллекцию для передачи на серверную сторону.
/// Класс-пустышка. Лучше реализовать код интерфейса в самом плагине.
/// </summary>
public class ClientPluginsDataTransfer : IClientPluginsDataTransfer
{
  /// <summary>
  /// Временный Guid плагина, владеющего данным экземпляром классом
  /// </summary>
  protected Guid FPluginGuid = Guid.Empty;

  /// <summary>
  /// Временный Guid плагина, владеющего данным экземпляром классом
  /// </summary>
  public virtual Guid PluginGuid
  {
    [DebuggerStepThrough] get => this.FPluginGuid;
  }

  /// <summary>
  /// Создать новый экземпляр класса ClientPluginsDataTransfer
  /// </summary>
  public ClientPluginsDataTransfer() => this.FPluginGuid = Guid.NewGuid();

  /// <summary>
  /// Метод вызывается ядром клиентской части для сбора информации у плагинов.
  /// Плагины, подписавшиеся в коллекции IClientPluginsService, должны записать в словарик
  /// PluginsData свою информацию в виде сериализуемых пар значений [Ключ] = [Значение].
  /// Указанная информация будет передана на серверную сторону.
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений для передачи
  /// дополнительной информации на серверную сторону</param>
  public virtual void GetPluginData(HybridDictionary PluginsData)
  {
  }

  /// <summary>
  /// Метод вызывается ядром клиентской части для раздачи информации плагинам.
  /// Плагины, подписавшиеся в коллекции IClientPluginsService, должны считать из словарика
  /// PluginsData свою информацию в виде сериализуемых пар значений [Ключ] = [Значение].
  /// </summary>
  /// <param name="PluginsData">Коллекция сериализуемых пар значений для чтения</param>
  public virtual void PutPluginData(HybridDictionary PluginsData)
  {
  }
}

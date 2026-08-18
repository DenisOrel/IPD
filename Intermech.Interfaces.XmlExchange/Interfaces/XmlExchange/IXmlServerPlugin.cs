// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlServerPlugin
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Интерфейс для проверки наличия загруженного плагина на сервере приложений
/// </summary>
public interface IXmlServerPlugin
{
  /// <summary>Guid плагина</summary>
  Guid PluginGuid { get; }
}

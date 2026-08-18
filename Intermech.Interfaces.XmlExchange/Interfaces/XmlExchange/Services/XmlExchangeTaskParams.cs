// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Services.XmlExchangeTaskParams
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Services;

/// <summary>Базовых класс для параметров задач импорта / экспорта</summary>
[Serializable]
public abstract class XmlExchangeTaskParams
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="configurationId"></param>
  protected XmlExchangeTaskParams(long configurationId) => this.ConfigurationId = configurationId;

  /// <summary>Ид. версии конфигурации</summary>
  public long ConfigurationId { get; }
}

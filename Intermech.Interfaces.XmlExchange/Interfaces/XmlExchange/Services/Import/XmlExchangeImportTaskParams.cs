// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Services.Import.XmlExchangeImportTaskParams
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Services.Import;

/// <summary>Класс параметров задачи импорта</summary>
[Serializable]
/// <summary>
/// 
/// </summary>
/// <param name="configurationId"></param>
public class XmlExchangeImportTaskParams(long configurationId) : XmlExchangeTaskParams(configurationId)
{
}

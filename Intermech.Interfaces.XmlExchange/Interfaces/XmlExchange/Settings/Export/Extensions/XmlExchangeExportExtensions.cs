// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Settings.Export.Extensions.XmlExchangeExportExtensions
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Settings.Export.Extensions;

/// <summary>Класс для хранения настроек расширений экспорта</summary>
[XmlRoot("Extentions")]
[Serializable]
public class XmlExchangeExportExtensions : XmlExchangeExportList<XmlExchangeExportExtension>
{
  /// <summary>Поиск элемента по Guid</summary>
  /// <param name="typeGuid"></param>
  /// <returns></returns>
  public XmlExchangeExportExtension GetItemByGuid(Guid guid)
  {
    XmlExchangeExportExtension itemByGuid = (XmlExchangeExportExtension) null;
    foreach (XmlExchangeExportExtension exchangeExportExtension in (List<XmlExchangeExportExtension>) this)
    {
      if (exchangeExportExtension != null && exchangeExportExtension.Guid == guid)
      {
        itemByGuid = exchangeExportExtension;
        break;
      }
    }
    return itemByGuid;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportDefAttrValueList
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Класс для хранения значений атрибутов по умолчанию</summary>
[XmlRoot("default_attributes")]
[Serializable]
public class XmlExchangeExportDefAttrValueList : XmlExchangeExportList<XmlExchangeExportDefAttrValue>
{
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Settings.Export.Common.XmlExportCompressMode
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Settings.Export.Common;

/// <summary>Режим архивирования пакета данных при экспорте</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportCompressMode
{
  /// <summary>Данные не архивируются</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_20")] None,
  /// <summary>ZIP архивация</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_21")] Zip,
}

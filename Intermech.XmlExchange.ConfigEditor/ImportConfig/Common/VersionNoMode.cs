// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.VersionNoMode
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[TypeConverter(typeof (EnumDescConverter))]
public enum VersionNoMode
{
  [Description("Максимальный (следующий) номер версии объекта"), Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.XmlValue("maxValue")] MaxValue,
  [Description("Номер версии объекта из значения в XML. Или максимальный номер, если версия с таким номером или старше существует в базе"), Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.XmlValue("xmlValue")] XmlValue,
  [Description("Номер версии объекта из значения в XML. Исключение из импорта если версия с таким номером или старше существует в базе"), Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.XmlValue("xmlValueSkipOld")] XmlValueSkipOld,
}

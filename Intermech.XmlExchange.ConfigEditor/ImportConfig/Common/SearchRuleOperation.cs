// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.SearchRuleOperation
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[TypeConverter(typeof (EnumDescConverter))]
public enum SearchRuleOperation
{
  [Description("Определяется соответствующим параметром настройки типа объекта")] None,
  [Description("Операция «Или» между условиями на атрибуты при поиске объектов в базе"), XmlValue("OR")] Or,
  [Description("Операция «И» между условиями на атрибуты при поиске объектов в базе"), XmlValue("AND")] And,
}

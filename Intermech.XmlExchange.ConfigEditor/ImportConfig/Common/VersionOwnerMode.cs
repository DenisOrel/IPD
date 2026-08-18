// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.VersionOwnerMode
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[TypeConverter(typeof (EnumDescConverter))]
public enum VersionOwnerMode
{
  [Description("Используется версия объекта, найденная по правилу поиска объекта / версии"), XmlValue("default")] Default,
  [Description("Создание новой версии объекта от базовой"), XmlValue("base")] Base,
  [Description("Поиск максимальной предыдущей версии"), XmlValue("previous")] Previous,
}

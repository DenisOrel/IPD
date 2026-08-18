// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.CreationRuleMode
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[TypeConverter(typeof (EnumDescConverter))]
public enum CreationRuleMode
{
  [Description("Режим создания нового объекта / создания новой версии объекта)"), XmlValue("createVersion")] CreateVersion,
  [Description("Режим поиска и обновления базовой версии объекта / создания новой версии от базовой версии  объекта"), XmlValue("refreshBase")] RefreshBase,
  [Description("Режим поиска версии объекта в базе (равной версии в XML) и обновление данных без очистки состава"), XmlValue("refreshVersion")] RefreshVersion,
  [Description("Режим поиска версии объекта в базе (равной версии в XML) и обновление данных с предварительной очисткой состава"), XmlValue("renewVersion")] RenewVersion,
  [Description("Режим поиска версии объекта в базе (равной версии в XML) и исключение объекта из импорта"), XmlValue("skipVersion")] SkipVersion,
}

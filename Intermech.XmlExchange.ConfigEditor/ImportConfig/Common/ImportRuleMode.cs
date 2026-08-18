// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.ImportRuleMode
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[TypeConverter(typeof (EnumDescConverter))]
public enum ImportRuleMode
{
  [Description("Режим обновления. Если найден существующий объект, то очищается состав и создается новый из объектов XML. Атрибуты объекта обновляются"), XmlValue("renew")] Renew,
  [Description("Режим добавления. Если найден существующий объект, то существующий состав не очищается и создается новый состав из объектов XML. Атрибуты объекта обновляются"), XmlValue("refresh")] Refresh,
  [Description("Создание новой версии на основе найденной существующей версии объекта. По правилу создания версий"), XmlValue("createVersion")] CreateVersion,
  [Description("Создание нового объекта. По правилу создания версий."), XmlValue("create")] Create,
  [Description("Создание объекта на основе справочников НСИ (Imbase). Предварительно производиться поиск по одному из индексированных атрибутов справочника"), XmlValue("createByDictionary")] CreateByDictionary,
  [Description("Импорт объектов данного типа не выполняется"), XmlValue("skip")] Skip,
  [Description("Создание новой версии на основе найденной существующей версии объекта c копированием дочерних связей средствами ядра. По правилу создания версий"), XmlValue("createVersionKernel")] CreateVersionKernel,
}

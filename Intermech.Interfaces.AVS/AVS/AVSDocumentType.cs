// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AVSDocumentType
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.ComponentModel;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Тип документа AVS</summary>
[TypeConverter(typeof (EnumCustomConverter))]
public enum AVSDocumentType
{
  /// <summary>Спецификация</summary>
  [Description("Спецификация ЕСКД"), Category("Спецификации")] Specification,
  /// <summary>Спецификация автомобильная</summary>
  [Description("Спецификация автомобильная"), Category("Спецификации")] AutoIndustrySpecification,
  /// <summary>Спецификация экспортная</summary>
  [Description("Спецификация экспортная"), Category("Спецификации")] ExportSpecification,
  /// <summary>Перечень элементов</summary>
  [Description("Перечень элементов"), Category("Перечни элементов")] ElementList,
  [Description("Пользовательский конструкторский документ"), Category("Конструкторские документы")] UserAVSDocument,
  /// <summary> Конструкторская ведомость </summary>
  [Description("Конструкторская ведомость"), Category("Конструкторские документы")] Vedomost,
  /// <summary> Конструкторская ведомость ВП или ВС</summary>
  [Description("Конструкторская ведомость"), Category("Конструкторские документы")] VedomostVsVp,
  /// <summary> Конструкторская таблица </summary>
  [Description("Конструкторская таблица"), Category("Конструкторские документы")] Tabl,
  /// <summary>Пользовательская спецификация</summary>
  [Description("Пользовательская спецификация"), Category("Спецификации")] UserSpecification,
  /// <summary>Пользовательский перечень элементов</summary>
  [Description("Пользовательский перечень элементов"), Category("Перечни элементов")] UserElementList,
}

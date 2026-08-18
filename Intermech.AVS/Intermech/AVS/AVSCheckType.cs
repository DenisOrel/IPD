// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSCheckType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS;

/// <summary>Тип проверки документа</summary>
[Flags]
public enum AVSCheckType
{
  [Description("Без проверок")] None = 0,
  /// <summary>Проверять пустые значения "Количество"</summary>
  [Description("Не задано количество")] EmptyCount = 1,
  /// <summary>Проверять наличие связи с объектом записи</summary>
  [Description("Не задано количество")] ObjectWithoutRelation = 2,
  /// <summary>Проверять отсутствие количества и связей</summary>
  [Description("Не задано количество")] EmptyCountOrWithoutRelation = ObjectWithoutRelation | EmptyCount, // 0x00000003
  /// <summary>Проверять соответствие количества и позиционного обозначения</summary>
  [Description("Количество не соответствует позиционному обозначению")] Count_PositionDesignation = 4,
  /// <summary>Проверять пустые позиции</summary>
  [Description("Не заполнена позиция")] EmptyPosition = 8,
  /// <summary>??? Проверять соответствие объектов и записей ImBase</summary>
  [Description("Ошибка Imbase")] ImBase = 16, // 0x00000010
  /// <summary>Проверять дублирование позиций</summary>
  [Description("Позиция дублируется")] DuplicatePosition = 32, // 0x00000020
  /// <summary>Проверять ошибки при расчёте массы</summary>
  [Description("Ошибка при расчете массы")] MassCalc = 64, // 0x00000040
  /// <summary>Проверять наличие не числа в позиции</summary>
  [Description("Позиция не число")] NotNumberPosition = 128, // 0x00000080
  /// <summary>Проверять дублирование позиционного обозначения</summary>
  [Description("Позиционное обозначение дублируется")] CheckDuplicatePositionDesignation = 256, // 0x00000100
  /// <summary>Проверять пустые позиционные обозначения</summary>
  [Description("Не задано позиционное обозначение")] EmptyPositionDesignation = 512, // 0x00000200
  /// <summary>Проверять пустые значения "Количество" для всех исполнений в форме Б</summary>
  [Description("Количество отсутствует во всех исполнениях формы Б")] EmptyCountAllProdFormB = 1024, // 0x00000400
  /// <summary>У изделия отсутствует заготовка</summary>
  [Description("Отсутствует запись о заготовке или количество в исполнении")] PartWithoutDraft = 2048, // 0x00000800
  /// <summary>У записи изделия и записи заготовки не совпадает количество</summary>
  [Description("Количество в исполнении заготовки не соответствует детали")] DraftCountDoesntMatch = 4096, // 0x00001000
  /// <summary>В шаблоне спецификации отсутствует настройка вывода для графы Примечание</summary>
  [Description("Отсутствует настройка вывода для графы Примечание")] MissingOutputMappingForNote = 8192, // 0x00002000
  /// <summary>Выполнить все проверки</summary>
  [Description("Все проверки")] All = 2147483647, // 0x7FFFFFFF
}

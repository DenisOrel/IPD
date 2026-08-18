// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ISubstitutesRemarksService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Интерфейс службы, помогающей выполнить генерацию примечания для связей, участвующих в допустимых заменах
/// </summary>
public interface ISubstitutesRemarksService
{
  /// <summary>
  /// Выводить маркеры положения вместо ключевых слов в тексте примечаний допзамен
  /// </summary>
  bool UsePlaceholders { get; set; }

  /// <summary>
  /// Рассчитать примечания для связей, участвующих в допустимых заменах
  /// </summary>
  /// <param name="substsSettings">Настройки допустимых замен</param>
  /// <param name="relAttrs">Набор связей и значения атрибутов, требуемых для расчёта примечаний:
  /// - Количество (связь),
  /// - Обозначение (объект),
  /// - Наименование (объект),
  /// - Позиция (количество),
  /// - Конструкторский основной вариант (связь),
  /// - Номер группы заменителей (связь),
  /// - Номер заменителя в группе (связь)</param>
  /// <returns>Словарь, содержащий идентификаторы связей и соответствующие им значения расшифровок допустимых замен</returns>
  Dictionary<long, string> CalcSubstituteRemarks(
    ISubstitutesSettings substsSettings,
    RelationAttributesPackage relAttrs);

  /// <summary>
  /// Рассчитать примечания для связей, участвующих в допустимых заменах
  /// </summary>
  /// <param name="substsSettings">Настройки допустимых замен</param>
  /// <param name="substs">Допустимые замены в составе</param>
  /// <returns>Словарь, содержащий идентификаторы связей и соответствующие им значения расшифровок допустимых замен</returns>
  Dictionary<long, string> CalcSubstituteRemarks(
    ISubstitutesSettings substsSettings,
    SubstituteObjects substs);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ISubstitutesSettings
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Настройки допустимых замен</summary>
public interface ISubstitutesSettings
{
  /// <summary>Количество в допустимых заменах</summary>
  bool QuantityInSubstitutes { get; set; }

  /// <summary>Размещать количество в скобках</summary>
  bool QuantityInBrackets { get; set; }

  /// <summary>Текст "допуск. замена"</summary>
  string ActualSubstitute { get; set; }

  /// <summary>Текст "совместно с"</summary>
  string ActualSubstitute2 { get; set; }

  /// <summary>Текст "на"</summary>
  string ActualSubstitute3 { get; set; }

  /// <summary>Текст "примен."</summary>
  string MaterialSubstitute { get; set; }

  /// <summary>Текст "совместно с"</summary>
  string MaterialSubstitute2 { get; set; }

  /// <summary>Текст "взамен"</summary>
  string MaterialSubstitute3 { get; set; }

  /// <summary>
  /// Перечислять позиции через данный разделитель (по умолчанию ", ", может быть "или " и т.п.)
  /// </summary>
  string PositionsSeparator { get; set; }

  /// <summary>Текст "примен. с"</summary>
  string Substitute { get; set; }

  /// <summary>Текст "совместно с"</summary>
  string Substitute2 { get; set; }

  /// <summary>Текст "взамен"</summary>
  string Substitute3 { get; set; }

  /// <summary>
  /// Использовать неразрывный пробел между количеством и единицей измерения
  /// </summary>
  bool NonbreakingSpace { get; set; }

  bool IncludePositionalDesignationInNote { get; set; }

  /// <summary>
  /// Загрузить настройки из глобальной конфигурации системы
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если настройки успешно загружены</returns>
  bool LoadSettings(IUserSession session);

  /// <summary>Внести изменения в глобальную конфигурацию системы</summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если изменения успешно внесены</returns>
  bool SaveSettings(IUserSession session);
}

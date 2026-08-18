// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SubstitutesSettings
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Настройки допустимых замен</summary>
public class SubstitutesSettings : LongLifeObject, ISubstitutesSettings
{
  /// <summary>Название модуля - "Intermech.Interfaces.Pdm"</summary>
  protected static string _moduleName = "IPS.Interfaces.Pdm";
  /// <summary>Название секции - "SubstituteSettings"</summary>
  protected static string _sectionName = "SubstituteSettings";
  /// <summary>Значение "Количество в допустимых заменах"</summary>
  protected static string _keyQuantityInSubstitutes = "QS";
  /// <summary>Значение "Размещать количество в скобках"</summary>
  protected static string _keyQuantityInBrackets = "QB";
  /// <summary>Значение "допуск. замена"</summary>
  protected static string _keyActualSubstitute = "AST";
  /// <summary>Значение "совместно с"</summary>
  protected static string _keyActualSubstitute2 = "AST2";
  /// <summary>Значение "на"</summary>
  protected static string _keyActualSubstitute3 = "AST3";
  /// <summary>Значение "примен."</summary>
  protected static string _keyMaterialSubstitute = "MST";
  /// <summary>Значение "совместно с"</summary>
  protected static string _keyMaterialSubstitute2 = "MST2";
  /// <summary>Значение "взамен"</summary>
  protected static string _keyMaterialSubstitute3 = "MST3";
  /// <summary>
  /// Значение "Перечислять позиции через данный разделитель (по умолчанию ", ", может быть "или " и т.п.)"
  /// </summary>
  protected static string _keyPositionsSeparator = "PS";
  /// <summary>Значение "примен. с"</summary>
  protected static string _keySubstitute = "ST";
  /// <summary>Значение "совместно с"</summary>
  protected static string _keySubstitute2 = "ST2";
  /// <summary>Значение "взамен"</summary>
  protected static string _keySubstitute3 = "ST3";
  /// <summary>
  /// Использовать неразрывный пробел между количеством и единицей измерения
  /// </summary>
  protected static string _keyNonbreakingSpace = "NBS";
  /// <summary>Количество в допустимых заменах</summary>
  protected bool _quantityInSubstitutes;
  /// <summary>Размещать количество в скобках</summary>
  protected bool _quantityInBrackets;
  /// <summary>Текст "допуск. замена"</summary>
  protected string _actualSubstitute;
  /// <summary>Текст "совместно с"</summary>
  protected string _actualSubstitute2;
  /// <summary>Текст "на"</summary>
  protected string _actualSubstitute3;
  /// <summary>Текст "примен."</summary>
  protected string _materialSubstitute;
  /// <summary>Текст "совместно с"</summary>
  protected string _materialSubstitute2;
  /// <summary>Текст "взамен"</summary>
  protected string _materialSubstitute3;
  /// <summary>
  /// Перечислять позиции через данный разделитель (по умолчанию ", ", может быть " или " и т.п.)
  /// </summary>
  protected string _positionsSeparator;
  /// <summary>Текст "примен. с"</summary>
  protected string _substitute;
  /// <summary>Текст "совместно"</summary>
  protected string _substitute2;
  /// <summary>Текст "взамен"</summary>
  protected string _substitute3;
  /// <summary>
  /// Использовать неразрывный пробел между количеством и единицей измерения
  /// </summary>
  protected bool _nonbreakingSpace;
  private bool _includePositionalDesignationInNote;

  /// <summary>
  /// Загрузить все настройки из глобальной конфигурации системы
  /// </summary>
  public SubstitutesSettings(IUserSession session) => this.LoadSettings(session);

  /// <summary>Загрузить настройки из глобальной конфигурации</summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если настройки успешно загружены</returns>
  public virtual bool LoadSettings(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    this._quantityInSubstitutes = configurations.ReadBool(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyQuantityInSubstitutes, true, DBConfigMode.GlobalOnly);
    this._quantityInBrackets = configurations.ReadBool(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyQuantityInBrackets, false, DBConfigMode.GlobalOnly);
    this._actualSubstitute = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyActualSubstitute, LocalizationHolder.rm.GetString("Interfaces.Pdm_13"), DBConfigMode.GlobalOnly);
    this._actualSubstitute2 = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyActualSubstitute2, LocalizationHolder.rm.GetString("Interfaces.Pdm_14"), DBConfigMode.GlobalOnly);
    this._actualSubstitute3 = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyActualSubstitute3, LocalizationHolder.rm.GetString("Interfaces.Pdm_15"), DBConfigMode.GlobalOnly);
    this._materialSubstitute = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyMaterialSubstitute, LocalizationHolder.rm.GetString("Interfaces.Pdm_82"), DBConfigMode.GlobalOnly);
    this._materialSubstitute2 = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyMaterialSubstitute2, LocalizationHolder.rm.GetString("Interfaces.Pdm_14"), DBConfigMode.GlobalOnly);
    this._materialSubstitute3 = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyMaterialSubstitute3, LocalizationHolder.rm.GetString("Interfaces.Pdm_83"), DBConfigMode.GlobalOnly);
    this._positionsSeparator = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyPositionsSeparator, LocalizationHolder.rm.GetString("Interfaces.Pdm_16"), DBConfigMode.GlobalOnly);
    this._substitute = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keySubstitute, LocalizationHolder.rm.GetString("Interfaces.Pdm_17"), DBConfigMode.GlobalOnly);
    this._substitute2 = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keySubstitute2, LocalizationHolder.rm.GetString("Interfaces.Pdm_18"), DBConfigMode.GlobalOnly);
    this._substitute3 = configurations.ReadString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keySubstitute3, LocalizationHolder.rm.GetString("Interfaces.Pdm_19"), DBConfigMode.GlobalOnly);
    this._nonbreakingSpace = configurations.ReadBool(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyNonbreakingSpace, false, DBConfigMode.GlobalOnly);
    return true;
  }

  /// <summary>Внести изменения в глобальную конфигурацию</summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если изменения успешно внесены</returns>
  public virtual bool SaveSettings(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    configurations.WriteBool(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyQuantityInSubstitutes, this._quantityInSubstitutes, 0L);
    configurations.WriteBool(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyQuantityInBrackets, this._quantityInBrackets, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyActualSubstitute, this._actualSubstitute, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyActualSubstitute2, this._actualSubstitute2, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyActualSubstitute3, this._actualSubstitute3, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyMaterialSubstitute, this._materialSubstitute, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyMaterialSubstitute2, this._materialSubstitute2, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyMaterialSubstitute3, this._materialSubstitute3, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyPositionsSeparator, this._positionsSeparator, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keySubstitute, this._substitute, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keySubstitute2, this._substitute2, 0L);
    configurations.WriteString(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keySubstitute3, this._substitute3, 0L);
    configurations.WriteBool(SubstitutesSettings._moduleName, SubstitutesSettings._sectionName, SubstitutesSettings._keyNonbreakingSpace, this._nonbreakingSpace, 0L);
    return true;
  }

  /// <summary>Количество в допустимых заменах</summary>
  public bool QuantityInSubstitutes
  {
    [DebuggerStepThrough] get => this._quantityInSubstitutes;
    set => this._quantityInSubstitutes = value;
  }

  /// <summary>Размещать количество в скобках</summary>
  public bool QuantityInBrackets
  {
    [DebuggerStepThrough] get => this._quantityInBrackets;
    set => this._quantityInBrackets = value;
  }

  /// <summary>Текст "допуск. замена"</summary>
  public string ActualSubstitute
  {
    [DebuggerStepThrough] get => this._actualSubstitute;
    set => this._actualSubstitute = value;
  }

  /// <summary>Текст "совместно с"</summary>
  public string ActualSubstitute2
  {
    [DebuggerStepThrough] get => this._actualSubstitute2;
    set => this._actualSubstitute2 = value;
  }

  /// <summary>Текст "на"</summary>
  public string ActualSubstitute3
  {
    [DebuggerStepThrough] get => this._actualSubstitute3;
    set => this._actualSubstitute3 = value;
  }

  /// <summary>Текст "примен."</summary>
  public string MaterialSubstitute
  {
    [DebuggerStepThrough] get => this._materialSubstitute;
    set => this._materialSubstitute = value;
  }

  /// <summary>Текст "совместно с"</summary>
  public string MaterialSubstitute2
  {
    [DebuggerStepThrough] get => this._materialSubstitute2;
    set => this._materialSubstitute2 = value;
  }

  /// <summary>Текст "взамен"</summary>
  public string MaterialSubstitute3
  {
    [DebuggerStepThrough] get => this._materialSubstitute3;
    set => this._materialSubstitute3 = value;
  }

  /// <summary>
  /// Перечислять позиции через данный разделитель (по умолчанию ", ", может быть "или " и т.п.)
  /// </summary>
  public string PositionsSeparator
  {
    [DebuggerStepThrough] get => this._positionsSeparator;
    set => this._positionsSeparator = value;
  }

  /// <summary>Текст "примен. с"</summary>
  public string Substitute
  {
    [DebuggerStepThrough] get => this._substitute;
    set => this._substitute = value;
  }

  /// <summary>Текст "совместно с"</summary>
  public string Substitute2
  {
    [DebuggerStepThrough] get => this._substitute2;
    set => this._substitute2 = value;
  }

  /// <summary>Текст "взамен"</summary>
  public string Substitute3
  {
    [DebuggerStepThrough] get => this._substitute3;
    set => this._substitute3 = value;
  }

  /// <summary>
  /// Использовать неразрывный пробел между количеством и единицей измерения
  /// </summary>
  public bool NonbreakingSpace
  {
    [DebuggerStepThrough] get => this._nonbreakingSpace;
    set => this._nonbreakingSpace = value;
  }

  public bool IncludePositionalDesignationInNote
  {
    get => this._includePositionalDesignationInNote;
    set => this._includePositionalDesignationInNote = value;
  }
}

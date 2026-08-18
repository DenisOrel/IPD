// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.RegNumberSettings
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Настройки для генерации регистрационного номера.</summary>
[Serializable]
public class RegNumberSettings
{
  /// <summary>Строка шаблона.</summary>
  [NotNull]
  public string Template;
  /// <summary>Режим обнуления счетчика (актуален только при включенном CountWithinType)</summary>
  public CountResetTypes CountResetType;
  /// <summary>Нумерация в пределах типа объектов.</summary>
  public bool CountWithinType;
  /// <summary>Нумерация в пределах подразделений.</summary>
  public bool CountWithinUnit;
  /// <summary>Флаг того, что регистрационный номер не присваивается новому документу.</summary>
  public bool EnableEmptyRegNumbers;
  /// <summary>Флаг автоматической генерации регистрационного номера.</summary>
  public bool AutoGenerateRegNumber;
  /// <summary>Флаг установки обозначения равным регистрационному номеру.</summary>
  public bool DesignationEqualRegNumber;

  public RegNumberSettings()
  {
    this.Template = string.Empty;
    this.CountResetType = CountResetTypes.PerYear;
    this.CountWithinType = false;
    this.CountWithinUnit = false;
    this.EnableEmptyRegNumbers = false;
    this.AutoGenerateRegNumber = false;
    this.DesignationEqualRegNumber = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="template">Строка шаблона</param>
  /// <param name="countResetType">Режим обнуления счетчика</param>
  /// <param name="countWithinType">Нумерация в пределах типа объектов</param>
  /// <param name="countWithinUnit">Нумерация в пределах подразделений</param>
  /// <param name="enableEmptyRegNumbers">Флаг того, что регистрационный номер не присваивается новому документу</param>
  /// <param name="autoGenerateRegNumber">Флаг автоматической генерации регистрационного номера</param>
  /// <param name="designationEqualRegNumber">Обозначение равно регистрационному номеру</param>
  public RegNumberSettings(
    [NotNull] string template,
    CountResetTypes countResetType,
    bool countWithinType,
    bool countWithinUnit,
    bool enableEmptyRegNumbers,
    bool autoGenerateRegNumber,
    bool designationEqualRegNumber)
  {
    this.Template = template;
    this.CountResetType = countResetType;
    this.CountWithinType = countWithinType;
    this.CountWithinUnit = countWithinUnit;
    this.EnableEmptyRegNumbers = enableEmptyRegNumbers;
    this.AutoGenerateRegNumber = autoGenerateRegNumber;
    this.DesignationEqualRegNumber = designationEqualRegNumber;
  }

  public override bool Equals(object obj)
  {
    return obj is RegNumberSettings regNumberSettings && regNumberSettings.CountResetType == this.CountResetType && regNumberSettings.CountWithinType == this.CountWithinType && regNumberSettings.CountWithinUnit == this.CountWithinUnit && regNumberSettings.Template == this.Template && regNumberSettings.EnableEmptyRegNumbers == this.EnableEmptyRegNumbers && regNumberSettings.AutoGenerateRegNumber == this.AutoGenerateRegNumber && regNumberSettings.DesignationEqualRegNumber == this.DesignationEqualRegNumber;
  }

  [NotNull]
  public RegNumberSettings Clone()
  {
    return new RegNumberSettings(this.Template, this.CountResetType, this.CountWithinType, this.CountWithinUnit, this.EnableEmptyRegNumbers, this.AutoGenerateRegNumber, this.DesignationEqualRegNumber);
  }
}

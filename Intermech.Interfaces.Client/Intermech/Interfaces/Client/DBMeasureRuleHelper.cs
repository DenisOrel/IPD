// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBMeasureRuleHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using ImSSP;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс предназначен для работы со строкой настроек ввода значений атрибутов, выраженных в единицах измерения
/// </summary>
internal class DBMeasureRuleHelper
{
  private string _RuleString;
  private CAttributeType4Category _Attribute;

  public DBMeasureRuleHelper(string ruleString, CAttributeType4Category attribute)
  {
    this._RuleString = ruleString.Trim();
    this._Attribute = attribute;
  }

  /// <summary>
  /// Проверяет строку настроек на возможность записи в базу и возвращает выделенную из строки настроек
  /// формулу контроля правильности значения
  /// </summary>
  public string ValidateRuleString(string newRuleSettings)
  {
    newRuleSettings = newRuleSettings.Trim();
    if (newRuleSettings == string.Empty)
      return string.Empty;
    string[] strArray = newRuleSettings.Split(',');
    return strArray.Length == 1 || strArray.Length == 4 ? strArray[0] : throw new KernelExceptionID(sc_10484.ssp_appserver_10485(552101709), (object) newRuleSettings);
  }

  /// <summary>Формула контроля правильности значения</summary>
  public string RuleFormula
  {
    [DebuggerStepThrough] get
    {
      if (this._RuleString == string.Empty)
        return string.Empty;
      return this._RuleString.Split(',')[0];
    }
  }

  /// <summary>Идентификатор единицы измерения по умолчанию</summary>
  public long DefaultMeasureID
  {
    [DebuggerStepThrough] get
    {
      if (this._RuleString != string.Empty)
      {
        string[] strArray = this._RuleString.Split(',');
        if (strArray.Length > 1)
        {
          MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(strArray[1]);
          return !descriptor.Empty ? descriptor.MeasureID : throw new KernelExceptionID(244, (object) strArray[1], (object) this._Attribute.Name);
        }
      }
      if (this._Attribute.SizeType > 0L)
      {
        long baseMeasureId = MeasureHelper.GetBaseMeasureID(this._Attribute.SizeType);
        if (baseMeasureId > 0L)
          return baseMeasureId;
      }
      return 0;
    }
  }

  /// <summary>
  /// Нужно ли записывать ид. единиц измерения в строковую часть атрибута
  /// </summary>
  public bool ShortNameInString
  {
    [DebuggerStepThrough] get
    {
      if (this._RuleString == string.Empty)
        return true;
      string[] strArray = this._RuleString.Split(',');
      return strArray.Length <= 2 || strArray[2].Trim() == "1";
    }
  }

  /// <summary>
  /// Приводить ли записываемые значения в единицу измерения по умолчанию
  /// </summary>
  public bool ConvertToDefaultMeasure
  {
    [DebuggerStepThrough] get
    {
      if (this._RuleString == string.Empty)
        return false;
      string[] strArray = this._RuleString.Split(',');
      return strArray.Length > 3 && strArray[3].Trim() == "1";
    }
  }
}

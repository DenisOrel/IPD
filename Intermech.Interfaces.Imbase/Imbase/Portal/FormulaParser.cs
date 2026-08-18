// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.FormulaParser
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>
/// Класс, преобразует формулу атрибута из старого формата Imbase в новый IPS
/// </summary>
internal class FormulaParser
{
  /// <summary>Информация по атрибуту, необходимая для процесса</summary>
  protected FormulaParser.AttributeInfo attributeInfo;

  /// <summary>Конструктор</summary>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута для которого формируется формула</param>
  /// <param name="fieldType">Тип атрибута для которого формируется формула</param>
  public FormulaParser(Guid attributeGuid, FieldTypes fieldType)
  {
    this.attributeInfo = new FormulaParser.AttributeInfo(attributeGuid, fieldType);
  }

  /// <summary>
  /// Получить глобальный идентификатор атрибута, соотвествующего полю FIELD
  /// </summary>
  /// <param name="field"></param>
  /// <returns></returns>
  protected virtual string GetFieldGuid(string field) => field;

  /// <summary>
  /// Метод, который формирует словарь: поле FIELD -&gt; лобальный идентификатор соотвествующего атрибута
  /// </summary>
  /// <returns></returns>
  protected virtual SortedDictionary<string, string> GetFieldsList()
  {
    return new SortedDictionary<string, string>();
  }

  /// <summary>Атрибут имеет числовой тип</summary>
  /// <param name="dataType">Тип атрибута</param>
  /// <param name="attrGUID">Глобальный идентификатор атрибута</param>
  /// <returns></returns>
  protected virtual bool IsNumberAttribute(FieldTypes dataType, Guid attrGUID) => false;

  /// <summary>Получить информацию по атрибуту</summary>
  /// <param name="attributeGuid"></param>
  /// <returns></returns>
  protected virtual FormulaParser.AttributeInfo GetAttributeInfo(Guid attributeGuid)
  {
    return (FormulaParser.AttributeInfo) null;
  }

  /// <summary>Преобразование формулы для новой системы</summary>
  public string Parse(string formula)
  {
    string inString = string.Empty;
    bool flag1 = true;
    int num1;
    if (this.attributeInfo.FieldType == FieldTypes.ftString)
    {
      for (; formula.Length > 0; formula = formula.Substring(num1 < formula.Length ? num1 + 1 : formula.Length))
      {
        if (!formula.Contains("{") || !formula.Contains("}"))
        {
          inString = !(inString == string.Empty) ? $"{inString}+'{formula}'" : $"'{formula}'";
          break;
        }
        int num2 = formula.IndexOf("{");
        num1 = formula.IndexOf("}", num2);
        if (num1 >= 0)
        {
          string str1 = formula.Substring(0, num2);
          string str2 = num1 > num2 ? formula.Substring(num2, num1 - num2) : string.Empty;
          if (flag1)
            flag1 = false;
          else
            inString += "+";
          if (!str1.Equals(string.Empty))
            inString = $"{inString}'{str1}'{(num2 != formula.Length ? "+" : string.Empty)}";
          if (!str2.Equals(string.Empty))
          {
            string str3 = str2.Replace("{", string.Empty).Replace("}", string.Empty);
            int num3 = str3.Contains("[") ? str3.IndexOf("[") : 0;
            int startIndex = str3.Contains("]") ? str3.IndexOf("]") + 1 : str3.Length;
            string fieldStr = this.GetFieldStr(str3.Substring(num3, startIndex - num3).Replace("[", string.Empty).Replace("]", string.Empty).Trim());
            string str4 = str3.Substring(0, num3);
            string str5 = str4.Equals(string.Empty) ? string.Empty : $"'{str4}',";
            string str6 = str3.Substring(startIndex);
            string str7 = str6.Equals(string.Empty) ? string.Empty : $",'{str6}'";
            inString = !str5.Equals(string.Empty) || !str7.Equals(string.Empty) ? inString + $"VAL({(str5.Equals(string.Empty) ? (object) "''," : (object) str5)}{fieldStr}{str7})" : inString + fieldStr;
          }
        }
        else
          break;
      }
    }
    else
    {
      SortedDictionary<string, string> fieldsList = this.GetFieldsList();
      inString = formula;
      bool flag2 = this.IsNumberAttribute(this.attributeInfo.FieldType, this.attributeInfo.AttributeGuid);
      foreach (KeyValuePair<string, string> keyValuePair in fieldsList)
      {
        if (inString.Contains(keyValuePair.Key))
        {
          if (keyValuePair.Value.Equals(string.Empty))
          {
            inString = $"'{formula}'";
            break;
          }
          string newValue = $"[{keyValuePair.Value}]";
          if (flag2)
          {
            FormulaParser.AttributeInfo attributeInfo = this.GetAttributeInfo(new Guid(keyValuePair.Value));
            inString = attributeInfo == null || !this.IsNumberAttribute(attributeInfo.FieldType, attributeInfo.AttributeGuid) ? inString.Replace(keyValuePair.Key, $"DBL({newValue})") : inString.Replace(keyValuePair.Key, newValue);
          }
          else
            inString = inString.Replace(keyValuePair.Key, newValue);
        }
      }
    }
    return this.TrimSkobs(inString);
  }

  protected virtual string GetFieldStr(string fldStr) => $"[{this.GetFieldGuid(fldStr)}]";

  /// <summary>Заменяет {[ в начале формулы на [ и наоборот в конце</summary>
  /// <param name="inString"></param>
  /// <returns></returns>
  private string TrimSkobs(string inString)
  {
    if (inString.IndexOf("{[") == 0 && inString.IndexOf("]}") == inString.Length - 2 && inString.Length == 40)
    {
      inString = inString.Remove(0, 1);
      inString = inString.Remove(inString.Length - 1, 1);
    }
    return inString;
  }

  /// <summary>Информация по атрибуту, необходимая для процесса</summary>
  protected class AttributeInfo
  {
    /// <summary>
    /// Глобальный идентификатор атрибута для которого формируется формула
    /// </summary>
    public Guid AttributeGuid;
    /// <summary>Тип атрибута для которого формируется формула</summary>
    public FieldTypes FieldType;

    public AttributeInfo(Guid attributeGuid, FieldTypes fieldType)
    {
      this.AttributeGuid = attributeGuid;
      this.FieldType = fieldType;
    }
  }
}

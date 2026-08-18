// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.IMHAssortmentClass
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// 
/// </summary>
[Serializable]
public class IMHAssortmentClass
{
  private string _name = string.Empty;
  private Dictionary<string, List<string>> _params = new Dictionary<string, List<string>>();

  /// <summary>Наименование класса.</summary>
  public string Name => this._name;

  /// <summary>Список параметров для поиска.</summary>
  /// <remark>Ключ - наименование обобщенного параметра
  /// Значение - список глобальных идентификаторов атрибутов</remark>
  public Dictionary<string, List<string>> Parameters => this._params;

  /// <summary>Конструктор.</summary>
  /// <param name="name">Наименование обобщенного параметра</param>
  /// <param name="parameters">Список глобальных идентификаторов атрибутов</param>
  public IMHAssortmentClass(string name) => this._name = name;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  public bool AddAbstractName(string name)
  {
    bool flag = false;
    if (!string.IsNullOrEmpty(name) && !this._params.ContainsKey(name))
    {
      this._params.Add(name, new List<string>());
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="abstractName"></param>
  /// <param name="attrGuid"></param>
  /// <param name="addIfEmpty"></param>
  public void AddAttribute(string abstractName, string attrGuid, bool addIfEmpty)
  {
    if (!GuidHelper.IsGuid(attrGuid))
      return;
    if (this._params.ContainsKey(abstractName))
    {
      List<string> stringList = this._params[abstractName];
      if (stringList.Contains(attrGuid))
        return;
      stringList.Add(attrGuid);
    }
    else
    {
      if (!addIfEmpty)
        return;
      this._params.Add(abstractName, new List<string>((IEnumerable<string>) new string[1]
      {
        attrGuid
      }));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  public void DelAbstractName(string name)
  {
    if (string.IsNullOrEmpty(name) || !this._params.ContainsKey(name))
      return;
    this._params.Remove(name);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="abstractName"></param>
  /// <param name="attrGuid"></param>
  public void DelAttribute(string abstractName, string attrGuid)
  {
    if (!this._params.ContainsKey(abstractName))
      return;
    this._params[abstractName].Remove(attrGuid);
  }
}

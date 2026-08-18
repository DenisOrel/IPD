// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.CoatingsFavouriteData
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
public class CoatingsFavouriteData
{
  private string _coatingsKey = string.Empty;
  private string _materialsKey = string.Empty;
  private string _caption = string.Empty;
  private List<object> _params = new List<object>();

  /// <summary>Наименование материала.</summary>
  public string Caption => this._caption;

  /// <summary>
  /// 
  /// </summary>
  public string CoatingsKey => this._coatingsKey;

  /// <summary>
  /// 
  /// </summary>
  public string MaterialsKey => this._materialsKey;

  /// <summary>
  /// 
  /// </summary>
  public List<object> Params => this._params;

  /// <summary>Конструктор.</summary>
  /// <param name="coatingsKey"></param>
  /// <param name="materialsKey"></param>
  /// <param name="parameters"></param>
  /// <param name="caption"></param>
  public CoatingsFavouriteData(
    string coatingsKey,
    string materialsKey,
    List<object> parameters,
    string caption)
  {
    this._coatingsKey = coatingsKey;
    this._materialsKey = materialsKey;
    this._params = parameters ?? new List<object>(0);
    this._caption = caption;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    bool flag = false;
    if (obj is CoatingsFavouriteData)
    {
      CoatingsFavouriteData coatingsFavouriteData = obj as CoatingsFavouriteData;
      if (this._coatingsKey == coatingsFavouriteData.CoatingsKey && this._materialsKey == coatingsFavouriteData.MaterialsKey && this._params.Count == coatingsFavouriteData.Params.Count)
      {
        flag = true;
        for (int index = 0; index < this._params.Count; ++index)
        {
          if (!(this._params[index].ToString() == coatingsFavouriteData.Params[index].ToString()))
          {
            flag = false;
            break;
          }
        }
      }
    }
    else
      flag = base.Equals(obj);
    return flag;
  }

  /// <summary>Наименование материала.</summary>
  /// <returns></returns>
  public override string ToString() => this._caption;
}

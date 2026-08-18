// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.IMHSystemSettings
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
public class IMHSystemSettings
{
  private Dictionary<string, string> _dictSettings;
  private IMHCoatingsSystemSettings _coatingsSettings;
  private List<IMHAssortmentClass> _assortmentSearchSettings;

  /// <summary>
  /// 
  /// </summary>
  public List<IMHAssortmentClass> AssortmentSearchSettings => this._assortmentSearchSettings;

  /// <summary>
  /// 
  /// </summary>
  public IMHCoatingsSystemSettings CoatingsSettings => this._coatingsSettings;

  /// <summary>
  /// 
  /// </summary>
  public Dictionary<string, string> Dict => this._dictSettings;

  /// <summary>Конструктор.</summary>
  /// <param name="dictSettings"></param>
  /// <param name="coatingsSettings"></param>
  /// <param name="assortmentSearchSettings"></param>
  public IMHSystemSettings(
    Dictionary<string, string> dictSettings,
    IMHCoatingsSystemSettings coatingsSettings,
    List<IMHAssortmentClass> assortmentSearchSettings)
  {
    this._dictSettings = dictSettings;
    this._coatingsSettings = coatingsSettings;
    this._assortmentSearchSettings = assortmentSearchSettings;
  }
}

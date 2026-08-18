// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.IMHCoatingsSystemSettings
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// 
/// </summary>
[Serializable]
public class IMHCoatingsSystemSettings
{
  private string _formula = "УЭ [№пп]: [P1] (толщина [P2])";
  private DataTable _dtParams;

  /// <summary>
  /// 
  /// </summary>
  public string Formula => this._formula;

  /// <summary>
  /// 
  /// </summary>
  public DataTable Params => this._dtParams;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dtParams"></param>
  public IMHCoatingsSystemSettings(DataTable dtParams) => this._dtParams = dtParams;
}

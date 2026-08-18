
// Type: Intermech.Security.SecurityNodeClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Security;

/// <summary>
/// Класс для назначения в Node TreeView, где отображаются юзера, роли, группы
/// </summary>
public class SecurityNodeClass
{
  public long UID;
  public QuickObjectInfo QuickObjectInfo;
  public object BeginDate = (object) DBNull.Value;
  public object EndDate = (object) DBNull.Value;
  private bool conditionsEnabled;
  public object Condition;

  public bool ConditionsEnabled => this.conditionsEnabled;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aQuickObjectInfo"></param>
  /// <param name="parentKey">-1 для прав по умолчанию, 0 для назначаемых; для parentKey=-1 fkeyID будет =0, для parentKey=0 fkeyID будет =0 для существующих прав и меньше 0 для новых назначенных в сеансе и еще не сохраненных</param>
  /// <param name="fkeyID">идентификатор F_KEY для идентификации и группировки новых добавляемых прав: меньше 0 для новых, = 0 для существующих-у них</param>
  /// <param name="aBeginDate"></param>
  /// <param name="aEndDate"></param>
  /// <param name="aConditionsEnabled"></param>
  /// <param name="aCondition"></param>
  public SecurityNodeClass(
    long uid,
    QuickObjectInfo aQuickObjectInfo,
    object aBeginDate,
    object aEndDate,
    bool aConditionsEnabled,
    object aCondition)
  {
    this.UID = uid;
    this.QuickObjectInfo = aQuickObjectInfo;
    this.BeginDate = aBeginDate;
    this.EndDate = aEndDate;
    this.conditionsEnabled = aConditionsEnabled;
    this.Condition = aCondition;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBMeasureAttributeType4Relation
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

internal class DBMeasureAttributeType4Relation(ClientSession session, DataRow row) : 
  CAttributeType4Relation(session, row),
  IDBMeasureAttributeType
{
  private DBMeasureRuleHelper _RuleHelper;

  public DBMeasureRuleHelper RuleHelper
  {
    [DebuggerStepThrough] get
    {
      if (this._RuleHelper == null)
        this._RuleHelper = new DBMeasureRuleHelper(this.ValidationRule, (CAttributeType4Category) this);
      return this._RuleHelper;
    }
  }

  public string RuleFormula
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.RuleHelper.RuleFormula;
    }
  }

  public long DefaultMeasureID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.RuleHelper.DefaultMeasureID;
    }
  }

  public bool ShortNameInString
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.RuleHelper.ShortNameInString;
    }
  }

  public bool ConvertToDefaultMeasure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.RuleHelper.ConvertToDefaultMeasure;
    }
  }

  /// <summary>
  /// Проверяет допустимость присвоения данному атрибуту единицы измерения muID
  /// </summary>
  public void ValidateMuID(long muID)
  {
    this._clientSession.Guard.ValidateCall();
    (this.attrType as IDBMeasureAttributeType).ValidateMuID(muID);
  }

  /// <summary>
  /// Возвращает список глобальных идентификаторов физических величин, единицы измерения которых можно присваивать данному атрибуту.
  /// Возвращает массив нулевой длины, если атрибуту можно присвоить любую единицу измерения.
  /// </summary>
  public long[] GetValidPhysicalValues()
  {
    this._clientSession.Guard.ValidateCall();
    return (this.attrType as IDBMeasureAttributeType).GetValidPhysicalValues();
  }

  public bool IsCompatible(long aMeasureID)
  {
    this._clientSession.Guard.ValidateCall();
    return (this.attrType as IDBMeasureAttributeType).IsCompatible(aMeasureID);
  }
}

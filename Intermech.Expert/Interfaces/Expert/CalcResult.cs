// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.CalcResult
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Expert;

[Serializable]
public class CalcResult : IComparable
{
  protected internal long _objId = -1;
  protected internal int _attrTypeID = -1;
  protected object _value;
  protected string _attrName = "";
  protected object _oldValue;

  public CalcResult(long objId, int attrTypeId, object Value)
  {
    this._objId = objId;
    this._attrTypeID = attrTypeId;
    this._value = Value;
  }

  /// <summary>Ид. типа искомого атрибута</summary>
  public long objectID
  {
    [DebuggerStepThrough] get => this._objId;
  }

  /// <summary>Ид. типа искомого атрибута</summary>
  public int attrTypeID
  {
    [DebuggerStepThrough] get => this._attrTypeID;
  }

  /// <summary>Значение</summary>
  public object value
  {
    [DebuggerStepThrough] get => this._value;
  }

  /// <summary>Имя атрибута</summary>
  public string attrName
  {
    [DebuggerStepThrough] get => this._attrName;
  }

  /// <summary>Старое значение</summary>
  public object oldValue
  {
    [DebuggerStepThrough] get => this._oldValue;
  }

  public void UpdateHeaderInfo(IUserSession ius)
  {
    this._attrName = MetaDataHelper.GetAttributeType(this._attrTypeID).Name;
    IDBObject dbObject = ius.GetObject(this._objId, false);
    if (dbObject == null)
      return;
    this._oldValue = dbObject.GetAttributeByID(this._attrTypeID)?.Value;
  }

  /// <summary>Comparer</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public int CompareTo(object obj)
  {
    return obj.GetType() == typeof (CalcResult) && Math.Abs(this._objId) == Math.Abs(((CalcResult) obj).objectID) ? 0 : -1;
  }
}

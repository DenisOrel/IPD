// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.CalcAttrPair
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Класс для "расчитываемых" атрибутов</summary>
[Serializable]
public class CalcAttrPair : ICloneable
{
  /// <summary>Идентификатор версии текущего объекта</summary>
  protected internal long _objID = -1;
  /// <summary>Ид. типа искомого атрибута</summary>
  protected internal int _attrTypeID = -1;
  /// <summary>Ид. типа искомого объекта</summary>
  protected internal int _objTypeID = -1;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  protected internal void SetObjID(long value) => this._objID = value;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  protected internal void SetAttrTypeID(int value) => this._attrTypeID = value;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  protected internal void SetObjTypeID(int value) => this._objTypeID = value;

  /// <summary>Конструктор</summary>
  /// <param name="objID">Идентификатор версии текущего объекта </param>
  /// <param name="attrTypeID">Ид. типа искомого атрибута</param>
  public CalcAttrPair(long objID, int attrTypeID)
    : this(objID, -1, attrTypeID)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objID">Идентификатор версии текущего объекта </param>
  /// <param name="objTypeID">Ид. типа искомого атрибута</param>
  /// <param name="attrTypeID">д. типа искомого объекта</param>
  public CalcAttrPair(long objID, int objTypeID, int attrTypeID)
  {
    this._objID = objID;
    this._attrTypeID = attrTypeID;
    this._objTypeID = objTypeID;
  }

  /// <summary>Идентификатор версии текущего объекта</summary>
  public long objID
  {
    [DebuggerStepThrough] get => this._objID;
  }

  /// <summary>Ид. типа искомого атрибута</summary>
  public int attrTypeID
  {
    [DebuggerStepThrough] get => this._attrTypeID;
  }

  /// <summary>Ид. типа искомого объекта</summary>
  public int objTypeID
  {
    [DebuggerStepThrough] get => this._objTypeID;
    set => this.SetObjTypeID(value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objID"></param>
  /// <param name="attrTypeID"></param>
  /// <returns></returns>
  private static int CalcHashCode(long objID, int attrTypeID)
  {
    return Convert.ToInt32(objID ^ (long) attrTypeID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => (int) this._objID ^ this._attrTypeID;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj is CalcAttrPair calcAttrPair && this._objID == calcAttrPair._objID && this._objTypeID == calcAttrPair._objTypeID && this._attrTypeID == calcAttrPair._attrTypeID;
  }

  public override string ToString()
  {
    return $"[ObjId={this._objID.ToString()}, ObjType={(object) this._objTypeID}, AttrType={(object) this._attrTypeID}]";
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public object Clone()
  {
    return (object) new CalcAttrPair(this._objID, this._objTypeID, this._attrTypeID);
  }
}

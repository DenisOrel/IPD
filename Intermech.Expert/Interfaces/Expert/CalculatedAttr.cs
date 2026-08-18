// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.CalculatedAttr
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Значение атрибута, хранимое в задаче ЭС</summary>
[Serializable]
public class CalculatedAttr
{
  /// <summary>Пара "тип объекта" + "Тип атрибута"</summary>
  protected CalcAttrPair _ca_pair;
  /// <summary>Значение</summary>
  protected object _value;
  /// <summary>Является ли атрибут временным?</summary>
  protected bool _temporary;
  /// <summary>True, если этот атрибут уже прописан</summary>
  protected bool _assigned;
  /// <summary>
  /// Состояние атрибута (присвоен ли атрибут пользователем или рассчитан ЭС)
  /// </summary>
  protected AttrState _attState;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  protected internal void SetValue(object value)
  {
    if (value is ICloneable cloneable)
      this._value = cloneable.Clone();
    else
      this._value = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objID"></param>
  /// <param name="attrTypeID"></param>
  /// <param name="Val"></param>
  public CalculatedAttr(long objID, int attrTypeID, object Val)
  {
    this._ca_pair = new CalcAttrPair(objID, attrTypeID);
    this._temporary = CalculatedAttr.IsTempAttr(this.ca_pair);
    this.SetValue(Val);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ca_pair"></param>
  /// <param name="Val"></param>
  public CalculatedAttr(CalcAttrPair ca_pair, object Val)
    : this(ca_pair, Val, AttrState.Unknown)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ca_pair"></param>
  /// <param name="Val"></param>
  /// <param name="aState"></param>
  public CalculatedAttr(CalcAttrPair ca_pair, object Val, AttrState aState)
  {
    this._ca_pair = ca_pair;
    this._temporary = CalculatedAttr.IsTempAttr(ca_pair);
    this._attState = aState;
    this.SetValue(Val);
  }

  /// <summary>Пара "тип объекта" + "тип атрибута"</summary>
  public CalcAttrPair ca_pair => this._ca_pair;

  /// <summary>Значение</summary>
  public object Value
  {
    get => this._value;
    set => this.SetValue(value);
  }

  /// <summary>Является ли атрибут временным?</summary>
  public bool Temporary
  {
    get => this._temporary;
    set => this._temporary = value;
  }

  /// <summary>
  /// Источник атрибута (присвоен пользователем или рассчитан ЭС)
  /// </summary>
  public AttrState attState
  {
    get => this._attState;
    set => this._attState = value;
  }

  /// <summary>Было ли это значение уже записано в объект</summary>
  public bool Assigned
  {
    get => this._assigned;
    set => this._assigned = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ca_pair"></param>
  /// <returns></returns>
  public static bool IsTempAttr(CalcAttrPair ca_pair) => false;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this.ca_pair.GetHashCode();
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.AttrChange
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Элементарное изменение атрибута. Используется для показа пользователю изменений
/// </summary>
[Serializable]
public class AttrChange : ICloneable
{
  private int _attrId;
  private FieldTypes _attrType;
  private object _oldValue;
  private object _newValue;

  /// <summary>Идентификатор атрибута</summary>
  public int AttrId
  {
    get => this._attrId;
    set => this._attrId = value;
  }

  /// <summary>Тип данных атрибута</summary>
  public FieldTypes AttrType
  {
    get => this._attrType;
    set => this._attrType = value;
  }

  /// <summary>Старое значение атрибута (только для показа)</summary>
  public object OldValue
  {
    get => this._oldValue;
    set => this._oldValue = value;
  }

  /// <summary>Новое значение атрибута</summary>
  public object NewValue
  {
    get => this._newValue;
    set => this._newValue = value;
  }

  /// <summary>
  /// Самый общий конструктор, когда (почти) все параметры известны
  /// </summary>
  /// <param name="attrId">Ид атрибута</param>
  /// <param name="oldValue">Старое значение атрибута</param>
  /// <param name="newValue">Новое (рассчитанное или присвоенное) значение атрибута</param>
  /// <param name="attrType">Тип атрибута (можно не задавать)</param>
  public AttrChange(int attrId, object oldValue, object newValue, FieldTypes attrType = FieldTypes.ftUnknown)
  {
    this._attrId = attrId;
    this._attrType = attrType;
    this._oldValue = oldValue;
    this._newValue = newValue;
  }

  /// <summary>Конструктор из базы</summary>
  /// <param name="idbA">Атрибут из базы (должен быть не null!)</param>
  /// <param name="newValue">Новое значение для атрибута</param>
  public AttrChange(IDBAttribute idbA, object newValue)
  {
    this._attrId = idbA.AttributeID;
    this._attrType = idbA.DataType;
    this._oldValue = idbA.ValuesCount != 1 ? (object) new PacketValue((IEnumerable) idbA.Values, DataTypeConvertor.AttrType2DataType(this._attrType, this._attrId)) : idbA.Value;
    this._newValue = newValue;
  }

  public object Clone()
  {
    return (object) new AttrChange(this.AttrId, this.OldValue, this.NewValue, this.AttrType);
  }
}

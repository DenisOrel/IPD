// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.DiapValue
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert;

/// <summary>Used to represent diapazones in the packets</summary>
[Serializable]
public class DiapValue : ISerializable, ICloneable
{
  private ExpertValue _low;
  private ExpertValue _high;

  /// <summary>Коснтруктор</summary>
  public DiapValue()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="low">начальное значение</param>
  /// <param name="high">конечное значение</param>
  public DiapValue(ExpertValue low, ExpertValue high)
  {
    this._low = low;
    this._high = high;
  }

  /// <summary>Начальное значение</summary>
  public ExpertValue Low
  {
    get => this._low;
    set => this._low = value;
  }

  /// <summary>Конечное значение</summary>
  public ExpertValue High
  {
    get => this._high;
    set => this._high = value;
  }

  /// <summary>Проверка на равенство</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (!obj.GetType().Equals(typeof (DiapValue)))
      return base.Equals(obj);
    DiapValue diapValue = obj as DiapValue;
    return this._low.Equals((object) diapValue._low) && this._high.Equals((object) diapValue._high);
  }

  /// <summary>Хэш-код</summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>В строку</summary>
  /// <returns></returns>
  public override string ToString()
  {
    return this._low != null && this._high != null ? $"{this._low.ToString()}:{this._high.ToString()}" : string.Empty;
  }

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected DiapValue(SerializationInfo info, StreamingContext context)
  {
    this._low = info.GetValue(nameof (Low), typeof (ExpertValue)) as ExpertValue;
    this._high = info.GetValue(nameof (High), typeof (ExpertValue)) as ExpertValue;
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Low", (object) this._low);
    info.AddValue("High", (object) this._high);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    return (object) new DiapValue(this._low != null ? this._low.Clone() as ExpertValue : (ExpertValue) null, this._high != null ? this._high.Clone() as ExpertValue : (ExpertValue) null);
  }
}

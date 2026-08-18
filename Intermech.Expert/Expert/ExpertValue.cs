// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertValue
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert;

/// <summary>Класс содержит описание одиночного значения</summary>
[Serializable]
public class ExpertValue : ISerializable, ICloneable, IConvertible
{
  private DataType _valueType;
  private object _value;

  /// <summary>Конструктор</summary>
  /// <param name="valueType">тип значения</param>
  /// <param name="value">значение</param>
  public ExpertValue(DataType valueType, object value)
  {
    this._valueType = valueType;
    this._value = value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  public ExpertValue(bool value)
    : this(DataType.Boolean, (object) value)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  public ExpertValue(DateTime value)
    : this(DataType.Date, (object) value)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  public ExpertValue(DiapValue value)
    : this(DataType.Diap, (object) value)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  public ExpertValue(double value)
    : this(DataType.Float, (object) value)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  /// <param name="isObjectLink">Опеределяет: значение это ссылка на объект (true) или целое число (false)</param>
  public ExpertValue(long value, bool isObjectLink)
    : this(isObjectLink ? DataType.ObjectLink : DataType.Integer, (object) value)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  public ExpertValue(PacketValue value)
    : this(DataType.Packet, (object) value)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  public ExpertValue(string value)
    : this(DataType.String, (object) value)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">значение</param>
  public ExpertValue(MeasuredValue value)
    : this(DataType.Measured, (object) value.Caption)
  {
  }

  /// <summary>Тип значения</summary>
  public DataType ValueType => this._valueType;

  /// <summary>Значение</summary>
  public object Value
  {
    get => this._value;
    set => this._value = value;
  }

  /// <summary>Пустое значение</summary>
  /// <returns></returns>
  public static ExpertValue Empty() => new ExpertValue(string.Empty);

  /// <summary>Пустое значение</summary>
  /// <param name="dataType">тип значения</param>
  /// <returns></returns>
  public static ExpertValue Empty(DataType dataType)
  {
    switch (dataType)
    {
      case DataType.Integer:
        return new ExpertValue(0L, false);
      case DataType.Float:
        return new ExpertValue(0.0);
      case DataType.Measured:
        return new ExpertValue(DataType.Measured, (object) new MeasuredValue(0.0, 0L));
      case DataType.Date:
        return new ExpertValue(DateTime.Now);
      case DataType.Boolean:
        return new ExpertValue(false);
      case DataType.ObjectLink:
      case DataType.ObjectIdLink:
        return new ExpertValue(-1L, true);
      case DataType.Packet:
        return new ExpertValue(new PacketValue());
      case DataType.Diap:
        return new ExpertValue(new DiapValue());
      default:
        return ExpertValue.Empty();
    }
  }

  /// <summary>В строку</summary>
  /// <returns></returns>
  public override string ToString() => this._value == null ? string.Empty : this._value.ToString();

  /// <summary>Проверка на равенство</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (!obj.GetType().Equals(typeof (ExpertValue)))
      return base.Equals(obj);
    ExpertValue expertValue = obj as ExpertValue;
    return this._valueType.Equals((object) expertValue._valueType) && this._value.Equals(expertValue._value);
  }

  /// <summary>Хэш-код</summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected ExpertValue(SerializationInfo info, StreamingContext context)
  {
    Dictionary<string, Type> paramsType = SerializationInfoHelper.GetParamsType(info);
    Type type1 = (Type) null;
    ref Type local = ref type1;
    paramsType.TryGetValue("Type", out local);
    this._valueType = !(type1 == typeof (int)) ? (DataType) EnumTypeHelper.GetEnumValue(typeof (DataType), info.GetString("Type"), (object) DataType.Integer) : (DataType) info.GetInt32("Type");
    try
    {
      string mValue = info.GetString(nameof (Value));
      Type type2 = DataTypeConvertor.DataType2Type(this._valueType);
      if (type2 == typeof (double))
      {
        try
        {
          this._value = (object) Convert.ToDouble(mValue);
        }
        catch (FormatException ex)
        {
          if (mValue.Contains(","))
            this._value = (object) Convert.ToDouble(mValue.Replace(",", "."));
          if (!mValue.Contains("."))
            return;
          this._value = (object) Convert.ToDouble(mValue.Replace(".", ","));
        }
      }
      else if (type2 == typeof (MeasuredValue))
      {
        try
        {
          this._value = (object) MeasureHelper.ConvertToMeasuredValue(mValue);
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case InvalidCastException _:
            case FormatException _:
              this._value = (object) null;
              break;
          }
        }
      }
      else
      {
        try
        {
          this._value = Convert.ChangeType((object) mValue, type2);
        }
        catch (InvalidCastException ex)
        {
          this._value = info.GetValue(nameof (Value), type2);
        }
        catch (FormatException ex)
        {
          this._value = info.GetValue(nameof (Value), type2);
        }
      }
    }
    catch (InvalidCastException ex)
    {
      string str = info.GetString(nameof (Value));
      if (str == null || str == string.Empty)
        this._value = (object) null;
      else
        throw;
    }
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Type", (int) this._valueType);
    info.AddValue("Value", this._value);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    ICloneable cloneable = this._value as ICloneable;
    return (object) new ExpertValue(this._valueType, cloneable != null ? cloneable.Clone() : this._value);
  }

  public TypeCode GetTypeCode()
  {
    switch (this._valueType)
    {
      case DataType.Integer:
        return TypeCode.Int64;
      case DataType.Float:
        return TypeCode.Double;
      case DataType.String:
        return TypeCode.String;
      case DataType.Date:
        return TypeCode.DateTime;
      case DataType.Boolean:
        return TypeCode.Boolean;
      case DataType.ObjectLink:
      case DataType.ObjectIdLink:
        return TypeCode.Object;
      default:
        return TypeCode.Object;
    }
  }

  public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(this._value, provider);

  public byte ToByte(IFormatProvider provider) => Convert.ToByte(this._value, provider);

  public char ToChar(IFormatProvider provider) => Convert.ToChar(this._value, provider);

  public DateTime ToDateTime(IFormatProvider provider) => Convert.ToDateTime(this._value, provider);

  public Decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(this._value, provider);

  public double ToDouble(IFormatProvider provider) => Convert.ToDouble(this._value, provider);

  public short ToInt16(IFormatProvider provider) => Convert.ToInt16(this._value, provider);

  public int ToInt32(IFormatProvider provider) => Convert.ToInt32(this._value, provider);

  public long ToInt64(IFormatProvider provider) => Convert.ToInt64(this._value, provider);

  public sbyte ToSByte(IFormatProvider provider) => Convert.ToSByte(this._value, provider);

  public float ToSingle(IFormatProvider provider) => Convert.ToSingle(this._value, provider);

  public string ToString(IFormatProvider provider) => Convert.ToString(this._value, provider);

  public object ToType(Type conversionType, IFormatProvider provider)
  {
    return Convert.ChangeType(this._value, conversionType, provider);
  }

  public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(this._value, provider);

  public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(this._value, provider);

  public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(this._value, provider);
}

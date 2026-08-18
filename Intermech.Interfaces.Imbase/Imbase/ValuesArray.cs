// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ValuesArray
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace Intermech.Imbase;

[Serializable]
public class ValuesArray : ISerializable, IComparable, IConvertible
{
  private Array _array;
  private Type _elementType;

  public ValuesArray()
  {
    this._array = (Array) null;
    this._elementType = (Type) null;
  }

  public ValuesArray(Array array, Type elementType)
  {
    if (array != null)
      this._array = array.Clone() as Array;
    this._elementType = elementType;
  }

  protected ValuesArray(SerializationInfo info, StreamingContext context)
    : this()
  {
    this._array = (Array) info.GetValue("array", typeof (Array));
    this._elementType = (Type) info.GetValue("elementType", typeof (Type));
  }

  private string Value
  {
    get
    {
      if (this._array == null || this._array.Length == 0)
        return string.Empty;
      object obj = this._array.GetValue(0);
      return obj == null ? string.Empty : obj.ToString();
    }
    set
    {
    }
  }

  public int Length => this._array == null ? 0 : this._array.Length;

  public object[] GetArray()
  {
    int length = this.Length;
    object[] destinationArray = new object[length];
    if (this._array != null)
      Array.Copy(this._array, (Array) destinationArray, length);
    return destinationArray;
  }

  public Type ElementType => this._elementType;

  public void SetValues(Array values)
  {
    if (values == null)
      this._array = Array.CreateInstance(typeof (object), 0);
    this._array = values.Clone() as Array;
  }

  public object GetValue(int index)
  {
    return this._array == null || this._array.Length == 0 ? (object) null : this._array.GetValue(index);
  }

  private IConvertible GetConvertible(int index)
  {
    return (this.GetValue(index) ?? (object) DBNull.Value) as IConvertible;
  }

  public override string ToString()
  {
    if (this._array == null || this._array.Length == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder(128 /*0x80*/);
    int length = this._array.Length;
    int num = length - 1;
    for (int index = 0; index < length; ++index)
    {
      object obj = this._array.GetValue(index);
      if (obj != null)
        stringBuilder.Append(obj.ToString());
      if (index < num)
        stringBuilder.Append("; ");
    }
    return stringBuilder.ToString();
  }

  public override bool Equals(object obj) => base.Equals(obj);

  public override int GetHashCode()
  {
    return this._array == null ? DBNull.Value.GetHashCode() : this._array.GetHashCode();
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("array", (object) this._array);
    info.AddValue("elementType", (object) this._elementType);
  }

  public int CompareTo(object obj)
  {
    if (!(obj is ValuesArray valuesArray))
      return 1;
    object obj1 = this.GetValue(0);
    object obj2 = valuesArray.GetValue(0);
    if (obj1 != null)
      return (obj1 as IComparable).CompareTo(obj2);
    return obj2 == null ? 0 : -1;
  }

  public TypeCode GetTypeCode() => this.GetConvertible(0).GetTypeCode();

  public bool ToBoolean(IFormatProvider provider) => this.GetConvertible(0).ToBoolean(provider);

  public byte ToByte(IFormatProvider provider) => this.GetConvertible(0).ToByte(provider);

  public char ToChar(IFormatProvider provider) => this.GetConvertible(0).ToChar(provider);

  public DateTime ToDateTime(IFormatProvider provider)
  {
    return this.GetConvertible(0).ToDateTime(provider);
  }

  public Decimal ToDecimal(IFormatProvider provider) => this.GetConvertible(0).ToDecimal(provider);

  public double ToDouble(IFormatProvider provider) => this.GetConvertible(0).ToDouble(provider);

  public short ToInt16(IFormatProvider provider) => this.GetConvertible(0).ToInt16(provider);

  public int ToInt32(IFormatProvider provider) => this.GetConvertible(0).ToInt32(provider);

  public long ToInt64(IFormatProvider provider) => this.GetConvertible(0).ToInt64(provider);

  public sbyte ToSByte(IFormatProvider provider) => this.GetConvertible(0).ToSByte(provider);

  public float ToSingle(IFormatProvider provider) => this.GetConvertible(0).ToSingle(provider);

  public string ToString(IFormatProvider provider) => this.ToString();

  public object ToType(Type conversionType, IFormatProvider provider)
  {
    return typeof (ValuesArray).Equals(conversionType) ? (object) this : this.GetConvertible(0).ToType(conversionType, provider);
  }

  public ushort ToUInt16(IFormatProvider provider) => this.GetConvertible(0).ToUInt16(provider);

  public uint ToUInt32(IFormatProvider provider) => this.GetConvertible(0).ToUInt32(provider);

  public ulong ToUInt64(IFormatProvider provider) => this.GetConvertible(0).ToUInt64(provider);
}

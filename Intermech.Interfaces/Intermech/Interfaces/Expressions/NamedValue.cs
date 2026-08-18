
// Type: Intermech.Interfaces.Expressions.NamedValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Expressions
{
    /// <summary>Класс для работы с именованым значением</summary>
    public class NamedValue : IConvertible
    {
      private string _name;
      private IConvertible _value;
      private Type _valueType;

      public Type ValueType => this._valueType;

      public string Name => this._name;

      public NamedValue()
      {
      }

      public NamedValue(string name, object value) => this.SetData(name, value);

      public NamedValue SetData(string name, object value)
      {
        if (value == null)
          throw new ArgumentException("Значение аргумента value не может быть null.");
        this._name = name;
        this._value = value as IConvertible;
        this._valueType = value.GetType();
        return this;
      }

      public override string ToString() => this._value.ToString();

      public TypeCode GetTypeCode() => this._value.GetTypeCode();

      public bool ToBoolean(IFormatProvider provider) => this._value.ToBoolean(provider);

      public byte ToByte(IFormatProvider provider) => this._value.ToByte(provider);

      public char ToChar(IFormatProvider provider) => this._value.ToChar(provider);

      public DateTime ToDateTime(IFormatProvider provider) => this._value.ToDateTime(provider);

      public Decimal ToDecimal(IFormatProvider provider) => this._value.ToDecimal(provider);

      public double ToDouble(IFormatProvider provider) => this._value.ToDouble(provider);

      public short ToInt16(IFormatProvider provider) => this._value.ToInt16(provider);

      public int ToInt32(IFormatProvider provider) => this._value.ToInt32(provider);

      public long ToInt64(IFormatProvider provider) => this._value.ToInt64(provider);

      public sbyte ToSByte(IFormatProvider provider) => this._value.ToSByte(provider);

      public float ToSingle(IFormatProvider provider) => this._value.ToSingle(provider);

      public string ToString(IFormatProvider provider) => this._value.ToString(provider);

      public object ToType(Type conversionType, IFormatProvider provider)
      {
        return this._value.ToType(conversionType, provider);
      }

      public ushort ToUInt16(IFormatProvider provider) => this._value.ToUInt16(provider);

      public uint ToUInt32(IFormatProvider provider) => this._value.ToUInt32(provider);

      public ulong ToUInt64(IFormatProvider provider) => this._value.ToUInt64(provider);
    }
}

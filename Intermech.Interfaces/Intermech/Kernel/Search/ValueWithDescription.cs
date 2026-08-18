
// Type: Intermech.Kernel.Search.ValueWithDescription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>Класс для хранения значения с расшифровкой</summary>
    [Serializable]
    public class ValueWithDescription : IConvertible
    {
      /// <summary>Значение</summary>
      public object Value { get; set; }

      /// <summary>Строковая расшифровка</summary>
      public string Description { get; set; }

      public ValueWithDescription()
      {
      }

      public ValueWithDescription(object value, string description)
      {
        this.Value = value;
        this.Description = description;
      }

      public override string ToString() => this.Description;

      public TypeCode GetTypeCode() => TypeCode.String;

      public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(this.Value, provider);

      public char ToChar(IFormatProvider provider) => Convert.ToChar(this.Value, provider);

      public sbyte ToSByte(IFormatProvider provider) => Convert.ToSByte(this.Value, provider);

      public byte ToByte(IFormatProvider provider) => Convert.ToByte(this.Value, provider);

      public short ToInt16(IFormatProvider provider) => Convert.ToInt16(this.Value, provider);

      public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(this.Value, provider);

      public int ToInt32(IFormatProvider provider) => Convert.ToInt32(this.Value, provider);

      public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(this.Value, provider);

      public long ToInt64(IFormatProvider provider) => Convert.ToInt64(this.Value, provider);

      public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(this.Value, provider);

      public float ToSingle(IFormatProvider provider) => Convert.ToSingle(this.Value, provider);

      public double ToDouble(IFormatProvider provider) => Convert.ToDouble(this.Value, provider);

      public Decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(this.Value, provider);

      public DateTime ToDateTime(IFormatProvider provider) => Convert.ToDateTime(this.Value, provider);

      public string ToString(IFormatProvider provider) => this.Description;

      public object ToType(Type conversionType, IFormatProvider provider)
      {
        return Convert.ChangeType(this.Value, conversionType, provider);
      }
    }
}

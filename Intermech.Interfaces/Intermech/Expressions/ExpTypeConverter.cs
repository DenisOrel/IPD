
// Type: Intermech.Expressions.ExpTypeConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Expressions;
using System;


namespace Intermech.Expressions
{
    /// <summary>
    /// TypeConverter. Contains methods related to conversion between types.
    /// </summary>
    public class ExpTypeConverter
    {
      public static Type GetValueType(object value)
      {
        return value is NamedValue namedValue ? namedValue.ValueType : value.GetType();
      }

      /// <summary>
      /// Returns a value indicating whether the specified SourceValue can be converted to the specified DestinationType.
      /// </summary>
      /// <param name="sourceValue">Source value.</param>
      /// <param name="destType">Destination type.</param>
      /// <returns>True, if the specified SourceType can be converted to the specified DestinationType; otherwise, false.</returns>
      public static bool CanConvert(object sourceValue, Type destType)
      {
        return ExpTypeConverter.CanConvert(ExpTypeConverter.GetValueType(sourceValue), destType);
      }

      /// <summary>
      /// Returns a value indicating whether the specified SourceType can be converted to the specified DestinationType.
      /// </summary>
      /// <param name="sourceType">Source type.</param>
      /// <param name="destType">Destination type.</param>
      /// <returns>True, if the specified SourceType can be converted to the specified DestinationType; otherwise, false.</returns>
      public static bool CanConvert(Type sourceType, Type destType)
      {
        if (sourceType.Equals(destType))
          return true;
        if (sourceType.Equals(typeof (byte)))
          return destType.Equals(typeof (ushort)) || destType.Equals(typeof (short)) || destType.Equals(typeof (uint)) || destType.Equals(typeof (int)) || destType.Equals(typeof (ulong)) || destType.Equals(typeof (long)) || destType.Equals(typeof (float)) || destType.Equals(typeof (double)) || destType.Equals(typeof (Decimal));
        if (sourceType.Equals(typeof (sbyte)))
          return destType.Equals(typeof (short)) || destType.Equals(typeof (int)) || destType.Equals(typeof (long)) || destType.Equals(typeof (float)) || destType.Equals(typeof (double)) || destType.Equals(typeof (Decimal));
        if (sourceType.Equals(typeof (short)))
          return destType.Equals(typeof (int)) || destType.Equals(typeof (long)) || destType.Equals(typeof (float)) || destType.Equals(typeof (double)) || destType.Equals(typeof (Decimal));
        if (sourceType.Equals(typeof (ushort)))
          return destType.Equals(typeof (uint)) || destType.Equals(typeof (int)) || destType.Equals(typeof (ulong)) || destType.Equals(typeof (long)) || destType.Equals(typeof (float)) || destType.Equals(typeof (double)) || destType.Equals(typeof (Decimal));
        if (sourceType.Equals(typeof (char)))
          return destType.Equals(typeof (ushort)) || destType.Equals(typeof (uint)) || destType.Equals(typeof (int)) || destType.Equals(typeof (ulong)) || destType.Equals(typeof (long)) || destType.Equals(typeof (float)) || destType.Equals(typeof (double)) || destType.Equals(typeof (Decimal));
        if (sourceType.Equals(typeof (int)))
          return destType.Equals(typeof (long)) || destType.Equals(typeof (double)) || destType.Equals(typeof (Decimal));
        if (sourceType.Equals(typeof (uint)))
          return destType.Equals(typeof (long)) || destType.Equals(typeof (double)) || destType.Equals(typeof (Decimal));
        if (sourceType.Equals(typeof (long)))
          return destType.Equals(typeof (Decimal)) || destType.Equals(typeof (double));
        if (sourceType.Equals(typeof (ulong)))
          return destType.Equals(typeof (Decimal));
        return sourceType.Equals(typeof (float)) && destType.Equals(typeof (double));
      }
    }
}

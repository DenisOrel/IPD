
// Type: Intermech.Data.TypedNull
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Data
{
    public sealed class TypedNull : IEquatable<TypedNull>
    {
      private readonly Type valueType;
      private static readonly Dictionary<Type, TypedNull> instanceCache = new Dictionary<Type, TypedNull>();

      private TypedNull(Type valueType)
      {
        this.valueType = !(valueType == (Type) null) ? valueType : throw new ArgumentNullException(nameof (valueType));
      }

      public bool Equals(TypedNull other) => other.valueType == this.valueType;

      public override bool Equals(object obj)
      {
        return !(obj is TypedNull typedNull) ? base.Equals(obj) : typedNull.valueType == this.valueType;
      }

      public override int GetHashCode() => this.valueType.GetHashCode();

      public override string ToString() => "null";

      public Type ValueType
      {
        [DebuggerStepThrough] get => this.valueType;
      }

      public static TypedNull String => TypedNull.Instance(typeof (string));

      public static TypedNull Int32 => TypedNull.Instance(typeof (int));

      public static TypedNull Int64 => TypedNull.Instance(typeof (long));

      public static TypedNull Double => TypedNull.Instance(typeof (double));

      public static TypedNull Boolean => TypedNull.Instance(typeof (bool));

      public static TypedNull Guid => TypedNull.Instance(typeof (Guid));

      public static TypedNull Instance(Type valueType)
      {
        TypedNull typedNull;
        lock (TypedNull.instanceCache)
        {
          if (!TypedNull.instanceCache.TryGetValue(valueType, out typedNull))
          {
            typedNull = new TypedNull(valueType);
            TypedNull.instanceCache.Add(valueType, typedNull);
          }
        }
        return typedNull;
      }
    }
}

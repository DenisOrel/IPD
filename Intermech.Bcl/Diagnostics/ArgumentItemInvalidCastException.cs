
// Type: Intermech.Diagnostics.ArgumentItemInvalidCastException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация вида "Ошибка конвертации элемента коллекции-аргумента".</summary>
    [Serializable]
    public class ArgumentItemInvalidCastException : ArgumentInvalidCastException, ISerializable
    {
      [CanBeNull]
      [NotWhitespace]
      public string CollectionClassName { get; }

      public ArgumentItemInvalidCastException()
      {
      }

      protected ArgumentItemInvalidCastException([CanBeNull] string message)
        : this((IEnumerable) null, (string) null, (Type) null, message)
      {
      }

      protected ArgumentItemInvalidCastException([CanBeNull] string message, [CanBeNull] Exception innerException)
        : this((IEnumerable) null, (string) null, (Type) null, message, innerException)
      {
      }

      public ArgumentItemInvalidCastException(
        [CanBeNull, NoEnumeration] IEnumerable collection,
        [CanBeNull, InvokerParameterName] string collectionName,
        [CanBeNull] Type type,
        [CanBeNull] string message = null,
        [CanBeNull] Exception innerException = null)
        : base(type, message, collectionName, innerException)
      {
        this.CollectionClassName = collection?.GetType().FullName;
      }

      public ArgumentItemInvalidCastException(
        [CanBeNull, NoEnumeration] IEnumerable collection,
        [CanBeNull] Type type,
        [CanBeNull] string message = null,
        [CanBeNull] Exception innerException = null)
        : base(type, message, (string) null, innerException)
      {
        this.CollectionClassName = collection?.GetType().FullName;
      }

      public ArgumentItemInvalidCastException(
        [CanBeNull, InvokerParameterName] string collectionName,
        [CanBeNull] Type type,
        [CanBeNull] string message = null,
        [CanBeNull] Exception innerException = null)
        : base(type, message, collectionName, innerException)
      {
      }

      protected ArgumentItemInvalidCastException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.CollectionClassName = info.GetString(nameof (CollectionClassName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("CollectionClassName", (object) this.CollectionClassName);
      }

      [NotNull]
      public override string Message
      {
        get
        {
          if (!string.IsNullOrWhiteSpace(this.OriginalMessage))
            return this.OriginalMessage;
          if (string.IsNullOrWhiteSpace(this.ParamName))
          {
            if (string.IsNullOrWhiteSpace(this.CollectionClassName))
              return string.IsNullOrWhiteSpace(this.TypeName) ? "Invalid cast of argument collection items." : $"Invalid cast of argument collection items to {this.TypeName} type.";
            if (string.IsNullOrWhiteSpace(this.TypeName))
              return $"Invalid cast {this.CollectionClassName} argument collection items.";
            return $"Invalid cast {this.CollectionClassName} argument collection items to {this.TypeName} type.";
          }
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
          {
            if (string.IsNullOrWhiteSpace(this.TypeName))
              return $"Invalid cast of {this.ParamName} argument collection items.";
            return $"Invalid cast of {this.ParamName} argument collection items to {this.TypeName} type.";
          }
          return string.IsNullOrWhiteSpace(this.TypeName) ? $"Invalid cast of {this.ParamName} argument collection ({this.CollectionClassName})." : $"Invalid cast of {this.ParamName} argument collection ({this.CollectionClassName}) items to {this.TypeName} type.";
        }
      }
    }
}

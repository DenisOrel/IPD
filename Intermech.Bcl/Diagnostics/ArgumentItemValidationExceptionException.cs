
// Type: Intermech.Diagnostics.ArgumentItemValidationExceptionException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация вида "Элемент коллекции не удовлетворяет условию валидации".</summary>
    [Serializable]
    public class ArgumentItemValidationExceptionException : ArgumentException, ISerializable
    {
      [CanBeNull]
      public string CollectionClassName { get; }

      public ArgumentItemValidationExceptionException()
        : this((string) null, (string) null, (string) null)
      {
      }

      protected ArgumentItemValidationExceptionException(
        [CanBeNull, NoEnumeration] string collectionClassName,
        [CanBeNull, InvokerParameterName] string collectionName,
        [CanBeNull] string message)
        : base(collectionName, message)
      {
        this.CollectionClassName = collectionClassName;
      }

      public ArgumentItemValidationExceptionException(
        [CanBeNull, NoEnumeration] IEnumerable collection,
        [CanBeNull, InvokerParameterName] string collectionName = null,
        [CanBeNull] string message = null)
        : this(collection?.GetType().FullName, collectionName, message)
      {
      }

      public ArgumentItemValidationExceptionException([CanBeNull, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
        : this((string) null, collectionName, message)
      {
      }

      protected ArgumentItemValidationExceptionException(
        [NotNull] SerializationInfo info,
        StreamingContext context)
        : base(info, context)
      {
        this.CollectionClassName = info.GetString(nameof (CollectionClassName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("CollectionClassName", (object) this.CollectionClassName);
      }

      [CanBeNull]
      protected string OriginalMessage => base.Message;

      [NotNull]
      public override string Message
      {
        get
        {
          if (!string.IsNullOrWhiteSpace(this.OriginalMessage))
            return this.OriginalMessage;
          if (string.IsNullOrWhiteSpace(this.ParamName))
            return string.IsNullOrWhiteSpace(this.CollectionClassName) ? "Argument collection items does not satisfy the conditions." : $"Argument collection of type {this.CollectionClassName} contains item that does not satisfy the conditions.";
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
            return $"Argument collection {this.ParamName} contains item that does not satisfy the conditions.";
          return $"Argument collection {this.ParamName} of type {this.CollectionClassName} contains item that does not satisfy the conditions.";
        }
      }
    }
}

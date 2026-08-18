
// Type: Intermech.Diagnostics.ItemValidationExceptionException
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
    public class ItemValidationExceptionException : Exception, ISerializable
    {
      [CanBeNull]
      public string CollectionClassName { get; }

      [CanBeNull]
      public string CollectionName { get; }

      public ItemValidationExceptionException()
        : this((string) null, (string) null, (string) null)
      {
      }

      protected ItemValidationExceptionException(
        [CanBeNull, InvokerParameterName] string collectionClassName,
        [CanBeNull, InvokerParameterName] string collectionName,
        [CanBeNull] string message)
        : base(message)
      {
        this.CollectionClassName = collectionClassName;
        this.CollectionName = collectionName;
      }

      public ItemValidationExceptionException(
        [CanBeNull, NoEnumeration] IEnumerable collection,
        [CanBeNull, InvokerParameterName] string collectionName = null,
        [CanBeNull] string message = null)
        : this(collection?.GetType().FullName, collectionName, message)
      {
      }

      public ItemValidationExceptionException([CanBeNull, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
        : this((string) null, collectionName, message)
      {
      }

      protected ItemValidationExceptionException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.CollectionClassName = info.GetString(nameof (CollectionClassName));
        this.CollectionName = info.GetString(nameof (CollectionName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("CollectionClassName", (object) this.CollectionClassName);
        info.AddValue("CollectionName", (object) this.CollectionName);
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
          if (string.IsNullOrWhiteSpace(this.CollectionName))
            return string.IsNullOrWhiteSpace(this.CollectionClassName) ? "Collection items does not satisfy the conditions." : $"Collection of type {this.CollectionClassName} contains item that does not satisfy the conditions.";
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
            return $"Collection {this.CollectionName} contains item that does not satisfy the conditions.";
          return $"Collection {this.CollectionName} of type {this.CollectionClassName} contains item that does not satisfy the conditions.";
        }
      }
    }
}

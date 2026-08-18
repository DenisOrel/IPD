
// Type: Intermech.Diagnostics.ItemNullsNotAllowedException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация вида "Коллекция не должна содержать null".</summary>
    [Serializable]
    public class ItemNullsNotAllowedException : NullReferenceException, ISerializable
    {
      [CanBeNull]
      public string CollectionClassName { get; }

      [CanBeNull]
      public string CollectionName { get; }

      public ItemNullsNotAllowedException()
        : this((IEnumerable) null)
      {
      }

      public ItemNullsNotAllowedException(
        [CanBeNull, NoEnumeration] IEnumerable collection,
        [CanBeNull, InvokerParameterName] string collectionName = null,
        [CanBeNull] string message = null)
        : base(message)
      {
        this.CollectionClassName = collection?.GetType().FullName;
        this.CollectionName = collectionName;
      }

      public ItemNullsNotAllowedException([CanBeNull, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
        : this((IEnumerable) null, collectionName, message)
      {
      }

      protected ItemNullsNotAllowedException([NotNull] SerializationInfo info, StreamingContext context)
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
            return "Collection cannot contains nulls.";
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
            return $"Collection {this.CollectionName} cannot contains nulls.";
          return $"Collection {this.CollectionName} of type {this.CollectionClassName} cannot contains nulls.";
        }
      }
    }
}

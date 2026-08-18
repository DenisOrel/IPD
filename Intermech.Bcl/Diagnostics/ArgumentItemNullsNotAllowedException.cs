
// Type: Intermech.Diagnostics.ArgumentItemNullsNotAllowedException
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
    public class ArgumentItemNullsNotAllowedException : ArgumentNullException, ISerializable
    {
      [CanBeNull]
      public string CollectionClassName { get; }

      public ArgumentItemNullsNotAllowedException()
        : this((IEnumerable) null)
      {
      }

      public ArgumentItemNullsNotAllowedException(
        [CanBeNull, NoEnumeration] IEnumerable collection,
        [CanBeNull, InvokerParameterName] string collectionName = null,
        [CanBeNull] string message = null)
        : base(collectionName, message)
      {
        this.CollectionClassName = collection?.GetType().FullName;
      }

      public ArgumentItemNullsNotAllowedException([CanBeNull, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
        : this((IEnumerable) null, collectionName, message)
      {
      }

      protected ArgumentItemNullsNotAllowedException([NotNull] SerializationInfo info, StreamingContext context)
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
            return "Argument collection cannot contains nulls.";
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
            return $"Argument collection {this.ParamName} cannot contains nulls.";
          return $"Argument collection {this.ParamName} of type {this.CollectionClassName} cannot contains nulls.";
        }
      }
    }
}
